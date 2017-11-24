using System;
using System.Globalization;
using System.Windows.Data;

namespace NTRCalendarWPF
{
    public class DayDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;
            if (parameter == null) return value;
            var day = (DateTime) value;
            var plus = int.Parse((string) parameter);
            return day.AddDays(plus);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
