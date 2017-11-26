using System;
using System.Windows;
using System.Windows.Input;
using NTRCalendarWPF.Helpers;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.View {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
//            CalendarViewModel.EnvironmentService = new ProductionEnvironmentService();
            CalendarViewModel.CloseAction = Close;

            CalendarViewModel.WindowService = new WindowService(context => {
                var window = new EditDetailsWindow();
                if (context is DateTime) {
                    window.EditDetailsViewModel.Day = (DateTime) context;
                    window.EditDetailsViewModel.OldEvent = null;
                }
                else if (context is Appointment) {
                    window.EditDetailsViewModel.OldEvent = (Appointment) context;
                }

                window.EditDetailsViewModel.CalendarEventRepository = CalendarViewModel.EventRepository;

                window.ShowDialog();
            });
        }
    }
}