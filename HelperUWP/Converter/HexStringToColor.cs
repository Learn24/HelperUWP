using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace HelperUWP.Converter
{
    public class HexStringToColor
    {
        public static SolidColorBrush ConvertColorFromHexString(string colorHex)
        {
            //Target hex string
            string hexColor = colorHex;
            //Remove # if present
            if (hexColor.IndexOf('#') != -1)
            {
                hexColor = hexColor.Replace("#", "");
            }

            if (hexColor.Length == 6)
            {
                hexColor = "FF" + hexColor;
            }

            //100 % — FF  //50 % — 80
            //95 % — F2  //45 % — 73
            //90 % — E6  //40 % — 66
            //85 % — D9  //30 % — 4D
            //80 % — CC     //25 % — 40
            //75 % — BF  //20 % — 33
            //70 % — B3  //15 % — 26
            //65 % — A6  //10 % — 1A
            //60 % — 99  //5 % — 0D
            //55 % — 8C  //0 % — 00

            byte alpha = 0;
            byte red = 0;
            byte green = 0;
            byte blue = 0;

            if (hexColor.Length == 8)
            {
                //#AARRGGBB
                alpha = byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                red = byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                green = byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                blue = byte.Parse(hexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier);
            }
            var color = Color.FromArgb(alpha, red, green, blue);
            var myBrush = new SolidColorBrush(color);
            return myBrush;
        }
    }
}
