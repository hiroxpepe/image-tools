/*
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 2 of the License, or
 * (at your option) any later version.
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Drawing;
using System.Drawing.Imaging;
// System.Windows.Forms を持ち込まない。

namespace ImageCutLib {
    /// <summary>
    /// 画像切り出し処理を行うクラスです。
    /// @author h.adachi
    /// </summary>
    public class Manager {

        ///////////////////////////////////////////////////////////////////////////////////////////    
        // フィールド (キャメルケース: 名詞、形容詞、bool => is+形容詞、has+過去分詞、can+動詞原型、三単現動詞)

        private static Manager instance = new Manager(); // シングルトン

        private Item item; // 設定保持オブジェクト

        private Image souceImage; // 元画像

        private Image darkenedImage; // 明度を落とした画像

        private Image editedImage; // 矩形描画された画像

        private Size tbnSize; // サムネイル画像の大きさ

        private bool hasImage = false; // 画像を持っているか

        private Rectangle selected; // 選択された矩形

        ///////////////////////////////////////////////////////////////////////////////////////////    
        // コンストラクタ

        /// <summary>
        /// パブリックコンストラクタを隠蔽します。
        /// </summary>
        private Manager() {
        }

        /// <summary>
        /// シングルトンとして内部インスタンスを返します。
        /// </summary>
        public static Manager GetInstance() {
            return instance;
        }

        /////////////////////////////////////////////////////////////////////////////////
        // プロパティ (パスカルケース: 名詞、形容詞、bool => Is+形容詞、Has+過去分詞、Can+動詞原型、三単現動詞)

        /// <summary>
        /// 設定を返します。
        /// </summary>
        public Item Item {
            get => item;
        }

        /// <summary>
        /// 選択範囲を返します。
        /// </summary>
        public Rectangle Selected {
            get => selected;
        }

        /// <summary>
        /// 選択範囲の有無を返します。
        /// </summary>
        public bool HasSelected {
            get {
                if (!(selected.Width <= 1 && selected.Height <= 1)) { // TODO: なぜ <= 1 か？
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 画像ストリームを設定します。
        /// </summary>
        public Image Image {
            set {
                souceImage = value;
                editedImage = souceImage; // 初期化は同じ画像
                // ADD // 明度を下げた画像作成
                darkenedImage = getBrightnessAdjustedImage(souceImage, -180);
                // ADD
                if (souceImage == null) {
                    throw new System.ArgumentException("Parameter cannot be null", "image");
                } else {
                    hasImage = true;
                }
            }
        }

        /// <summary>
        /// 出力すべき画像を持っているかどうかを返します。
        /// </summary>
        public bool HasImage {
            get => hasImage;
        }

        /// <summary>
        /// クリップボードから取得したそのままの画像を返します。
        /// </summary>
        public Image RawImage {
            get => souceImage;
        }

        /// <summary>
        /// 矩形選択された画像を返します。
        /// </summary>
        public Image SelectedImage {
            get {
                var _bmp = new Bitmap(souceImage);
                var _addjust = new Rectangle(selected.Location, new Size(selected.Width + 1, selected.Height + 1)); // 切出しサイズに調整が必要
                return _bmp.Clone(_addjust, _bmp.PixelFormat);
            }
        }

        /// <summary>
        /// サムネイル画像のサイズを設定します。
        /// </summary>
        public Size ThumbnailSize {
            set {
                tbnSize.Width = value.Width;
                tbnSize.Height = value.Height;
            }
        }

        /// <summary>
        /// サムネイルの大きさにリサイズした画像を返します。
        /// </summary>
        public Image ThumbnailImage {
            get {
                var _tbnImage = new Bitmap(tbnSize.Width, tbnSize.Height);
                var _g = Graphics.FromImage(_tbnImage);
                var _widthRate = 0f;
                var _heightRate = 0f;
                if (editedImage.Width == 960 && editedImage.Height == 540) { // 960px * 540px フルサイズ
                    _g.DrawImage(editedImage, 0, 0, tbnSize.Width, tbnSize.Height);
                } else if (editedImage.Width > editedImage.Height) { // 横長画像
                    _heightRate = editedImage.Height / (float) editedImage.Width;
                    var _offset = (int) (tbnSize.Height - tbnSize.Width * _heightRate) / 2;
                    _g.DrawImage(editedImage, 0, _offset, tbnSize.Width, tbnSize.Width * _heightRate);
                } else if (editedImage.Width < editedImage.Height) { // 縦長画像
                    _widthRate = editedImage.Width / (float) editedImage.Height;
                    var _offset = (int) (tbnSize.Width - tbnSize.Height * _widthRate) / 2;
                    _g.DrawImage(editedImage, _offset, 0, tbnSize.Height * _widthRate, tbnSize.Height);
                } else { // 正方形画像
                    var _offset = (tbnSize.Width - tbnSize.Height) / 2;
                    _g.DrawImage(editedImage, _offset, 0, tbnSize.Height, tbnSize.Height);
                }
                _g.Dispose();
                return _tbnImage;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // パブリック メソッド (パスカルケース: 動詞)

        /// <summary>
        /// 画像の上に矩形を描画した画像ストリームを取得します。
        /// </summary>
        /// <returns>※画像を取得した先で古い画像の Dispose() を呼び出すことが必要です。</returns>
        public Bitmap DrawRectangleOnImage(Rectangle rectangle) {
            // 矩形描画先となる画像を新たに取得
            editedImage = new Bitmap(souceImage/*darkenedImage*/);

            // [ADD]元画像から矩形領域を切り抜いた画像を作成 ///////////////////////////////////////////
            // 選択矩形でオリジナル画像を切り抜く
            //var _bmp = new Bitmap(souceImage);
            //var _addjust = new Rectangle(selected.Location, new Size(selected.Width, selected.Height));
            //Bitmap _rectImg = _bmp.Clone(_addjust, _bmp.PixelFormat);

            // [ADD]ベース画像に合成する
            //Graphics _g = Graphics.FromImage(editedImage);
            //_g.DrawImage(_rectImg, selected.Location.X, selected.Location.Y, selected.Width, selected.Height);

            // 矩形領域を描画
            Graphics _g = Graphics.FromImage(editedImage);
            var _point = rectangle.Location;
            var _size = rectangle.Size;
            _g.DrawRectangle(Pens.Red, _point.X, _point.Y, _size.Width, _size.Height);
            _g.Dispose();

            return (Bitmap) editedImage;
        }

        /// <summary>
        /// 設定を選択矩形に適用します。
        /// </summary>
        public void ApplyItem(Item item) {
            selected.X = item.X;
            selected.Y = item.Y;
            selected.Width = item.Width;
            selected.Height = item.Height;
        }

        /// <summary>
        /// 選択範囲をプロパティに設定してから返します。
        /// </summary>
        public Rectangle GetAppliedSelected(Rectangle target) {
            selected = target;
            // 設定をオブジェクトとして保持する
            item = new Item("未設定," + selected.X + "," + selected.Y + "," + selected.Width + "," + selected.Height + ",未設定" + ",未設定");
            return selected;
        }

        /////////////////////////////////////////////////////////////////////////////////
        // プライベート メソッド (キャメルケース: 名詞、形容詞)

        ///// <summary>
        ///// 明度を調整した画像にオリジナルの選択範囲を上から貼ります。
        ///// </summary>
        //private Image getRectanglePastedImage(Image souceImage, Image adjustedImage) {
        //    // ベースは明度調整済み画像
        //    Bitmap _baseImg = new Bitmap(adjustedImage); //new Bitmap(adjustedImage.Width, adjustedImage.Height);
        //    Graphics _g = Graphics.FromImage(_baseImg);

        //    // 選択矩形でオリジナル画像を切り抜く
        //    var _bmp = new Bitmap(souceImage);
        //    var _addjust = new Rectangle(selected.Location, new Size(selected.Width, selected.Height));
        //    Bitmap _rectImg = _bmp.Clone(_addjust, _bmp.PixelFormat);

        //    // ベース画像に合成する
        //    _g.DrawImage(_rectImg, selected.Location.X, selected.Location.Y, selected.Width, selected.Height);
        //    _g.Dispose();
        //    return _baseImg;
        //}

        /// <summary>
        /// 画像の明度を調整して返します。
        /// </summary>
        /// <param name="brightness"> 明るさ（-255～255）</param>
        private Image getBrightnessAdjustedImage(Image souceImage, int brightness) {
            // 明るさを変更した画像の描画先となるImageオブジェクトを作成
            Bitmap _editedImage = new Bitmap(souceImage.Width, souceImage.Height);
            Graphics _g = Graphics.FromImage(_editedImage);

            // ColorMatrixオブジェクトの作成
            // 指定された値をRBGの各成分にプラスする
            float _plusVal = (float) brightness / 255f;
            var _cm = new ColorMatrix(
                new float[][] {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {_plusVal, _plusVal, _plusVal, 0, 1}
            });

            // ImageAttributesオブジェクトの作成
            var _ia = new ImageAttributes();
            // ColorMatrixを設定する
            _ia.SetColorMatrix(_cm);

            // ImageAttributesを使用して描画
            _g.DrawImage(souceImage,
                new Rectangle(0, 0, souceImage.Width, souceImage.Height),
                0, 0, souceImage.Width, souceImage.Height, GraphicsUnit.Pixel, _ia);

            // リソースを解放する
            _g.Dispose();
            return _editedImage;
        }
    }

}
