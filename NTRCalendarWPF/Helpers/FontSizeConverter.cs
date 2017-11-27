using System;
using System.Globalization;
using System.Windows.Data;

namespace NTRCalendarWPF.Helpers {
    public class FontSizeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var size = (double) value;
            var factor = double.Parse((string)parameter);
            return size / factor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }
}