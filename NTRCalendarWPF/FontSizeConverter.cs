using System;
using System.Globalization;
using System.Windows.Data;

namespace NTRCalendarWPF {
    public class FontSizeConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var w = (double)values[0];
            var h = (double)values[1];

            return (w * h) / double.Parse((string)parameter);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var size = (double?) value;
            return size / 10;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}