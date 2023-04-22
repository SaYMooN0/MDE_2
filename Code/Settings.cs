using Newtonsoft.Json;
using System.IO;

namespace MDE_2.Code
{
    static class Settings
    {
        static public string ChosenTheme { get; set; }

        private static readonly string SettingsFilePath = "settings.json";

        static public void Serialize()
        {
            string json = JsonConvert.SerializeObject(new { ChosenTheme = ChosenTheme }, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, json);
        }

        static public void Deserialize()
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                var deserializedSettings = JsonConvert.DeserializeObject<dynamic>(json);
                ChosenTheme = deserializedSettings.ChosenTheme;
                Reporter.Log("Settings deserialized successfully");
            }
            else
            {
                ChosenTheme = "Default_Dark";
                Reporter.Log("Error: Seetins.json not found");
            }
        }
    }
}