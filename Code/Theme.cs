using System.IO.Packaging;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace MDE_2.Code
{
    class Theme
    {
        public string Name { get; }
        public string MainBackGroundStr { get; }
        public string MainBorderStr { get; }
        [JsonIgnore]
        public SolidColorBrush MainBackGround => new SolidColorBrush((Color)ColorConverter.ConvertFromString(MainBackGroundStr));
        public SolidColorBrush MainBorder => new SolidColorBrush((Color)ColorConverter.ConvertFromString(MainBorderStr));

        public Theme(string Name, string BackGroundColor, string MainBorderColor)
        {
            this.Name = Name;
            this.MainBackGroundStr = BackGroundColor;
            this.MainBorderStr = MainBorderColor;
        }

        public override string ToString()
        {
            string str = "Theme: " + Name + "\n";
            str += "Main BackGround Color: " + MainBackGroundStr+"\n";
            str += "Main Border Color: " + MainBorderStr+"\n";
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