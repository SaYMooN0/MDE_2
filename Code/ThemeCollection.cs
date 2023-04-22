
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;

namespace MDE_2.Code
{
    class ThemeCollection
    {
        List<Theme> Themes;
        public Theme CurrentTheme { get; private set; }
        public ThemeCollection()
        {
            Themes = new List<Theme>();
            DeserializeThemeList();
            if (Themes.Count > 0)
            {
                SetCurrentTheme(Settings.ChosenTheme);
                return;
            }
            Theme DefaultDark = new Theme("Default_Dark", "#1E1E1E");
            Themes.Add(DefaultDark);
            Theme DefaultLight = new Theme("Default_Light", "#1A1A1A");
            Themes.Add(DefaultLight);
            SetCurrentTheme(Settings.ChosenTheme);
        }
        public void SetCurrentTheme(string themeName)
        {
            foreach (var theme in Themes)
            {
                if (theme.Name == themeName)
                {
                    CurrentTheme= theme;
                    return;
                }
            }
            Reporter.Log("ERROR: SetCurrentTheme:\nTheme with this name not found");
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
        private void DeserializeThemeList()
        {
            string folderPath = @"Themes";
            try
            {
                string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");
                foreach (string jsonFile in jsonFiles)
                {
                    string fileName = Path.GetFileName(jsonFile);
                    Themes.Add(Theme.DeserializeTheme(@"Themes\" + fileName));
                }
            }
            catch (Exception ex) { Reporter.Log("Error: "+ex.Message);}
        }
        
        public void SerializeThemeList()
        { 
            foreach (Theme theme in Themes)
                theme.SerializeTheme();
        }
    }
}
