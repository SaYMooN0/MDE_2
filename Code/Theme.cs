using System.Windows.Media;

namespace MDE_2.Code
{
    class Theme
    {
        string Name{ get; }
        SolidColorBrush MainBackGround { get; }
        public Theme(string name, string MainBG) {
            Name = name;    
            MainBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(MainBG));
        }
        public override string ToString()
        {
            string str = "Theme: "+Name+"\n";
            str += "Main BackGround Color: " + MainBackGround.Color.ToString();
            return str;
        }
    }
}
