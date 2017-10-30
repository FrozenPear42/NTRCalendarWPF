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
        public ICommand CommandNext { get; set; }
        public ICommand CommandPrevious { get; set; }
        public ICommand CommandAddEvent { get; set; }
        public ICommand CommandEditEvent { get; set; }

        public ObservableCollection<string> WeekFields { get; set; }
        public ObservableCollection<CalendarEvent> Events { get; set; }

        private DateTime _firstDay;

        public DateTime FirstDay {
            get => _firstDay;
            set => SetProperty(ref _firstDay, value);
        }

        public CalendarViewModel() {
            WeekFields = new ObservableCollection<string> {"", "", "", ""};
            Events = new ObservableCollection<CalendarEvent>();

            Events.Add(new CalendarEvent("Test1", DateTime.Now, DateTime.Now));
            Events.Add(new CalendarEvent("Test2", DateTime.Now, DateTime.Now));

            CommandPrevious = new RelayCommand(e => ChangeWeek(-1));
            CommandNext = new RelayCommand(e => ChangeWeek(1));
            CommandAddEvent = new RelayCommand(e => OpenEditWindow((DateTime) e, null));
            CommandEditEvent = new RelayCommand(e => OpenEditWindow(((CalendarEvent) e).Start.Date, (CalendarEvent) e));


            _firstDay = DateTime.Today;
            while (_firstDay.DayOfWeek != DayOfWeek.Monday) _firstDay = _firstDay.AddDays(-1);
            FirstDay = _firstDay;

            UpdateView();
        }

        private void OpenEditWindow(DateTime day, CalendarEvent calendarEvent) {

            Console.Out.WriteLine(day);
            if(calendarEvent != null)
                Console.Out.WriteLine(calendarEvent);

            var dialog = new EditDetailsWindow();
            dialog.ShowDialog();
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