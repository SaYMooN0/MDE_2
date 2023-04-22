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
            SetElementSize(CenterGrid, 0.2, 0.38);
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
            Screen.Background = themeCollection.CurrentTheme.MainBackGround;
            Painter.ColorTheButton(DevelopBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(HistoryBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(SettinsBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(ReturnBTN, themeCollection.CurrentTheme);
        }

        private void DevelopBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            ReturnBTN.Visibility = Visibility.Visible;
            DevelopPage.Visibility = Visibility.Visible;
        }

        private void SettinsBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            ReturnBTN.Visibility = Visibility.Visible;
            SettingsPage.Visibility = Visibility.Visible;
        }

        private void HistoryBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            ReturnBTN.Visibility = Visibility.Visible;
            HistoryPage.Visibility = Visibility.Visible;
        }

        private void ReturnBTN_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Visible;
            ReturnBTN.Visibility = Visibility.Hidden;

            DevelopPage.Visibility = Visibility.Hidden;
            HistoryPage.Visibility = Visibility.Hidden;
            SettingsPage.Visibility = Visibility.Hidden;
        }

        private void ChangeThemeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (themeCollection.CurrentTheme.Name == "Default_Dark")
                themeCollection.SetCurrentTheme("Default_Light");
            else
                themeCollection.SetCurrentTheme("Default_Dark");
            ColorTheElements();
            MessageBox.Show(themeCollection.CurrentTheme.ToString());
        }
    }
}
