﻿
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
        public ThemeCollection()
        {
            Themes = new List<Theme>();
            DeserializeThemeList();
            if (Themes.Count > 0)
                return;
            Theme DefaultDark = new Theme("Default_Dark", "#1E1E1E");
            Themes.Add(DefaultDark);
            Theme DefaultLight = new Theme("Default_Light", "#1A1A1A");
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
        private void DeserializeThemeList()
        {
            try
            {
                string folderPath = @"Themes";
                string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");
                MessageBox.Show($"Количество файлов с расширением .json: {jsonFiles.Length}");

                foreach (string jsonFile in jsonFiles)
                {

                    string fileName = Path.GetFileName(jsonFile);
                    Themes.Add(Theme.DeserializeTheme(@"Themes\" + fileName));
                    Reporter.Log("Theme deserialised");
                }
            }
            catch (Exception ex)
            {
                Reporter.Log("Error: "+ex.Message);
            }
        }
        
        public void SerializeThemeList()
        {
            foreach (Theme theme in Themes)
            {
                theme.SerializeTheme();
            }
            MessageBox.Show("serialized");
        }
    }
}
