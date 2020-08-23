using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ImageCut {

    /// <summary>
    /// クリップボードを監視するクラスです。
    /// 使用後は必ずDispose()メソッドを呼び出して下さい。
    /// </summary>
    public class ClipboardWatcher : IDisposable {

        /////////////////////////////////////////////////////////////////////////////////
        // フィールド (キャメルケース: 名詞、形容詞、bool => is+形容詞、has+過去分詞、can+動詞原型、三単現動詞)

        private ClipboardWatcherForm form; // クリップボードを監視すするフォーム

        /////////////////////////////////////////////////////////////////////////////////
        // イベント

        /// <summary>
        /// クリップボードの内容に変更があると発生します。
        /// </summary>
        public event EventHandler DrawClipboard;

        /////////////////////////////////////////////////////////////////////////////////
        // コンストラクタ

        /// <summary>
        /// ClipboardWatcherクラスを初期化して
        /// クリップボードビューアチェインに登録します。
        /// 使用後は必ずDispose()メソッドを呼び出して下さい。
        /// </summary>
        public ClipboardWatcher() {
            form = new ClipboardWatcherForm();
            form.StartWatch(invokeDrawClipboard);
        }

        /////////////////////////////////////////////////////////////////////////////////
        // パブリック メソッド (パスカルケース: 動詞)

        /// <summary>
        /// ClipboardWatcherクラスを
        /// クリップボードビューアチェインから削除します。
        /// </summary>
        public void Dispose() {
            form.Dispose();
        }

        /////////////////////////////////////////////////////////////////////////////////
        // プライベート メソッド (キャメルケース: 動詞)

        /// <summary>
        /// クリップボードの内容変更イベントを実行します。
        /// </summary>
        private void invokeDrawClipboard() {
            DrawClipboard?.Invoke(this, EventArgs.Empty);
        }

        /////////////////////////////////////////////////////////////////////////////////
        // プライベート クラス

        /// <summary>
        /// クリップボードを監視するウインドウ(Form)です。
        /// </summary>
        private class ClipboardWatcherForm : Form {

            [DllImport("user32.dll")]
            private static extern IntPtr SetClipboardViewer(IntPtr hwnd);

            [DllImport("user32.dll")]
            private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            private static extern bool ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNext);

            const int WM_DRAWCLIPBOARD = 0x0308;
            const int WM_CHANGECBCHAIN = 0x030D;

            IntPtr nextHandle;

            ThreadStart proc;

            public void StartWatch(ThreadStart proc) {
                this.proc = proc;
                this.nextHandle = SetClipboardViewer(this.Handle);
            }

            /////////////////////////////////////////////////////////////////////////////////
            // オーバーライド メソッド

            protected override void WndProc(ref Message m) {
                if (m.Msg == WM_DRAWCLIPBOARD) {
                    SendMessage(nextHandle, m.Msg, m.WParam, m.LParam);
                    proc();
                } else if (m.Msg == WM_CHANGECBCHAIN) {
                    if (m.WParam == nextHandle) {
                        nextHandle = m.LParam;
                    } else {
                        SendMessage(nextHandle, m.Msg, m.WParam, m.LParam);
                    }
                }
                base.WndProc(ref m);
            }

            protected override void Dispose(bool disposing) {
                ChangeClipboardChain(this.Handle, nextHandle);
                base.Dispose(disposing);
            }
        }
    }

}
