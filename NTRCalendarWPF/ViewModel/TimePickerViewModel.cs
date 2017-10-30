using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.ViewModel
{
    public class TimePickerViewModel : ViewModelBase {
        private DateTime _time;
        public DateTime Time {
            get => _time;
            set => SetProperty(ref _time, value);
        }
    }
}
