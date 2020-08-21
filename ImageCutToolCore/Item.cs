using System;

namespace ImageCutToolCore {

    /// <summary>
    /// 設定を保持するオブジェクトです。
    /// </summary>
    public class Item {

        /////////////////////////////////////////////////////////////////////////////////
        // フィールド (キャメルケース: 名詞、形容詞、bool => is+形容詞、has+過去分詞、can+動詞原型、三単現動詞)

        private string name; // 設定名

        private string outputName; // 出力ファイル名

        private string outputDir; // 出力ディレクトリ

        private int x;

        private int y;

        private int width;

        private int height;

        ///////////////////////////////////////////////////////////////////////////////////////////    
        // コンストラクタ

        /// <summary>
        /// 文字列として連結された設定を必要とします。
        /// </summary>
        public Item(string body) {
            apply(body);
        }

        /////////////////////////////////////////////////////////////////////////////////
        // プロパティ (パスカルケース: 名詞、形容詞、bool => Is+形容詞、Has+過去分詞、Can+動詞原型、三単現動詞)

        public string Name {
            get => name;
        }

        public string OutputName {
            get => outputName;
        }

        public string OutputDir {
            get => outputDir;
        }

        public int X {
            get => x; set => x = value;
        }

        public int Y {
            get => y; set => y = value;
        }

        public int Width {
            get => width; set => width = value;
        }

        public int Height {
            get => height; set => height = value;
        }

        /// <summary>
        /// フィールドをカンマで連結した文字列を返します。
        /// </summary>
        public string Body {
            get => name + "," + x + "," + y + "," + width + "," + height + "," + outputName + "," + outputDir;
        }

        /////////////////////////////////////////////////////////////////////////////////
        // プライベート メソッド (キャメルケース: 動詞)

        /// <summary>
        /// 文字列として連結された設定をフィールドに取り込みます。
        /// </summary>
        private void apply(string body) {
            // ※仕様:名前,X,Y,幅,高さ,ファイル名,ディレクトリ
            if (body != null && body != "") {
                string[] _param = body.Split(','); // カンマで分割して配列化する
                // フィールドに取り込む
                name = _param[0];
                x = Int32.Parse(_param[1]);
                y = Int32.Parse(_param[2]);
                width = Int32.Parse(_param[3]);
                height = Int32.Parse(_param[4]);
                outputName = _param[5];
                outputDir = _param[6];
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        // パブリック メソッド (パスカルケース: 動詞)

        /// <summary>
        /// 設定名を更新します。
        /// </summary>
        public void UpdateName(string value) {
            name = value;
        }

        /// <summary>
        /// 出力ファイル名を更新します。
        /// </summary>
        public void UpdateOutputName(string value) {
            outputName = value;
        }

        /// <summary>
        /// 出力ディレクトリを更新します。
        /// </summary>
        public void UpdateOutputDir(string value) {
            outputDir = value;
        }
    }

}
