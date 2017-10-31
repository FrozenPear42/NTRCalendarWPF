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

        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(DateTime), typeof(DayPanel),
                new PropertyMetadata(DateTime.MinValue,
                    (d, e) => { ((DayPanel) d)._vm.Day = e.NewValue as DateTime? ?? DateTime.MinValue; }));

        public static readonly DependencyProperty EventsSourceProperty =
            DependencyProperty.Register("EventsSource", typeof(ObservableCollection<CalendarEvent>),
                typeof(DayPanel),
                new PropertyMetadata(null,
                    (d, e) => {
                        ((DayPanel) d)._vm.EventsSource = e.NewValue as ObservableCollection<CalendarEvent>;
                    }));

        public static readonly DependencyProperty EditEventProperty =
            DependencyProperty.Register("EditEvent", typeof(ICommand), typeof(DayPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty AddEventProperty =
            DependencyProperty.Register("AddEvent", typeof(ICommand), typeof(DayPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty AccentProperty =
            DependencyProperty.Register("Accent", typeof(Brush), typeof(DayPanel), new PropertyMetadata(null));

        public ObservableCollection<CalendarEvent> EventsSource {
            get => (ObservableCollection<CalendarEvent>) GetValue(EventsSourceProperty);
            set => SetValue(EventsSourceProperty, value);
        }

        public DateTime Day {
            get => (DateTime) GetValue(DayProperty);
            set => SetValue(DayProperty, value);
        }

        public ICommand EditEvent {
            get => (ICommand) GetValue(EditEventProperty);
            set => SetValue(EditEventProperty, value);
        }

        public ICommand AddEvent {
            get => (ICommand) GetValue(AddEventProperty);
            set => SetValue(AddEventProperty, value);
        }

        public Brush Accent {
            get => (Brush) GetValue(AccentProperty);
            set => SetValue(AccentProperty, value);
        }

        public DayPanel() {
            InitializeComponent();
            _vm = (DayPanelViewModel) DayPanelRoot.DataContext;
        }
    }
}