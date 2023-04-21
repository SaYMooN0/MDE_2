using MDE_2.Code;
using System;
using System.Windows;

namespace MDE_2
{
    public partial class MainWindow : Window
    {
        ThemeCollection themeCollection;
        public MainWindow()
        {
            InitializeComponent();
            Starting();
        }

       
        private void Starting()
        {
            Reporter.CreateFile();
            Closing += MainWindow_Closing;
            themeCollection = new ThemeCollection();
            Reporter.Log(themeCollection.ToString());
        }
        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            themeCollection.SerializeThemeList();
        }
    }
}
