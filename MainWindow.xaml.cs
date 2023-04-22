using MDE_2.Code;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MDE_2
{
    public partial class MainWindow : Window
    {
        ThemeCollection themeCollection;
        public MainWindow()
        {
            InitializeComponent();
            WinStarting();
            
        }
        private void WinStarting()
        {
            Reporter.CreateFile();
            Closing +=WinClosing;
            Settings.Deserialize();
            themeCollection = new ThemeCollection();
            Reporter.Log(themeCollection.ToString());
            SizeChanged += PositionAllTheElements;
            ColorTheElements();
        }
        private void WinClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            themeCollection.SerializeThemeList();
            Settings.Serialize();
        }
        private void PositionAllTheElements(object sender, SizeChangedEventArgs e)
        {
            SetElementSize(CenterGrid, 0.4, 0.4);
            SetElementOnPosition(CenterGrid);
            
        }
        private void SetElementOnPosition(FrameworkElement control)
        {
            try
            {
                Canvas.SetLeft(control, (Width-control.Width)/2);
                Canvas.SetTop(control, (Height - control.Height) / 2);
            }
            catch(Exception ex) {
                Reporter.Log("ERROR: SetElement \n" + ex.Message);
            }
        }
        private void SetElementSize(FrameworkElement control, double widthMultiplier, double heightMultiplier)
        {
            try
            {
                control.Width = widthMultiplier*Width;
                control.Height = widthMultiplier* Height;
            }
            catch (Exception ex)
            {
                Reporter.Log("ERROR: SetElementSize \n" + ex.Message);
            }
        }
        private void ColorTheElements()
        {
            MainCanvas.Background = themeCollection.CurrentTheme.MainBackGround;
        }

        private void DevelopBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettinsBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
