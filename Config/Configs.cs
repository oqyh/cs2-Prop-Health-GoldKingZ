using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Localization;

namespace Prop_Health_GoldKingZ.Config
{
    public static class Configs
    {
        public static class Shared {
            public static string? CookiesModule { get; set; }
            public static IStringLocalizer? StringLocalizer { get; set; }
        }
        private static readonly string ConfigDirectoryName = "config";
        private static readonly string ConfigFileName = "config.json";
        private static string? _configFilePath;
        private static ConfigData? _configData;

        private static readonly JsonSerializerOptions SerializationOptions = new()
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            WriteIndented = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };

        public static bool IsLoaded()
        {
            return _configData is not null;
        }

        public static ConfigData GetConfigData()
        {
            if (_configData is null)
            {
                throw new Exception("Config not yet loaded.");
            }
            
            return _configData;
        }

        public static ConfigData Load(string modulePath)
        {
            var configFileDirectory = Path.Combine(modulePath, ConfigDirectoryName);
            if(!Directory.Exists(configFileDirectory))
            {
                Directory.CreateDirectory(configFileDirectory);
            }

            _configFilePath = Path.Combine(configFileDirectory, ConfigFileName);
            if (File.Exists(_configFilePath))
            {
                _configData = JsonSerializer.Deserialize<ConfigData>(File.ReadAllText(_configFilePath), SerializationOptions);
            }
            else
            {
                _configData = new ConfigData();
            }

            if (_configData is null)
            {
                throw new Exception("Failed to load configs.");
            }

            SaveConfigData(_configData);
            
            return _configData;
        }

        private static void SaveConfigData(ConfigData configData)
        {
            if (_configFilePath is null)
            {
                throw new Exception("Config not yet loaded.");
            }
            string json = JsonSerializer.Serialize(configData, SerializationOptions);


            json = System.Text.RegularExpressions.Regex.Unescape(json);
            File.WriteAllText(_configFilePath, json, System.Text.Encoding.UTF8);
        }

        public class ConfigData
        {
            public string empty { get; set; }
            public string Reload_Prop_Settings_Flags { get; set; }
            public string Reload_Prop_Settings_CommandsInGame { get; set; }
            public string Get_Prop_Settings_Flags { get; set; }
            public string Get_Prop_Settings_CommandsInGame { get; set; }
            public string empty1 { get; set; }
            public int Default_Health { get; set; }
            public string Prop_Color_Argb { get; set; }
            public int Prop_Only_TeamXCanDamage { get; set; }
            public string Prop_Damge_Print { get; set; }
            public string empty2 { get; set; }
            public bool EnableDebug { get; set; }
            public string empty3 { get; set; }
            public string Information_For_You_Dont_Delete_it { get; set; }
            
            public ConfigData()
            {
                empty = "----------------------------[ ↓ Main Configs ↓ ]-------------------------------";
                Reload_Prop_Settings_Flags = "@css/root,@css/admin";
                Reload_Prop_Settings_CommandsInGame = "!reloadprop,!reloadprops";
                Get_Prop_Settings_Flags = "@css/root,@css/admin";
                Get_Prop_Settings_CommandsInGame = "!getprop,!getprops";
                empty1 = "----------------------------[ ↓ Prop Configs ↓ ]-------------------------------";
                Default_Health = -1;
                Prop_Color_Argb = "255 0 0 255";
                Prop_Only_TeamXCanDamage = 2;
                Prop_Damge_Print = "1,2:10";
                empty2 = "----------------------------[ ↓ Utilities ↓ ]----------------------------------------------";
                EnableDebug = false;
                empty3 = "----------------------------[ ↓ Info For All Configs Above ↓ ]----------------------------";
                Information_For_You_Dont_Delete_it = " Vist  [https://github.com/oqyh/cs2-Prop-Health-GoldKingZ/tree/main?tab=readme-ov-file#-configuration-] To Understand All Above";
            }
        }
    }
}