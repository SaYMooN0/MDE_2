
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MDE_2.Code
{
    static class Painter
    {
        public static void ColorTheButton(Button btn, Theme theme)
        {
            btn.Background = theme.MainBackGround;
            btn.Foreground = theme.MainBorder;
            btn.BorderBrush = theme.MainBorder;
        }
        public static void ColorTheIcon(Path icon, SolidColorBrush brush)
        {
            icon.Fill = brush;
        }
        public static void ColorTheTabMenu(TabControl element, Theme theme)
        {
            TabItem tabItem=null;
            try
            {
                
                for (int i = 0; i < element.Items.Count; i++)
                {
                    tabItem = element.Items[i] as TabItem;
                    tabItem.Background= theme.MainBackGround;
                    tabItem.BorderBrush= theme.MainBackGround;
                    tabItem.Foreground= theme.SecondBorder;
                }
                element.Background = theme.MainBackGround;
                element.BorderBrush = theme.MainBackGround;
            }
            catch (System.Exception ex)
            {
                Reporter.Log("Error:  ColorTheMEnuMEthod:\n"+ex.Message);
            }
        }
    }
}
