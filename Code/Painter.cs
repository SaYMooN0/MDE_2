using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
    }
}
