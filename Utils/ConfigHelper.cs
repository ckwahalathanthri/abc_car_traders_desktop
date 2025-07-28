using System.Configuration;

namespace ABCCarTraders.Utils
{
    public static class ConfigHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString 
                ?? "Server=localhost;Database=ABCCarTraders;Uid=root;Pwd=your_password;";
        }

        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }
    }
}