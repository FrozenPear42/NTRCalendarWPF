using System;
using System.Collections.Generic;
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
using NTRCalendarWPF.ViewModel;

namespace NTRCalendarWPF.View {
    /// <summary>
    /// Logika interakcji dla klasy TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl {
        private readonly TimePickerViewModel _viewModel;

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(TimeSpan), typeof(TimePicker),  new FrameworkPropertyMetadata(TimeSpan.Zero, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((TimePicker)o)._viewModel.Time = (TimeSpan) args.NewValue));

        public static readonly DependencyProperty MinTimeProperty =
            DependencyProperty.Register("MinTime", typeof(TimeSpan), typeof(TimePicker), new FrameworkPropertyMetadata(TimeSpan.Zero, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((TimePicker)o)._viewModel.MinTime = (TimeSpan)args.NewValue));

        public static readonly DependencyProperty MaxTimeProperty =
            DependencyProperty.Register("MaxTime", typeof(TimeSpan), typeof(TimePicker), new FrameworkPropertyMetadata(TimeSpan.FromDays(1), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((TimePicker)o)._viewModel.MaxTime = (TimeSpan)args.NewValue));

        public TimeSpan Value {
            get => (TimeSpan) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public TimeSpan MinTime {
            get => (TimeSpan) GetValue(MinTimeProperty);
            set => SetValue(MinTimeProperty, value);
        }

        public TimeSpan MaxTime {
            get => (TimeSpan) GetValue(MaxTimeProperty);
            set => SetValue(MaxTimeProperty, value);
        }

        public TimePicker() {
            InitializeComponent();
            _viewModel = (TimePickerViewModel) TimePickerRoot.DataContext;
            _viewModel.OnValueChanged = time => Value = time;
            _viewModel.Time = Value;
            _viewModel.MinTime = MinTime;
            _viewModel.MaxTime = MaxTime;
        }
    }
}