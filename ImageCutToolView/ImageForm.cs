using System;
using System.Drawing;
using System.Windows.Forms;
using ImageCutToolCore;

namespace ImageCutToolView {

    public partial class ImageForm : Form {

        // TODO: UIの Backcolor を Control から変更している

        // TODO: 処理する画像の解像度を設定出来るようにする

        // 960px * 540px のフルサイズのキャプチャー画像を表示する

        /////////////////////////////////////////////////////////////////////////////////
        // 定数 (大文字単語 アンダースコア区切り: 名詞)

        private static int MAX_WIDTH = 960;

        private static int MAX_HEIGHT = 540;

        private static int BASE = 1;

        /////////////////////////////////////////////////////////////////////////////////
        // フィールド (キャメルケース: 名詞、形容詞、bool => is+形容詞、has+過去分詞、can+動詞原型、三単現動詞)

        private Manager manager = null; // 画像切り出しオブジェクト

        private Point mouseDown = new Point(); // 矩形選択の時にマウスダウンしたポイント

        private Point mouseUp = new Point(); // 矩形選択の時にマウスアップしたポイント

        private Bitmap bmp; // 表示する画像

        bool view = false; // 描画フラグ

        private Rectangle selected; // 選択された矩形

        private Rectangle dragged; // ドラッグされる矩形

        private bool isDragged = false; // 矩形範囲モードか、矩形ドラッグ移動モードかフラグ

        private Point startForDragge; // 矩形がドラッグ移動される開始ポイント

        private long mouseMoveIdx = 0; // マウス処理を間引くためのカウンタ

        private bool widthLimit = false; // 選択された矩形が右端に達しているかどうか

        private bool heightLimit = false; // 選択された矩形が下端に達しているかどうか

        private Size sizeOnLimit = new Size(); // 選択された矩形が右下端に達した時の矩形サイズ

        /////////////////////////////////////////////////////////////////////////////////
        // コンストラクタ

        public ImageForm() {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////
        // オーバーライドメソッド

        /// <summary>
        /// 矢印キー入力の処理をオーバーライドします。
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData) {
            //TRACE("--keyData: " + keyData.ToString());
            switch (keyData.ToString()) { // 文字列で取らないと Ctrl 同時押しが判定出来なかった
                /////// move
                case "Up":
                    moveUp();
                    break;
                case "Down":
                    moveDoun();
                    break;
                case "Left":
                    moveLeft();
                    break;
                case "Right":
                    moveRight();
                    break;
                /////// move * 10
                case "Up, Shift":
                    moveUp(10);
                    break;
                case "Down, Shift":
                    moveDoun(10);
                    break;
                case "Left, Shift":
                    moveLeft(10);
                    break;
                case "Right, Shift":
                    moveRight(10);
                    break;
                /////// scale
                case "Up, Control":
                    scaleDownHeight();
                    break;
                case "Down, Control":
                    scaleUpHeight();
                    break;
                case "Left, Control":
                    scaleDownWidth();
                    break;
                case "Right, Control":
                    scaleUpWidth();
                    break;
                /////// scale * 10
                case "Up, Shift, Control":
                    scaleDownHeight(10);
                    break;
                case "Down, Shift, Control":
                    scaleUpHeight(10);
                    break;
                case "Left, Shift, Control":
                    scaleDownWidth(10);
                    break;
                case "Right, Shift, Control":
                    scaleUpWidth(10);
                    break;
                default:
                    return base.ProcessDialogKey(keyData);
            }
            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////
        // イベントハンドラ

        /// <summary>
        /// 画像フォームロード時に呼ばれます。
        /// </summary>
        private void ImageForm_Load(object sender, EventArgs e) {
            try {
                manager = Manager.GetInstance(); // manager オブジェクト取得
                pictureBoxImage.Image = manager.RawImage;
                bmp = new Bitmap(manager.RawImage);

                initializeControl(); // コントロールを初期化
                
                var _selected = manager.Selected;
                if (!(_selected.Size.Width == 1 && _selected.Size.Height == 1)) { // 矩形の設定がある場合
                    getDrawnSelected(_selected.Location, _selected.Size); // 選択矩形の描画
                    updateNumericUpDownOnSelected();
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ピクチャーBOXマウスダウン時に呼ばれます。
        /// </summary>
        private void pictureBoxImage_MouseDown(object sender, MouseEventArgs e) {
            isMousePointerInSelectedRect(e); // マウスカーソル位置判定

            if (e.Button == MouseButtons.Left) { // マウス左ボタン押した
                if (isMousePointerInSelectedRect(e)) { // ドラッグ移動開始
                    startForDragge = new Point(e.X, e.Y);
                    isDragged = true;
                }

                // 描画フラグON
                view = true;

                // マウスを押したポイントを保存
                mouseDown.X = e.X;
                mouseDown.Y = e.Y;
            }
        }

        /// <summary>
        /// ピクチャーBOXマウスアップ時に呼ばれます。
        /// </summary>
        private void pictureBoxImage_MouseUp(object sender, MouseEventArgs e) {
            isMousePointerInSelectedRect(e); // マウスカーソル位置判定

            if (e.Button == MouseButtons.Left) { // マウス左ボタン離した
                if (isMousePointerInSelectedRect(e)) { // ドラッグ移動終了
                    startForDragge = new Point(0, 0);
                    isDragged = false;
                    selected = manager.GetAppliedSelected(dragged); // ドラッグで離された矩形にする

                } else if (!isMousePointerInSelectedRect(e)) { // 矩形選択終了
                    Point _start = new Point();
                    Point _end = new Point();

                    // マウスを離したポイントを保存
                    mouseUp.X = e.X;
                    mouseUp.Y = e.Y;

                    // 座標から(X,Y)座標を計算
                    getRegion(mouseDown, mouseUp, ref _start, ref _end);

                    // 矩形サイズを計算
                    var _size = new Size(getLength(_start.X, _end.X), getLength(_start.Y, _end.Y));

                    // 矩形領域を描画
                    drawRegion(new Rectangle(_start, _size));
                }
            }

            // 描画フラグOFF ※ここで正しい
            view = false;
        }

        /// <summary>
        /// ピクチャーBOXマウスムーブ時に呼ばれます。
        /// </summary>
        private void pictureBoxImage_MouseMove(object sender, MouseEventArgs e) {
            isMousePointerInSelectedRect(e); // マウスカーソル位置判定

            Point _mouseMove = new Point();
            Point _start = new Point();
            Point _end = new Point();

            // 描画フラグcheck
            if (view == false) {
                return;
            }

            if (e.Button == MouseButtons.Left) { // マウス左ボタン押しながら
                if (isMousePointerInSelectedRect(e)) { // カーソルが矩形内ならドラッグ移動
                    // オフセット値取得
                    var _offsetX = e.X - startForDragge.X;
                    var _offsetY = e.Y - startForDragge.Y;

                    // ドラッグ移動されている矩形を保存しておく
                    var _point = new Point(selected.X + _offsetX, selected.Y + _offsetY);
                    var _size = new Size(selected.Width, selected.Height);
                    limiteToRectangle(ref _point, ref _size);
                    dragged = new Rectangle(_point, _size);

                    // 矩形描画
                    drawRegion(new Rectangle(_point, _size));

                    // ドラッグで移動されている矩形情報表示
                    updateNumericUpDownOnDragged();

                } else if (!isMousePointerInSelectedRect(e)) { // カーソルが矩形外なら矩形範囲選択
                    // カーソルが示している場所の座標を取得
                    _mouseMove.X = e.X;
                    _mouseMove.Y = e.Y;

                    // 座標から(X,Y)座標を計算
                    getRegion(mouseDown, _mouseMove, ref _start, ref _end);

                    // 矩形サイズを計算
                    var _size = new Size();
                    if (widthLimit && !heightLimit) { // リミットに達しているかどうか
                        _size = new Size(sizeOnLimit.Width, getLength(_start.Y, _end.Y));
                    } else if (!widthLimit && heightLimit) {
                        _size = new Size(getLength(_start.X, _end.X), sizeOnLimit.Height);
                    } else if (widthLimit && heightLimit) {
                        _size = new Size(sizeOnLimit.Width, sizeOnLimit.Height);
                    } else {
                        _size = new Size(getLength(_start.X, _end.X), getLength(_start.Y, _end.Y));
                    }

                    // 矩形範囲制限
                    limiteToRectangle(ref _start, ref _size);

                    // 矩形領域を描画
                    drawRegion(new Rectangle(_start, _size));

                    // 選択した矩形情報表示
                    updateNumericUpDownOnSelected();
                }
            } 

            mouseMoveIdx++; // マウスイベントを間引くカウンタ
        }

        /// <summary>
        /// 数値コントロール 矩形Y位置を変更します。
        /// </summary>
        private void numericUpDownTop_Click(object sender, EventArgs e) {
            updateTop();
        }

        private void numericUpDownTop_KeyUp(object sender, KeyEventArgs e) {
            updateTop();
        }

        /// <summary>
        /// 数値コントロール 矩形X位置を変更します。
        /// </summary>
        private void numericUpDownLeft_Click(object sender, EventArgs e) {
            updateLeft();
        }

        private void numericUpDownLeft_KeyUp(object sender, KeyEventArgs e) {
            updateLeft();
        }

        /// <summary>
        /// 数値コントロール 矩形幅を変更します。
        /// </summary>
        private void numericUpDownWidth_Click(object sender, EventArgs e) {
            updateWidth();
        }

        private void numericUpDownWidth_KeyUp(object sender, KeyEventArgs e) {
            updateWidth();
        }

        /// <summary>
        /// 数値コントロール 矩形高さを変更します。
        /// </summary>
        private void numericUpDownHeight_Click(object sender, EventArgs e) {
            updateHeight();
        }

        private void numericUpDownHeight_KeyUp(object sender, KeyEventArgs e) {
            updateHeight();
        }

        /// <summary>
        /// ボタンにドロップされた画像をクリップボードにコピーします。
        /// </summary>
        private void pictureBoxImage_DragDrop(object sender, DragEventArgs e) {
            // DragEnterと同様の判定を行う
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy
                && e.Data.GetDataPresent(DataFormats.FileDrop, true)) {
                // 実際にデータを取り出す
                var _data = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                // データが取得できたか判定する
                if (_data != null) {
                    foreach (var _filePath in _data) { // ※現在は単一ファイルを受ける仕様
                        // FIXME: 単一ファイル対応
                        Bitmap _bmp = new Bitmap(_filePath);
                        Clipboard.SetData(DataFormats.Bitmap, _bmp);
                        _bmp.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// ボタンにドラッグエンターされた時にエフェクトを表示します。
        /// </summary>
        private void pictureBoxImage_DragEnter(object sender, DragEventArgs e) {
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy // 目的の操作(この場合はCopy)ができることと、データの種類を確認する
                && e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy; // Copyのエフェクトを表示する
            } else {
                e.Effect = DragDropEffects.None; // 対応していない場合
            }
        }

        /// <summary>
        /// ボタンにドラッグオーバーされた時にエフェクトを表示します。
        /// </summary>
        private void pictureBoxImage_DragOver(object sender, DragEventArgs e) {
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy // 目的の操作(この場合はCopy)ができることと、データの種類を確認する
                && e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy; // Copyのエフェクトを表示する
            } else {
                e.Effect = DragDropEffects.None; // 対応していない場合
            }
        }

        /// <summary>
        /// フォームにマウスエンターしたら、フォームをアクティブにします。
        /// </summary>
        private void ImageForm_MouseEnter(object sender, EventArgs e) {
            this.Activate();
        }

        /////////////////////////////////////////////////////////////////////////////////
        // パブリック メソッド (パスカルケース: 動詞)

        /// <summary>
        /// 画像描画を更新します。
        /// </summary>
        public void UpdateImage() {
            var _selected = manager.Selected;
            getDrawnSelected(_selected.Location, _selected.Size);
            updateNumericUpDownOnSelected();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // プライベート メソッド (キャメルケース: 動詞)

        /// <summary>
        /// 現在のマウスポインターが選択矩形の中に入っているかどうかを返します。
        /// </summary>
        private bool isMousePointerInSelectedRect(MouseEventArgs e) {
            var _mouseX = e.X;
            var _mouseY = e.Y;
            var _rect = isDragged ? dragged : selected; // 選択されつつある矩形かドラッグ移動されている矩形か判定して返す
            if ((_mouseX > _rect.X && _mouseY > _rect.Y) && (_mouseX < _rect.X + _rect.Width && _mouseY < _rect.Y + _rect.Height)) {
                Cursor.Current = Cursors.Hand;
                return true;
            }
            Cursor.Current = Cursors.Default;
            return false;
        }

        /// <summary>
        /// 選択した矩形を画像の外に出さないように調整します。
        /// </summary>
        private void limiteToRectangle(ref Point point, ref Size size) {
            // 最大幅、最大高さ
            if (size.Width > MAX_WIDTH - 1) {
                size = new Size(MAX_WIDTH, size.Height);
            }
            if (size.Height > MAX_HEIGHT - 1) {
                size = new Size(size.Width, MAX_HEIGHT);
            }
            // X, Y 位置
            if (point.X < 0) {
                point = new Point(0, point.Y);
            }
            if (point.Y < 0) {
                point = new Point(point.X, 0);
            }
            if (point.X + size.Width > (MAX_WIDTH - 1)) { // 右端フラグON
                point = new Point((MAX_WIDTH - 1) - (size.Width), point.Y);
                widthLimit = true;
                sizeOnLimit.Width = size.Width;
            } else { // 右端フラグ解除
                widthLimit = false;
                sizeOnLimit.Width = 0;
            }
            if (point.Y + size.Height > (MAX_HEIGHT - 1)) { // 下端フラグON
                point = new Point(point.X, (MAX_HEIGHT - 1) - (size.Height));
                heightLimit = true;
                sizeOnLimit.Height = size.Height;
            } else { // 下端フラグ解除
                heightLimit = false;
                sizeOnLimit.Height = 0;
            }
        }

        /// <summary>
        /// 長さの絶対値を取得します。
        /// </summary>
        private int getLength(int start, int end) {
            return Math.Abs(start - end);
        }

        /// <summary>
        /// マウスで選択した矩形を取得します。
        /// </summary>
        private void getRegion(Point mouseDown, Point mouseMove, ref Point start, ref Point end) {
            start.X = Math.Min(mouseDown.X, mouseMove.X);
            start.Y = Math.Min(mouseDown.Y, mouseMove.Y);
            end.X = Math.Max(mouseDown.X, mouseMove.X);
            end.Y = Math.Max(mouseDown.Y, mouseMove.Y);

            // 矩形がはみ出さない対処
            if (start.X < 0) {
                start.X = 0;
            }
            if (start.Y < 0) {
                start.Y = 0;
            }
            if (end.X < 0) {
                end.X = 0;
            }
            if (end.Y < 0) {
                end.Y = 0;
            }
            if ((end.X - start.X) > (MAX_WIDTH - 1)) {
                end.X = (MAX_WIDTH - 1) + start.X;
            }
            if ((end.Y - start.Y) > (MAX_HEIGHT - 1)) {
                end.Y = (MAX_HEIGHT - 1) + start.Y;
            }

            // フィールドに現時点での選択範囲を保存する
            Point _point = new Point(start.X, start.Y);
            Size _size = new Size(end.X - start.X, end.Y - start.Y);
            selected = manager.GetAppliedSelected(new Rectangle(_point, _size));
            dragged = selected;
        }

        /// <summary>
        /// 選択矩形を描画します。
        /// </summary>
        private void drawRegion(Rectangle rectangle) {
            if (mouseMoveIdx % 3 != 1) {
                return; // 描画処理は重いのでマウスイベントを間引く
            }

            var _old = bmp;
            bmp = manager.DrawRectangleOnImage(rectangle); // 矩形を画像の上に描画する
            _old.Dispose();

            pictureBoxImage.Image = bmp; // PictureBoxに表示する
        }

        /// <summary>
        /// 数値コントロールを更新します。 矩形選択時
        /// </summary>
        private void updateNumericUpDownOnSelected() {
            numericUpDownTop.Value = selected.Top;
            numericUpDownLeft.Value = selected.Left;
            numericUpDownWidth.Value = (selected.Width + BASE);
            numericUpDownHeight.Value = (selected.Height + BASE);
        }

        /// <summary>
        /// 数値コントロールを更新します。 矩形ドラッグ移動時
        /// </summary>
        private void updateNumericUpDownOnDragged() {
            numericUpDownTop.Value = dragged.Top;
            numericUpDownLeft.Value = dragged.Left;
            numericUpDownWidth.Value = (dragged.Width + BASE);
            numericUpDownHeight.Value = (dragged.Height + BASE);
        }

        /////// move

        /// <summary>
        /// 選択矩形を上に移動します。
        /// </summary>
        private void moveUp(int amount = 1) {
            if (selected.Y < amount) { // amountの移動上限
                amount = selected.Y;
            }
            if (selected.Y == 0) { // 移動上限
                return;
            }
            Point _point = new Point(selected.X, selected.Y - amount);
            Size _size = new Size(selected.Width, selected.Height);
            selected = getDrawnSelected(_point, _size);
            numericUpDownTop.Value = selected.Y;
        }

        /// <summary>
        /// 選択矩形を下に移動します。
        /// </summary>
        private void moveDoun(int amount = 1) {
            if ((selected.Y + selected.Height) + amount > (MAX_HEIGHT - 2)) { // amountの移動下限
                amount = ((MAX_HEIGHT - 2) - (selected.Y + selected.Height)) + 1;
            }
            if ((selected.Y + selected .Height) > (MAX_HEIGHT - 2)) { // 移動下限
                return;
            }
            Point _point = new Point(selected.X, selected.Y + amount);
            Size _size = new Size(selected.Width, selected.Height);
            selected = getDrawnSelected(_point, _size);
            numericUpDownTop.Value = selected.Y;
        }

        /// <summary>
        /// 選択矩形を左に移動します。
        /// </summary>
        private void moveLeft(int amount = 1) {
            if (selected.X < amount) { // amountの移動左限
                amount = selected.X;
            }
            if (selected.X == 0) { // 移動左限
                return;
            }
            Point _point = new Point(selected.X - amount, selected.Y);
            Size _size = new Size(selected.Width, selected.Height);
            selected = getDrawnSelected(_point, _size);
            numericUpDownLeft.Value = selected.X;
        }

        /// <summary>
        /// 選択矩形を右に移動します。
        /// </summary>
        private void moveRight(int amount = 1) {
            if ((selected.X + selected.Width) + amount > (MAX_WIDTH - 2)) { // amountの移動右限
                amount = ((MAX_WIDTH - 2) - (selected.X + selected.Width)) + 1;
            }
            if ((selected.X + selected.Width) > (MAX_WIDTH - 2)) { // 移動右限
                return;
            }
            Point _point = new Point(selected.X + amount, selected.Y);
            Size _size = new Size(selected.Width, selected.Height);
            selected = getDrawnSelected(_point, _size);
            numericUpDownLeft.Value = selected.X;
        }

        /////// scale

        /// <summary>
        /// 選択矩形の幅を拡大します。
        /// </summary>
        private void scaleUpWidth(int amount = 1) {
            if ((selected.X + selected.Width) + amount > (MAX_WIDTH - 2)) { // amountの拡縮右限
                amount = ((MAX_WIDTH - 2) - (selected.X + selected.Width)) + 1;
            }
            if ((selected.X + selected.Width) > (MAX_WIDTH - 2)) { // 拡縮右限
                return;
            }
            Point _point = new Point(selected.X, selected.Y);
            Size _size = new Size(selected.Width + amount, selected.Height);
            selected = getDrawnSelected(_point, _size);
            numericUpDownWidth.Value = (selected.Width + BASE);
        }

        /// <summary>
        /// 選択矩形の幅を縮小します。
        /// </summary>
        private void scaleDownWidth(int amount = 1) {
            if (selected.Width < amount) { // amountの拡縮幅限
                amount = selected.Width - 1;
            }
            if (selected.Width == 1) { // 拡縮幅限
                return;
            }
            Point _point = new Point(selected.X, selected.Y);
            Size _size = new Size(selected.Width - amount, selected.Height);
            selected = getDrawnSelected(_point, _size);
            numericUpDownWidth.Value = (selected.Width + BASE);
        }

        /// <summary>
        /// 選択矩形の高さを拡大します。
        /// </summary>
        private void scaleUpHeight(int amount = 1) {
            if ((selected.Y + selected.Height) + amount > (MAX_HEIGHT - 2)) { // amountの拡縮下限
                amount = ((MAX_HEIGHT - 2) - (selected.Y + selected.Height)) + 1;
            }
            if ((selected.Y + selected.Height) > (MAX_HEIGHT - 2)) { // 拡縮下限
                return;
            }
            Point _point = new Point(selected.X, selected.Y);
            Size _size = new Size(selected.Width, selected.Height + amount);
            selected = getDrawnSelected(_point, _size);
            numericUpDownHeight.Value = (selected.Height + BASE);
        }

        /// <summary>
        /// 選択矩形の高さを縮小します。
        /// </summary>
        private void scaleDownHeight(int amount = 1) {
            if (selected.Height < amount) { // amountの拡縮高さ限
                amount = selected.Height - 1;
            }
            if (selected.Height == 1) { // 拡縮高さ限
                return;
            }
            Point _point = new Point(selected.X, selected.Y);
            Size _size = new Size(selected.Width, selected.Height - amount);
            selected = getDrawnSelected(_point, _size);
            numericUpDownHeight.Value = (selected.Height + BASE);
        }

        /////// update

        /// <summary>
        /// 選択矩形のX位置を変更します。
        /// </summary>
        private void updateTop() {
            Point _point = new Point(selected.X, (int) numericUpDownTop.Value);
            Size _size = new Size(selected.Width, selected.Height);
            selected = getDrawnSelected(_point, _size);
        }

        /// <summary>
        /// 選択矩形のY位置を変更します。
        /// </summary>
        private void updateLeft() {
            Point _point = new Point((int) numericUpDownLeft.Value, selected.Y);
            Size _size = new Size(selected.Width, selected.Height);
            selected = getDrawnSelected(_point, _size);
        }

        /// <summary>
        /// 選択矩形の幅を変更します。
        /// </summary>
        private void updateWidth() {
            Point _point = new Point(selected.X, selected.Y);
            Size _size = new Size(((int) numericUpDownWidth.Value - BASE), selected.Height);
            selected = getDrawnSelected(_point, _size);
        }

        /// <summary>
        /// 選択矩形の高さを変更します。
        /// </summary>
        private void updateHeight() {
            Point _point = new Point(selected.X, selected.Y);
            Size _size = new Size(selected.Width, ((int) numericUpDownHeight.Value - BASE));
            getDrawnSelected(_point, _size);
        }

        /// <summary>
        /// 選択範囲を描画した後にその矩形を返します。
        /// </summary>
        private Rectangle getDrawnSelected(Point _point, Size _size) {
            selected = manager.GetAppliedSelected(new Rectangle(_point, _size));
            initMouseMoveIdx();
            drawRegion(selected);
            dragged = selected;
            return selected;
        }

        /// <summary>
        /// マウス移動カウンタをリセットします。
        /// </summary>
        private void initMouseMoveIdx() {
            mouseMoveIdx = 1;
        }

        /// <summary>
        /// コントロールを初期化します。
        /// </summary>
        private void initializeControl() {
            pictureBoxImage.AllowDrop = true; // ドロップ有効化
        }

        /// <summary>
        /// デバッグ用
        /// </summary>
        private void TRACE(string value) {
            System.Diagnostics.Trace.WriteLine(value);
        }
    }

}
