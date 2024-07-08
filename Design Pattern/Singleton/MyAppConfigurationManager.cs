namespace WebApi.Design_Pattern.Singleton
{
    public class MyAppConfigurationManager
    {
        private static MyAppConfigurationManager _instance;
        private static readonly object _lock = new object();
        private Dictionary<string, string> settings;

        private MyAppConfigurationManager()
        {
            settings = new Dictionary<string, string>();
            LoadSettings();
        }

        public static MyAppConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MyAppConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private void LoadSettings()
        {
            settings["api_url"] = "https://api/film";
            settings["timeout"] = "1000";
            settings["log_level"] = "DEBUG";
            settings["default_year_filter"] = "2000";
        }

        public string GetSetting(string key)
        {
            if (settings.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }
    }
}
