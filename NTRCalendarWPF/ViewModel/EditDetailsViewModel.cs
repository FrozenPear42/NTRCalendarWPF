using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.ViewModel {
    public class EditDetailsViewModel : ViewModelBase {
        private CalendarEvent _oldEvent;
        private CalendarEvent _currentEvent;
        private bool _isNewEvent;

        public Action CloseAction { get; set; }
        public ICalendarEventRepository CalendarEventRepository { set; get; }

        public CalendarEvent CurrentEvent {
            get => _currentEvent;
            set => SetProperty(ref _currentEvent, value);
        }

        public CalendarEvent OldEvent {
            get => _oldEvent;
            set {
                _oldEvent = value;
                IsNewEvent = (value == null);
                CurrentEvent = (value == null)
                    ? new CalendarEvent()
                    : new CalendarEvent(value.Name, value.Start, value.End);
            }
        }

        public bool IsNewEvent {
            get => _isNewEvent;
            private set => SetProperty(ref _isNewEvent, value);
        }

        public ICommand CommandRemove { get; }
        public ICommand CommandSave { get; }
        public ICommand CommandCancel { get; }

        public EditDetailsViewModel() {
            CurrentEvent = new CalendarEvent();
            CommandSave = new RelayCommand(e => {
                if (IsNewEvent)
                    CalendarEventRepository.AddEvent(CurrentEvent);
                else
                    CalendarEventRepository.ReplaceEvent(OldEvent, CurrentEvent);
                CloseAction?.Invoke();
            });
            CommandRemove = new RelayCommand(e => {
                CalendarEventRepository.RemoveEvent(OldEvent);
                CloseAction?.Invoke();
            });
            CommandCancel = new RelayCommand(e => CloseAction?.Invoke());
        }
    }
}