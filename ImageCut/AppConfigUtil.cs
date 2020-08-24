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

using System.Linq;
using System.Configuration;

namespace ImageCut {
    /// <summary>
    /// @author h.adachi
    /// </summary>
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
