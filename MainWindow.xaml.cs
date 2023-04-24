using MDE_2.Code;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

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
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                    AdjustWindowSize();
                else
                    Application.Current.MainWindow.DragMove();
        }
        private void BTNWinCross_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        private void BTNWinSquare_Click(object sender, RoutedEventArgs e) { AdjustWindowSize(); }
        private void BTNWinMinus_Click(object sender, RoutedEventArgs e) { this.WindowState = WindowState.Minimized; }
        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                UIElementsInstalling(new object(), null);
            }
            else
                this.WindowState = WindowState.Maximized;
        }
        private void WinStarting()
        {
            var systemBorder = SystemParameters.WindowNonClientFrameThickness;
            this.MaxWidth = SystemParameters.WorkArea.Width + (systemBorder.Left + systemBorder.Right);
            this.MaxHeight = SystemParameters.WorkArea.Height + (systemBorder.Top + systemBorder.Bottom);
            Reporter.CreateFile();
            SizeChanged += UIElementsInstalling;
            StateChanged += UIElementsInstalling;
            UIElementsInstalling(new object(), null);
            Closing += WinClosing;
            Settings.Deserialize();
            themeCollection = new ThemeCollection();
            Reporter.Log(themeCollection.ToString());

            ColorTheElements();
        }

        private void UIElementsInstalling(object? sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                Width = SystemParameters.WorkArea.Width;
                Height = SystemParameters.WorkArea.Height;
                UIElementsInstalling(new object(), null);
            }


        }

        private void WinClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            themeCollection.SerializeThemeList();
            Settings.Serialize();
        }
        private void UIElementsInstalling(object sender, SizeChangedEventArgs e)
        {
            TitleBar.Width= Width-10;
            SetElementSize(MainPage, 1, 1);
            SetElementSize(CenterGrid, 0.24, 0.44);
            SetElementOnPosition(CenterGrid, 0.64, 0);
            SetMarginForElementsInGrid(CenterGrid, 0.1);
            SetButtonBorderSize(DevelopBTN);
            SetButtonFontSize(DevelopBTN,5);
            SetButtonBorderSize(HistoryBTN);
            SetButtonFontSize(HistoryBTN, 5);
            SetButtonBorderSize(SettinsBTN);
            SetButtonFontSize(SettinsBTN,5);
            SetIconsSize(ICN_Develop);
            SetIconsSize(ICN_History);
            SetIconsSize(ICN_Settings);
    
        }
        private void SetElementOnPosition(FrameworkElement control, double verticalMargin, double horizontalMargin)
        {
            try
            {
                Canvas.SetLeft(control, (Width - control.Width) / 2 * (horizontalMargin + 1));
                Canvas.SetTop(control, (Height - control.Height) / 2 * (verticalMargin + 1));

            }
            catch (Exception ex)
            {
                Reporter.Log("ERROR: SetElement \n" + ex.Message);
            }
        }
        private void SetMarginForElementsInGrid(Grid g, double multiplier) { foreach (Control control in g.Children) control.Margin=new Thickness(multiplier*g.Height*0.2); }
        private void SetIconsSize(Path icn)
        {
            FrameworkElement parent = icn.Parent as FrameworkElement;
            parent = parent.Parent as FrameworkElement;
            double size = Math.Min(parent.ActualHeight, parent.ActualWidth/3.2);
            size *= 0.62;
            icn.Height = size;
            icn.Width=size;
           
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
        private void SetButtonBorderSize(Button control) { SetButtonBorderSize(control, 1); }
        private void SetButtonFontSize(Button control, double multiplier) {
            try
            {
                control.FontSize = Math.Min(control.ActualWidth/4.8, control.ActualHeight)*0.155 * multiplier;
            }
            catch
            {
                Reporter.Log($"Error: SetButtonSize for {control.Name} Heght: {control.Height} Width: {control.Width}");
                return;
            }
        }
        private void SetButtonBorderSize(Button control, double Multiplier)
        {
            Thickness thickness = new Thickness(Math.Sqrt(control.ActualHeight * control.ActualWidth) * 0.05 * Multiplier);
            control.BorderThickness = thickness;
        }
        private void ColorTheElements()
        {
            Screen.Background = themeCollection.CurrentTheme.MainBackGround;
            //Background= themeCollection.CurrentTheme.MainBackGround;
            //BorderBrush = themeCollection.CurrentTheme.MainBackGround;
            Painter.ColorTheButton(DevelopBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(HistoryBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(SettinsBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(ReturnBTN, themeCollection.CurrentTheme);
            Painter.ColorTheIcon(ICN_Develop, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICN_History, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICN_Settings, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICNWinCross, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICNWinSquare, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICNWinMinus, themeCollection.CurrentTheme.MainBorder);
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
        }
    }
}
