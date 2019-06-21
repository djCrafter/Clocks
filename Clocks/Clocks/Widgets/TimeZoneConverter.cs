﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Clocks.Widgets
{
    public class TimeZoneConverter : IValueConverter
    {
        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : TimeZoneInfo
                .ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, (TimeZoneInfo)value)
                .ToString("HH:mm:ss dd MMM yy");
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
