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

using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageCutLib;

namespace ImageCut {
    /// <summary>
    /// @author h.adachi
    /// </summary>
    public partial class MainForm : Form {

        /////////////////////////////////////////////////////////////////////////////////
        // フィールド (キャメルケース: 名詞、形容詞、bool => is+形容詞、has+過去分詞、can+動詞原型、三単現動詞)

        private Manager manager = null; // 画像切り出しオブジェクト

        private ImageForm imageForm; // 画像表示用フォーム

        private ClipboardWatcher clipboardWatcher; // クリップボード監視オブジェクト

        /////////////////////////////////////////////////////////////////////////////////
        // コンストラクタ

        public MainForm() {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////
        // イベントハンドラ

        /// <summary>
        /// メインフォーム読み込み時に呼ばれます。
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e) {
            manager = Manager.GetInstance(); // manager オブジェクト取得
            clipboardWatcher = new ClipboardWatcher(); // クリップボード監視 開始
            clipboardWatcher.DrawClipboard += (sender2, e2) => {
                if (Clipboard.ContainsImage()) { // クリップボードの画像が更新されたとき
                    showThumbnail(); // サムネイル画像表示
                    if (imageForm != null && imageForm.Visible) {
                        imageForm.UpdateImage(); // 表示中なら切り出しフォームの画像更新
                    }
                    if (radioButtonAuto.Checked) { // 自動保存モードの場合
                        outputFileAtAutoMode(); // 画像ファイル出力
                    }
                }
            };
            loadAppconfig(); // App.config 読み込み

            initializeControl(); // コントロールを初期化
        }

        /// <summary>
        /// メインフォーム終了時に呼ばれます。
        /// </summary>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            clipboardWatcher.Dispose(); // クリップボード監視 解除
            saveAppconfig(); // App.config 書き込み
        }

        /// <summary>
        /// 画像フォームを開きます。
        /// </summary>
        private async void buttonShowForm_Click(object sender, EventArgs e) {
            if (!manager.HasImage) {
                var _defaultBack = toolStripStatusLabelMain.BackColor;
                var _defaultFore = toolStripStatusLabelMain.ForeColor;
                toolStripStatusLabelMain.Text = "画像がありません。";
                toolStripStatusLabelMain.BackColor = Color.Crimson;
                toolStripStatusLabelMain.ForeColor = Color.White;
                await TaskEx.Run(() => Thread.Sleep(1000));
                toolStripStatusLabelMain.Text = "";
                toolStripStatusLabelMain.BackColor = _defaultBack;
                toolStripStatusLabelMain.ForeColor = _defaultFore;
                return; // 画像がない場合
            }
            if (imageForm == null) {
                imageForm = new ImageForm();
                imageForm.Show();
            } else {
                // TODO: 画像フォームを閉じた場合
                if (!imageForm.Visible) {
                    imageForm = new ImageForm(); // FIXME: また new する必要があるのかわからない
                    imageForm.Show();
                }
                imageForm.UpdateImage();
            }
        }

        /// <summary>
        /// 画像ファイルを出力します。
        /// </summary>
        private async void buttonOutputFile_Click(object sender, EventArgs e) {
            if (!manager.HasImage) {
                var _defaultBack = toolStripStatusLabelMain.BackColor;
                var _defaultFore = toolStripStatusLabelMain.ForeColor;
                toolStripStatusLabelMain.Text = "画像がありません。";
                toolStripStatusLabelMain.BackColor = Color.Crimson;
                toolStripStatusLabelMain.ForeColor = Color.White;
                await TaskEx.Run(() => Thread.Sleep(1000));
                toolStripStatusLabelMain.Text = "";
                toolStripStatusLabelMain.BackColor = _defaultBack;
                toolStripStatusLabelMain.ForeColor = _defaultFore;
                return;
            } else {
                toolStripStatusLabelMain.Text = "";
                var _outputFileName = textBoxOutputFile.Text;
                var _outputDir = textBoxOutputDir.Text;
                var _outputImage = manager.RawImage;
                await TaskEx.Run(() => _outputImage.Save(_outputDir + "\\" + _outputFileName, System.Drawing.Imaging.ImageFormat.Png));
                var _default = toolStripStatusLabelMain.BackColor;
                toolStripStatusLabelMain.Text = "画像ファイルを出力しました。";
                toolStripStatusLabelMain.BackColor = Color.Lime;
                await TaskEx.Run(() => Thread.Sleep(1000));
                toolStripStatusLabelMain.Text = "";
                toolStripStatusLabelMain.BackColor = _default;
            }
        }

        /// <summary>
        /// 画像ファイルを出力します。(※選択矩形切り出し)
        /// </summary>
        private async void buttonOutputSelectedFile_Click(object sender, EventArgs e) {
            if (!manager.HasImage) {
                var _defaultBack = toolStripStatusLabelMain.BackColor;
                var _defaultFore = toolStripStatusLabelMain.ForeColor;
                toolStripStatusLabelMain.Text = "画像がありません。";
                toolStripStatusLabelMain.BackColor = Color.Crimson;
                toolStripStatusLabelMain.ForeColor = Color.White;
                await TaskEx.Run(() => Thread.Sleep(1000));
                toolStripStatusLabelMain.Text = "";
                toolStripStatusLabelMain.BackColor = _defaultBack;
                toolStripStatusLabelMain.ForeColor = _defaultFore;
                return;
            } else if (!manager.HasSelected) {
                var _defaultBack = toolStripStatusLabelMain.BackColor;
                var _defaultFore = toolStripStatusLabelMain.ForeColor;
                toolStripStatusLabelMain.Text = "選択範囲がありません。";
                toolStripStatusLabelMain.BackColor = Color.Crimson;
                toolStripStatusLabelMain.ForeColor = Color.White;
                await TaskEx.Run(() => Thread.Sleep(1000));
                toolStripStatusLabelMain.Text = "";
                toolStripStatusLabelMain.BackColor = _defaultBack;
                toolStripStatusLabelMain.ForeColor = _defaultFore;
                return;
            } else {
                toolStripStatusLabelMain.Text = "";
                var _outputFileName = textBoxOutputFile.Text;
                var _outputDir = textBoxOutputDir.Text;
                var _outputImage = manager.SelectedImage;
                await TaskEx.Run(() => _outputImage.Save(_outputDir + "\\" + _outputFileName, System.Drawing.Imaging.ImageFormat.Png));
                var _default = toolStripStatusLabelMain.BackColor;
                toolStripStatusLabelMain.Text = "画像ファイルを出力しました。";
                toolStripStatusLabelMain.BackColor = Color.Lime;
                await TaskEx.Run(() => Thread.Sleep(1000));
                toolStripStatusLabelMain.Text = "";
                toolStripStatusLabelMain.BackColor = _default;
            }
        }

        /// <summary>
        /// 画像出力ファイル名を指定します。
        /// </summary>
        private void buttonReferOutputFile_Click(object sender, EventArgs e) {
            var _dr = openOutputFileDialog.ShowDialog();
            if (_dr == DialogResult.OK) {
                textBoxOutputFile.Text = Path.GetFileName(openOutputFileDialog.FileName);
                textBoxOutputDir.Text = Path.GetDirectoryName(openOutputFileDialog.FileName);
            }
        }

        /// <summary>
        /// 画像出力先フォルダを指定します。
        /// </summary>
        private void buttonReferOutputDir_Click(object sender, EventArgs e) {
            var _dr = outputFolderBrowserDialog.ShowDialog();
            if (_dr == DialogResult.OK) {
                textBoxOutputDir.Text = outputFolderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// リストから選択された設定を読み込みます。
        /// </summary>
        private async void buttonLoadItem_Click(object sender, EventArgs e) {
            if (!manager.HasImage) {
                var _defaultBack = toolStripStatusLabelMain.BackColor;
                var _defaultFore = toolStripStatusLabelMain.ForeColor;
                toolStripStatusLabelMain.Text = "画像がありません。";
                toolStripStatusLabelMain.BackColor = Color.Crimson;
                toolStripStatusLabelMain.ForeColor = Color.White;
                await TaskEx.Run(() => Thread.Sleep(1000));
                toolStripStatusLabelMain.Text = "";
                toolStripStatusLabelMain.BackColor = _defaultBack;
                toolStripStatusLabelMain.ForeColor = _defaultFore;
                return;
            } else {
                var _selected = (string) listBoxItem.SelectedItem;
                var _item = new Item(_selected);

                manager.ApplyItem(_item); // managerオブジェクトに設定を反映する
                if (imageForm != null) {
                    imageForm.UpdateImage(); // 画像フォームを更新する
                }

                updateThumbnail(); // サムネイル画像更新

                // フォーム上のコントロールに表示
                textBoxItemName.Text = _item.Name;
                textBoxOutputFile.Text = _item.OutputName;
                textBoxOutputDir.Text = _item.OutputDir;
            }
        }

        /// <summary>
        /// リストで選択された設定を削除します。
        /// </summary>
        private void buttonDeleteItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show(
                "本当に削除しますか？",
                "確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                ) == DialogResult.No) {
                return;
            }
            int _selected = listBoxItem.SelectedIndex;
            listBoxItem.Items.RemoveAt(_selected);
        }

        /// <summary>
        /// 画像フォームの設定を保存します。
        /// </summary>
        private void buttonAddItem_Click(object sender, EventArgs e) {
            if (manager != null) {
                var _item = manager.Item;
                _item.UpdateName(textBoxItemName.Text);
                _item.UpdateOutputName(textBoxOutputFile.Text);
                _item.UpdateOutputDir(textBoxOutputDir.Text);
                listBoxItem.Items.Add(_item.Body);
            }
        }

        /// <summary>
        /// ボタンにドロップされた画像をクリップボードにコピーします。
        /// </summary>
        private void pictureBoxThumbnail_DragDrop(object sender, DragEventArgs e) {
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
        private void pictureBoxThumbnail_DragEnter(object sender, DragEventArgs e) {
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
        private void pictureBoxThumbnail_DragOver(object sender, DragEventArgs e) {
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
        private void MainForm_MouseEnter(object sender, EventArgs e) {
            this.Activate();
        }

        /////////////////////////////////////////////////////////////////////////////////
        // プライベート メソッド (キャメルケース: 名詞、形容詞)

        /// <summary>
        /// フォーム上のピクチャーBOXにサムネイル画像を表示します。
        /// </summary>
        private void showThumbnail() {
            try {
                manager.Image = Clipboard.GetImage();
                manager.ThumbnailSize = pictureBoxThumbnail.Size;
                pictureBoxThumbnail.Image = manager.ThumbnailImage;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// フォーム上のピクチャーBOXのサムネイル画像を更新します。
        /// </summary>
        private void updateThumbnail() {
            try {
                manager.DrawRectangleOnImage(manager.Selected); // 設定の矩形をメモリ上で画像に描画する
                pictureBoxThumbnail.Image = manager.ThumbnailImage;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// App.config 読み込み
        /// </summary>
        private void loadAppconfig() {
            textBoxOutputFile.Text = AppConfigUtil.Read("outputFile");
            textBoxOutputDir.Text = AppConfigUtil.Read("outputDir");
            setItemList(AppConfigUtil.Read("itemList"));
        }

        /// <summary>
        /// App.config 読み込み
        /// </summary>
        private void saveAppconfig() {
            AppConfigUtil.Write("outputFile", textBoxOutputFile.Text);
            AppConfigUtil.Write("outputDir", textBoxOutputDir.Text);
            AppConfigUtil.Write("itemList", getItemList());
        }

        /// <summary>
        /// 設定をリストBOXにセットする
        /// </summary>
        private void setItemList(string itemList) {
            if (itemList != "") {
                string[] _del = { "\r\n" };
                var _itemListArray = itemList.Split(_del, StringSplitOptions.None);
                foreach (var _itemListBody in _itemListArray) {
                    if (_itemListBody != null) {
                        listBoxItem.Items.Add(_itemListBody);
                    }
                }
            }
        }

        /// <summary>
        /// リストBOXのデータを設定に変換する
        /// </summary>
        private string getItemList() {
            string _itemList = "";
            foreach (var _item in listBoxItem.Items) {
                var _itemString = _item.ToString();
                if (_itemString != "") {
                    _itemList += _itemString + "\r\n"; // 一行ごとに改行を追加
                }
            }
            _itemList = _itemList.TrimEnd(); // 末尾の改行は削除
            return _itemList;
        }

        /// <summary>
        /// 自動保存モードの時に画像ファイルを出力します。
        /// </summary>
        private void outputFileAtAutoMode() {
            if (manager != null && manager.HasImage) {
                toolStripStatusLabelMain.Text = "";
                var _outputFileName = textBoxOutputFile.Text.Replace(".png", "") + "_" +DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                var _outputDir = textBoxOutputDir.Text;
                var _outputImage = manager.RawImage;
                _outputImage.Save(_outputDir + "\\" + _outputFileName, System.Drawing.Imaging.ImageFormat.Png);
                toolStripStatusLabelMain.Text = "画像ファイルを出力しました。";
            } else {
                toolStripStatusLabelMain.Text = "[エラー]出力出来る画像がありません。";
            }
        }

        /// <summary>
        /// コントロールを初期化します。
        /// </summary>
        private void initializeControl() {
            radioButtonNormal.Checked = true;
            listBoxItem.SetSelected(0, true);
            pictureBoxThumbnail.AllowDrop = true; // ドロップ有効化
            toolTipMain.SetToolTip(pictureBoxThumbnail, "ここに画像ファイルをドロップします。");
        }

        /// <summary>
        /// デバッグ用
        /// </summary>
        private void TRACE(string value) {
            System.Diagnostics.Trace.WriteLine(value);
        }
    }

}
