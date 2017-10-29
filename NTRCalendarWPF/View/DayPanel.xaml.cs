using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NTRCalendarWPF.Model;
using NTRCalendarWPF.ViewModel;

namespace NTRCalendarWPF.View {
    public partial class DayPanel : UserControl {
        private DayPanelViewModel _vm;

        public DayPanel() {
            InitializeComponent();
            _vm = (DayPanelViewModel) DayPanelRoot.DataContext;
        }

        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(DateTime), typeof(DayPanel),
                new PropertyMetadata(DateTime.Today, (d, e) => { ((DayPanel) d)._vm.Day = e.NewValue as DateTime? ?? new DateTime(); }));

        public DateTime Day {
            get => (DateTime) GetValue(DayProperty);
            set => SetValue(DayProperty, value);
        }

        public static readonly DependencyProperty EventsSourceProperty =
            DependencyProperty.Register("EventsSource", typeof(ObservableCollection<CalendarEvent>),
                typeof(DayPanel),
                new PropertyMetadata(null,
                    (d, e) => {((DayPanel) d)._vm.EventsSource = e.NewValue as ObservableCollection<CalendarEvent>; }));

        public ObservableCollection<CalendarEvent> EventsSource {
            get =>  (ObservableCollection<CalendarEvent>) GetValue(EventsSourceProperty);
            set => SetValue(EventsSourceProperty, value);
        }
    }
}