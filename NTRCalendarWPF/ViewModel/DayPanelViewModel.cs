using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            set => SetProperty(ref _eventsSource, value);
        }

        public ObservableCollection<CalendarEvent> Events {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public DateTime Day {
            get => _day;
            set {
                Title = value.ToString("dd MMMM");
                SetProperty(ref _day, value);
            }
        }

        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DayPanelViewModel() {
            _events = new ObservableCollection<CalendarEvent>();
        }
    }
}