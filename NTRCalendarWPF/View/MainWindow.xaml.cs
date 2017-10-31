using System;
using System.Windows;
using System.Windows.Input;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.View {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            CalendarViewModel.WindowService = new WindowService(context => {
                var window = new EditDetailsWindow {
                    EditDetailsViewModel = {
                        OldEvent = (CalendarEvent) context,
                        CalendarEventRepository = CalendarViewModel.EventRepository
                    }
                };
                window.ShowDialog();
            });
        }
    }
}