using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NTRCalendarWPF.Helpers;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.ViewModel {
    public class EditDetailsViewModel : ViewModelBase {
        private Appointment _oldEvent;
        private Appointment _currentEvent;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private bool _isNewEvent;

        public Action CloseAction { get; set; }
        public ICalendarEventRepository CalendarEventRepository { set; get; }
        public IDialogService DialogService { get; set; }
        public DateTime Day { get; set; }

        public TimeSpan StartTime {
            get => _startTime;
            set {
                SetProperty(ref _startTime, value);
                CurrentEvent.StartTime = value;
            }
        }

        public TimeSpan EndTime {
            get => _endTime;
            set {
                SetProperty(ref _endTime, value);
                CurrentEvent.EndTime = value;
            }
        }

        public Appointment CurrentEvent {
            get => _currentEvent;
            set {
                SetProperty(ref _currentEvent, value);
                StartTime = value.StartTime;
                EndTime = value.EndTime;
            }
        }

        public Appointment OldEvent {
            get => _oldEvent;
            set {
                _oldEvent = value;
                IsNewEvent = value == null;
                if (value != null)
                    Day = value.AppointmentDate;
                if (value == null)
                    CurrentEvent = new Appointment {AppointmentDate = Day};
                else
                    CurrentEvent = new Appointment {
                        AppointmentID = value.AppointmentID,
                        Title = value.Title,
                        Description = value.Description,
                        AppointmentDate = value.AppointmentDate,
                        StartTime = value.StartTime,
                        EndTime = value.EndTime,
                    };
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
            CurrentEvent = new Appointment();
            CommandSave = new RelayCommand(e => {
                try {
                    if (IsNewEvent)
                        CalendarEventRepository.AddEvent(CurrentEvent);
                    else
                        CalendarEventRepository.ReplaceEvent(OldEvent, CurrentEvent);
                    CloseAction?.Invoke();
                }
                catch (Exception ex) {
                    DialogService.ShowDialog("Error", ex.Message);
                }
            });
            CommandRemove = new RelayCommand(e => {
                try {
                    CalendarEventRepository.RemoveEvent(OldEvent);
                    CloseAction?.Invoke();
                }
                catch (Exception ex) {
                    DialogService.ShowDialog("Error", ex.Message);
                }
            });
            CommandCancel = new RelayCommand(e => CloseAction?.Invoke());
        }
    }
}