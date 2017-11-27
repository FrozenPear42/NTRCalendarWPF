using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using NTRCalendarWPF.Helpers;
using NTRCalendarWPF.Helpers.Mock;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.ViewModel {
    public class CalendarViewModel : ViewModelBase {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DateTime _firstDay;
        private bool _isPopupOpen;
        private string _fontName;
        private Theme _theme;

        public ICommand CommandNext { get; set; }
        public ICommand CommandPrevious { get; set; }
        public ICommand CommandAddEvent { get; set; }
        public ICommand CommandEditEvent { get; set; }
        public ICommand CommandTogglePopup { get; set; }
        public IWindowService WindowService { set; private get; }
        public IEnvironmentService EnvironmentService { set; private get; }
        public Action CloseAction { get; set; }
        public Person Person { get; set; }

        public CalendarRepository CalendarRepository { get; set; }
        public ICalendarEventRepository EventRepository { get; set; }
        public ObservableCollection<string> WeekFields { get; set; }
        public ObservableCollection<Appointment> Events { get; set; }

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
            Fonts = new List<string> {"Courier New", "Arial", "Sans Serif"};
            WeekFields = new ObservableCollection<string> {"", "", "", ""};
            FontName = Fonts[0];
            ColorTheme = Themes[0];

            EnvironmentService = new NewUserEnv(new[] {"agruszka", "Andrzej", "Gruszka"});
            CalendarRepository = new CalendarRepository();
            ParseArgs();
            EventRepository = new DBCalendarEventRepository(Person, CalendarRepository);
            //            EventRepository = new FileCalendarEventRepository("asd.dat");


            Events = new ObservableCollection<Appointment>(EventRepository.GetEvents());
            EventRepository.EventRepositoryChanged += () => {
                Events.Clear();
                EventRepository.GetEvents().ForEach(a => Events.Add(a));
            };

            CommandPrevious = new RelayCommand(e => ChangeWeek(-1));
            CommandNext = new RelayCommand(e => ChangeWeek(1));
            CommandAddEvent = new RelayCommand(e => WindowService?.ShowWindow((DateTime) e));
            CommandEditEvent = new RelayCommand(e => WindowService?.ShowWindow((Appointment) e));
            CommandTogglePopup = new RelayCommand(e => IsPopupOpen = !IsPopupOpen);

            var day = DateTime.Today;
            while (day.DayOfWeek != DayOfWeek.Monday) day = day.AddDays(-1);
            FirstDay = day;

            log.InfoFormat("App started today({0}) with first day of week {1} in week {2}", DateTime.Now, _firstDay,
                new GregorianCalendar().GetWeekOfYear(day, CalendarWeekRule.FirstDay, DayOfWeek.Monday));

            UpdateWeeks();
        }

        private void ParseArgs() {
            var args = EnvironmentService.GetCommandlineArguments().ToArray();
            var argsCount = args.Length;

            if (argsCount == 2) {
                var userID = args[1];
                Person = CalendarRepository.GetPersonByUserID(userID);
                if (Person == null) {
                    Console.Out.WriteLine(
                        "UserID not found, use for creation: Calendar <userID> <firstName> <secondName>");
                    log.InfoFormat("UserID {0} not found, exiting", userID);
                    CloseAction?.Invoke();
                }
                log.InfoFormat("Running with user ID: {0}", userID);
            }
            else if (argsCount == 4) {
                var userID = args[1];
                var firstName = args[2];
                var lastName = args[3];
                try {
                    Person = CalendarRepository.AddPerson(firstName, lastName, userID);
                    log.InfoFormat("Created User {1} {2} with UserID: {0}", userID, firstName, lastName);
                }
                catch (Exception) {
                    Console.Out.WriteLine("UserID already exists");
                    log.InfoFormat("Username {0} exists, using it", userID);
                    Person = CalendarRepository.GetPersonByUserID(userID);
                }
            }
            else {
                Console.Out.WriteLine("Usage: Calendar <userID> || Calendar <userID> <firstName> <secondName>");
                log.InfoFormat("Wrong params, exiting");
                CloseAction?.Invoke();
            }
        }

        private void ChangeWeek(int direction) {
            FirstDay = FirstDay.AddDays(7 * direction);
            UpdateWeeks();
        }

        private void UpdateWeeks() {
            var calendar = new GregorianCalendar();
            for (var i = 0; i < 4; ++i) {
                var day = calendar.AddWeeks(_firstDay, i);
                var week = calendar.GetWeekOfYear(day, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                var year = calendar.GetYear(day);
                WeekFields[i] = $"W{week:D2}\n{year}";
            }
        }
    }
}