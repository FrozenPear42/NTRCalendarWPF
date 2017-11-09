using System;
using System.Globalization;
using System.Windows.Data;

namespace NTRCalendarWPF {
    public class FontSizeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var size = (double?) value;
            return size / 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}