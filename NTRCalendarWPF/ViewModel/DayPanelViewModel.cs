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
        private ObservableCollection<CalendarEvent> _eventsSource;
        private ObservableCollection<CalendarEvent> _events;
        private DateTime _day;
        private string _title;

        public ObservableCollection<CalendarEvent> EventsSource {
            get => _eventsSource;
            set {
                SetProperty(ref _eventsSource, value);
                UpdateEvents();
            }
        }

        public ObservableCollection<CalendarEvent> Events {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public DateTime Day {
            get => _day;
            set {
                SetProperty(ref _day, value);
                Title = value.ToString("dd MMMM");
                UpdateEvents();
            }
        }

        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private void UpdateEvents() {
            if (EventsSource != null)
                Events = new ObservableCollection<CalendarEvent>(EventsSource.Where(e => e.Start.Date.Equals(Day.Date))
                    .OrderBy(e => e.Start));
        }

        public DayPanelViewModel() {
            Events = new ObservableCollection<CalendarEvent>();
        }

    }
}