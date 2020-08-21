using System.Linq;
using System.Configuration;

namespace ImageCutToolView {

    public static class AppConfigUtil {

        public static string Read(string key) {
            // プロセス起動時の値を保持しているので、編集処理がある場合は値を再取得する必要あり
            // ConfigurationManager.RefreshSection("appSettings");
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key)) {
                return ConfigurationManager.AppSettings[key];
            }
            return null;
        }

        public static void Write(string key, string value) {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings.AllKeys.Contains(key)) {
                config.AppSettings.Settings[key].Value = value;
            } else {
                config.AppSettings.Settings.Add(key, value);
            }
            config.Save();
        }
    }
}
