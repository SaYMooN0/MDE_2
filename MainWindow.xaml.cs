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
            Closing += WinClosing;
            Settings.Deserialize();
            themeCollection = new ThemeCollection();
            Reporter.Log(themeCollection.ToString());
            SizeChanged += UIElementsInstalling;
            ColorTheElements();
        }
        private void WinClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            themeCollection.SerializeThemeList();
            Settings.Serialize();
        }
        private void UIElementsInstalling(object sender, SizeChangedEventArgs e)
        {
            SetElementSize(CenterGrid, 0.3, 0.38);
            SetElementOnPosition(CenterGrid, 0.5, 0);
        }
        private void CenterElement(FrameworkElement control) { SetElementOnPosition(control, 0, 0); }
        private void SetElementOnPosition(FrameworkElement control, double verticalMargin, double gorizontalMargin)
        {
            try
            {
                Canvas.SetLeft(control, (Width - control.Width) / 2 * (gorizontalMargin + 1));
                Canvas.SetTop(control, (Height - control.Height) / 2 * (verticalMargin + 1));

            }
            catch (Exception ex)
            {
                Reporter.Log("ERROR: SetElement \n" + ex.Message);
            }
        }
        private void SetElementSize(FrameworkElement control, double widthMultiplier, double heightMultiplier)
        {
            try
            {
                control.Width = widthMultiplier * Width;
                control.Height = heightMultiplier * Height;

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
