using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NTRCalendarWPF.Annotations;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.ViewModel {
    public class DayPanelViewModel : ViewModelBase {
        private ObservableCollection<Appointment> _eventsSource;
        private ObservableCollection<Appointment> _events;
        private DateTime _day;
        private string _title;
        private bool _isToday;

        public ObservableCollection<Appointment> EventsSource {
            get => _eventsSource;
            set {
                SetProperty(ref _eventsSource, value);
                value.CollectionChanged += (sender, args) => { UpdateEvents(); Console.Out.WriteLine("Changed Event source: U");                };
                UpdateEvents();
            }
        }

        public ObservableCollection<Appointment> Events {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public DateTime Day {
            get => _day;
            set {
                SetProperty(ref _day, value);
                Title = value.ToString("dd MMMM");
                IsToday = value.Equals(DateTime.Today);
                UpdateEvents();
            }
        }

        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsToday {
            get => _isToday;
            private set => SetProperty(ref _isToday, value);
        }

        private void UpdateEvents() {
            if (EventsSource != null)
                Events = new ObservableCollection<Appointment>(EventsSource
                    .Where(e => e.AppointmentDate.Equals(Day.Date))
                    .OrderBy(e => e.StartTime));
        }

        public DayPanelViewModel() {
            Events = new ObservableCollection<Appointment>();
        }
    }
}