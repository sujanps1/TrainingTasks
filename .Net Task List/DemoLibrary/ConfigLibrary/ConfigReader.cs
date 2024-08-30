using System;
using System.IO;
using System.Text.Json;

namespace ConfigLibrary
{
    public class ConfigReader
    {
        private  AppConfig _config;

        public ConfigReader()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Configuration file not found.", filePath);
            }
            try
            {
                var json = File.ReadAllText(filePath);
                _config = JsonSerializer.Deserialize<AppConfig>(json);
            }
            catch (Exception)
            {

            }
            

            if (_config == null)
            {
                throw new InvalidOperationException("Error.");
            }
        }

        public string ServerName => _config.ServerName;
        public string ServerPort => _config.ServerPort;
    }

    public class AppConfig
    {
        public string ServerName { get; set; }
        public string ServerPort { get; set; }
    }
}
