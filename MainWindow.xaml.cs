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
            Starting();
            
        }
        private void Starting()
        {
            Reporter.CreateFile();
            Closing += MainWindow_Closing;
            themeCollection = new ThemeCollection();
            Reporter.Log(themeCollection.ToString());
            SizeChanged += PositionAllTheElements;
        }
        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            themeCollection.SerializeThemeList();
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
    }
}
