using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.ViewModel {
    public class EditDetailsViewModel : ViewModelBase {

        private CalendarEvent _calendarEvent;

        public ICalendarEventRepository CalendarEventRepository { set; get; }

        public CalendarEvent CurrentEvent {
            get => _calendarEvent;
            set => SetProperty(ref _calendarEvent, value);
        }

        public ICommand CommandRemove { get; private set; }
        public ICommand CommandSave { get; private set; }
        public ICommand CommandCancel { get; private set; }

        public EditDetailsViewModel() {
//            CommandSave = new RelayCommand(e => Save());
//            CommandRemove = new RelayCommand(e => CalendarEventRepository.RemoveEvent());
        }
    }
}