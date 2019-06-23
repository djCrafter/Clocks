using System;
using Xamarin.Forms;

namespace Clocks.Models
{
    public class ClockModel
    {
        public Color HeadColor { get; set; }
        public Color FaceColor { get; set; }
        public TimeZoneInfo ClockTimeZone { get; set; }
    }
}
