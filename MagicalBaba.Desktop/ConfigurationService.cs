using System;
using System.IO;
using System.Xml.Serialization;
using MagicalBaba.BaseLibrary;

namespace MagicalBaba.Desktop
{
    class ConfigurationService : IConfigurationService
    {
        private static ConfigurationService _instance;
        private const string SettingsFileName = "settings.xml";
        private const string SettingsBackupFileName = "settings_back.xml";
        private Configuration _configuration;
        private static string _settingsBackFile;
        private static string _settingsFile;

        private ConfigurationService()
        {
            _settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                SettingsFileName);
            _settingsBackFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                SettingsBackupFileName);
            if (!File.Exists(_settingsFile) || !File.Exists(_settingsBackFile))
            {
                //opening the program first time, copy the files
                File.Copy(SettingsFileName, _settingsFile, false);
                File.Copy(SettingsBackupFileName, _settingsBackFile, false);
            }
        }

        public static ConfigurationService Instance
        {
            get { return _instance ?? (_instance = new ConfigurationService()); }
        }

        public Configuration GetConfiguration()
        {
            if (_configuration != null) return _configuration;
            _configuration = LoadConfigurationFromDisk();
            return _configuration;
        }

        public void Reset()
        {
            File.Copy(_settingsBackFile, _settingsFile, true);
            _configuration = LoadConfigurationFromDisk();
        }

        public void SaveConfiguration(Configuration configuration)
        {
            _configuration = configuration;
            var serializer = new XmlSerializer(typeof(Configuration));
            using (TextWriter writer = new StreamWriter(_settingsFile))
            {
                serializer.Serialize(writer, configuration);
            }
        }

        private static Configuration LoadConfigurationFromDisk()
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            using (TextReader reader = new StreamReader(_settingsFile))
            {
                return serializer.Deserialize(reader) as Configuration;
            }
        }
    }
}
