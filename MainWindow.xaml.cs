using MDE_2.Code;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MDE_2
{
    public partial class MainWindow : Window
    {
        ThemeCollection themeCollection;
        Size lastSize;
        public MainWindow()
        {
            InitializeComponent();
            WinStarting();

        }
        private void BTNWinCross_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        private void BTNWinSquare_Click(object sender, RoutedEventArgs e) { AdjustWindowSize(); }
        private void BTNWinMinus_Click(object sender, RoutedEventArgs e) { this.WindowState = WindowState.Minimized; }
        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                Height = lastSize.Height;
                Width = lastSize.Width;
            }
            else
            {
                lastSize = new Size(Width, Height);
                this.WindowState = WindowState.Maximized;

            }
            UIElementsInstalling(new object(), null);
        }
        private void WinStarting()
        {
            var systemBorder = SystemParameters.WindowNonClientFrameThickness;
            this.MaxWidth = SystemParameters.WorkArea.Width + (systemBorder.Left + systemBorder.Right);
            this.MaxHeight = SystemParameters.WorkArea.Height + (systemBorder.Top + systemBorder.Bottom);
            Reporter.CreateFile();
            SizeChanged += UIElementsInstalling;
            UIElementsInstalling(new object(), null);
            Closing += WinClosing;
            Settings.Deserialize();
            themeCollection = new ThemeCollection();
            Reporter.Log(themeCollection.ToString());
            ColorTheElements();
            BTNWinMinus.Height = 30;
            BTNWinMinus.Width = 30;
            BTNWinCross.Height = 30;
            BTNWinCross.Width = 30;
            BTNWinSquare.Height = 30;
            BTNWinSquare.Width = 30;
        }
        private void WinClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            themeCollection.SerializeThemeList();
            Settings.Serialize();
        }
        private void UIElementsInstalling(object sender, SizeChangedEventArgs e)
        {
            SetElementSize(MainPage, 1, 1);
            TitleBar.Width = Width;
            SetElementSize(CenterGrid, 0.24, 0.44);
            SetElementOnPosition(CenterGrid, 0.64, 0);
            SetMarginForElementsInGrid(CenterGrid, 0.1);
            SetButtonBorderSize(DevelopBTN);
            SetButtonFontSize(DevelopBTN, 5);
            SetButtonBorderSize(HistoryBTN);
            SetButtonFontSize(HistoryBTN, 5);
            SetButtonBorderSize(SettinsBTN);
            SetButtonFontSize(SettinsBTN, 5);
            SetIconsSize(ICN_Develop);
            SetIconsSize(ICN_History);
            SetIconsSize(ICN_Settings);
            SetSettingsMenuTab();
           

        }
        private void SetSettingsMenuTab()
        {
            double headerWidth=0;
            Canvas content = null;
            TabItem item=null;
            TextBlock header=null;
            try
            {
                for (int i=0;i<SettingsMenuTab.Items.Count;i++) {
                    item = SettingsMenuTab.Items[i] as TabItem;
               
                    content =item.Content  as Canvas;
                    content.Width = Width;
                    content.Height = Height * 0.9;
                    header=item.Header as TextBlock;
                    headerWidth += header.Width;
                    SetElementSize(header, 0.10, 0.05);
                    header.FontSize= Math.Min(header.Width / 4.8, header.Height)*0.9;
                   
                }
            }
            catch (Exception ex) {
                Reporter.Log("Error: SetSettingsMEnuTab:\n" + ex.Message);
            }
            if(SettingsPage.Visibility==Visibility.Visible)
            {

                TitleBar.Width = Width-headerWidth-50;
                Panel.SetZIndex(SettingsMenuTab, -1);
                Panel.SetZIndex(TitleBar, 1);
            }
            SettingsMenuTab.Background=new SolidColorBrush(Colors.White);
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
        private void SetMarginForElementsInGrid(Grid g, double multiplier) { foreach (Control control in g.Children) control.Margin = new Thickness(multiplier * g.Height * 0.2); }
        private void SetIconsSize(Path icn)
        {
            FrameworkElement parent = icn.Parent as FrameworkElement;
            parent = parent.Parent as FrameworkElement;
            double size = Math.Min(parent.ActualHeight, parent.ActualWidth / 3.2);
            size *= 0.62;
            icn.Height = size;
            icn.Width = size;

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
                control.FontSize = Math.Min(control.ActualWidth / 4.8, control.ActualHeight) * 0.155 * multiplier;
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
            Painter.ColorTheButton(DevelopBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(HistoryBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(SettinsBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(ReturnBTN, themeCollection.CurrentTheme);
            Painter.ColorTheButton(BTNWinMinus, themeCollection.CurrentTheme);
            Painter.ColorTheButton(BTNWinCross, themeCollection.CurrentTheme);
            Painter.ColorTheButton(BTNWinSquare, themeCollection.CurrentTheme);
            Painter.ColorTheIcon(ICN_Develop, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICN_History, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICN_Settings, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICNWinCross, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICNWinSquare, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheIcon(ICNWinMinus, themeCollection.CurrentTheme.MainBorder);
            Painter.ColorTheTabMenu(SettingsMenuTab, themeCollection.CurrentTheme);
            var res = Resources.MergedDictionaries[0];
            res["ButtonPressedBackground"] =themeCollection.CurrentTheme.SecondBorder;
            res["ButtonMouseOverBackground"] =themeCollection.CurrentTheme.SecondBorder;
          
          
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
            SetSettingsMenuTab();
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
            TitleBar.Width = Width;
        }

        private void ChangeThemeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (themeCollection.CurrentTheme.Name == "Default_Dark")
                themeCollection.SetCurrentTheme("Default_Light");
            else
                themeCollection.SetCurrentTheme("Default_Dark");
            ColorTheElements();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SettingsMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VisualityTab.Background = Brushes.Transparent;
            DictionaryTab.Background = Brushes.Transparent;
            FilesTab.Background = Brushes.Transparent;
            OtherTab.Background = Brushes.Transparent;

            TabItem selectedItem = (TabItem)SettingsMenuTab.SelectedItem;
            selectedItem.Background = themeCollection.CurrentTheme.SecondBackGround;
        }
    }
}
