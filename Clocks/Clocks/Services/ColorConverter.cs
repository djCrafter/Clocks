using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Text;

namespace Clocks.Services
{
    public static class ColorConverter
    {
        public static string ColorToHex(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            return $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";
        }
    }
}
