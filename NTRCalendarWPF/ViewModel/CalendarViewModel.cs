using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using NTRCalendarWPF.Annotations;
using NTRCalendarWPF.Model;
using NTRCalendarWPF.View;

namespace NTRCalendarWPF.ViewModel {
    public class CalendarViewModel : ViewModelBase {
        private DateTime _firstDay;
        private bool _isPopupOpen;
        private string _fontName;
        private Theme _theme;

        public ICommand CommandNext { get; set; }
        public ICommand CommandPrevious { get; set; }
        public ICommand CommandAddEvent { get; set; }
        public ICommand CommandEditEvent { get; set; }
        public ICommand CommandTogglePopup { get; set; }
        public IWindowService WindowService { set; get; }

        public ICalendarEventRepository EventRepository { get; }
        public ObservableCollection<string> WeekFields { get; set; }
        public ObservableCollection<CalendarEvent> Events { get; set; }

        public List<string> Fonts { get; }
        public List<Theme> Themes { get; }

        public string FontName {
            get => _fontName;
            set => SetProperty(ref _fontName, value);
        }

        public Theme ColorTheme {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }

        public DateTime FirstDay {
            get => _firstDay;
            set => SetProperty(ref _firstDay, value);
        }

        public bool IsPopupOpen {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        public CalendarViewModel() {
            Themes = new List<Theme> {
                new Theme("Blue-Green", "#CBE5E5", "#CBCBE8", "#00008f", "#008000", "#009688"),
                new Theme("Pink-Amber", "#FFC107", "#E91E63", "#ffffff", "#FFA000", "#FF5722"),
                new Theme("Blue-Purple", "#448AFF", "#673AB7", "#ffffff", "#7B1FA2", "#E040FB"),
            };
            Fonts = new List<string>{"Courier New", "Arial", "Sans Serif"};
            WeekFields = new ObservableCollection<string> {"", "", "", ""};
            FontName = Fonts[0];
            ColorTheme = Themes[0];

            EventRepository = new FileCalendarEventRepository("calendar.dat");
            EventRepository.EventAdded += calendarEvent => Events.Add(calendarEvent);
            EventRepository.EventRemoved += calendarEvent => Events.Remove(calendarEvent);
            EventRepository.EventReplaced += (oldEvent, newEvent) => {
                if (Events.Remove(oldEvent)) Events.Add(newEvent);
            };

            Events = new ObservableCollection<CalendarEvent>(EventRepository.GetEvents());

            CommandPrevious = new RelayCommand(e => ChangeWeek(-1));
            CommandNext = new RelayCommand(e => ChangeWeek(1));
            CommandAddEvent = new RelayCommand(e => WindowService?.ShowWindow((DateTime)e));
            CommandEditEvent = new RelayCommand(e => WindowService?.ShowWindow((CalendarEvent) e));

            CommandTogglePopup = new RelayCommand(e => IsPopupOpen = !IsPopupOpen);

            var day = DateTime.Today;
            while (day.DayOfWeek != DayOfWeek.Monday) day = day.AddDays(-1);
            FirstDay = day;

            UpdateView();
        }

        private void UpdateView() {
            var calendar = new GregorianCalendar();

            for (var i = 0; i < 4; ++i) {
                var day = calendar.AddWeeks(_firstDay, i);
                var week = calendar.GetWeekOfYear(day, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                var year = calendar.GetYear(day);
                WeekFields[i] = $"W{week:D2}\n{year}";
            }
        }

        private void ChangeWeek(int direction) {
            FirstDay = FirstDay.AddDays(7 * direction);

            UpdateView();
        }
    }
}