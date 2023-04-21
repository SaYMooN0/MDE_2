using System.IO.Packaging;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace MDE_2.Code
{
    class Theme
    {
        public string Name { get; }
        public string MainBackGroundColor { get; }
        [JsonIgnore]
        public SolidColorBrush MainBackGround => new SolidColorBrush((Color)ColorConverter.ConvertFromString(MainBackGroundColor));

        public Theme(string Name, string MainBackGroundColor)
        {
            this.Name = Name;
            this.MainBackGroundColor = MainBackGroundColor;
        }

        public override string ToString()
        {
            string str = "Theme: " + Name + "\n";
            str += "Main BackGround Color: " + MainBackGroundColor;
            return str;
        }

        public void SerializeTheme()
        {
            string serializedTheme = JsonConvert.SerializeObject(this, Formatting.Indented);
            string fileName = $"Themes\\{Name}.json";
            System.IO.File.WriteAllText(fileName, serializedTheme);
        }
        static public Theme DeserializeTheme(string filePath)
        {
            
            string jsonTheme = System.IO.File.ReadAllText(filePath);
            Theme deserializedTheme = JsonConvert.DeserializeObject<Theme>(jsonTheme);
            return deserializedTheme;
        }
    }
}