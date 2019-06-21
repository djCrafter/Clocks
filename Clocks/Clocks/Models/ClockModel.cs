using System;
using System.Collections.Generic;
using System.Text;
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
