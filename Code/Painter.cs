
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
        public static void ColorTheIcon(Path icon,SolidColorBrush brush)
        {
            icon.Fill= brush;
        }
        
    }
}
