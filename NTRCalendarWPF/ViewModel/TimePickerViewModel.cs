using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NTRCalendarWPF.ViewModel {
    public class TimePickerViewModel : ViewModelBase {
        private TimeSpan _time;
        private TimeSpan _minTime;
        private TimeSpan _maxTime;

        private string _hours;
        private string _minutes;

        public Action<TimeSpan> OnValueChanged;

        public TimeSpan Time {
            get => _time;
            set {
                var oldTime = _time;
                SetProperty(ref _time, value);
                Hours = value.Hours.ToString();
                Minutes = value.Minutes.ToString();
                if (oldTime != value)
                    OnValueChanged?.Invoke(Time);
            }
        }

        public TimeSpan MinTime {
            get => _minTime;
            set => SetProperty(ref _minTime, value);
        }

        public TimeSpan MaxTime {
            get => _maxTime;
            set => SetProperty(ref _maxTime, value);
        }

        public string Hours {
            get => _hours;
            set {
                if (!int.TryParse(value, out int result)) return;
                if (result < 0 || result > 23) return;
                var time = new TimeSpan(result, Time.Minutes, 0);
//                if (TimeSpan.Compare(MinTime, time) > 0 || TimeSpan.Compare(time, MaxTime) > 0) return;
                if (time != _time)
                    Time = time;
                if (value.Length == 1)
                    value = "0" + value;
                SetProperty(ref _hours, value);
            }
        }

        public string Minutes {
            get => _minutes;
            set {
                if (!int.TryParse(value, out int result)) return;
                if (result < 0 || result > 59) return;

                var time = new TimeSpan(Time.Hours, result, 0);
//                if (TimeSpan.Compare(MinTime, time) > 0 || TimeSpan.Compare(time, MaxTime) > 0) return;
                if (time != _time)
                    Time = time;
                if (value.Length == 1)
                    value = "0" + value;
                SetProperty(ref _minutes, value);
            }
        }
    }
}