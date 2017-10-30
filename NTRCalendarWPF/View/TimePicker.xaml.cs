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

namespace NTRCalendarWPF.View {
    /// <summary>
    /// Logika interakcji dla klasy TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime), typeof(TimePicker),
                new PropertyMetadata(DateTime.Now));

        public DateTime Value {
            get => (DateTime) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public TimePicker() {
            InitializeComponent();
        }
    }
}