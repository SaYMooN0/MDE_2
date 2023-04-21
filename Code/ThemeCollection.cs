
using System.Collections.Generic;

namespace MDE_2.Code
{
    class ThemeCollection
    {
        List<Theme> Themes;
        public ThemeCollection()
        {
            Themes = new List<Theme>();
            Theme DefaultDark = new Theme("Default Dark", "#1E1E1E");
            Themes.Add(DefaultDark);
            Theme DefaultLight = new Theme("Default Light", "#1A1A1A");
            Themes.Add(DefaultLight);
        }
        public override string ToString()
        {
            string str = "Themes Collection:\n";
            if (Themes.Count < 1)
                return "No Themes";
            str += "Count: " + Themes.Count.ToString()+"\n";
            for (int i = 0; i < Themes.Count; i++)
                str +=i.ToString()+") "+ Themes[i].ToString()+"\n";
            return str;
        }
    }
}
