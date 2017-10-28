using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NTRCalendarWPF.ViewModel {
    public class CalendarViewModel : INotifyPropertyChanged {
        public CalendarViewModel() {
            CommandPrevious = new RelayCommand(e => ChangeWeek(-1));
            CommandNext = new RelayCommand(e => ChangeWeek(1));
            InitView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CommandNext { get; set; }
        public ICommand CommandPrevious { get; set; }

        private String _weekField1;
        private String _weekField2;
        private String _weekField3;
        private String _weekField4;

        public String WeekField1 {
            get => _weekField1;
            set {
                if (value == _weekField1) return;
                _weekField1 = value;
                NotifyPropertyChanged();
            }
        }

        public String WeekField2 {
            get => _weekField2;
            set {
                if (value == _weekField2) return;
                _weekField2 = value;
                NotifyPropertyChanged();
            }
        }

        public String WeekField3 {
            get => _weekField3;
            set {
                if (value == _weekField3) return;
                _weekField3 = value;
                NotifyPropertyChanged();
            }
        }

        public String WeekField4 {
            get => _weekField4;
            set {
                if (value == _weekField4) return;
                _weekField4 = value;
                NotifyPropertyChanged();
            }
        }

        private void InitView() {
            WeekField1 = "asd";
            WeekField2 = "asd";
            WeekField3 = "asd";
            WeekField4 = "asd";
        }

        private void ChangeWeek(int direction) { }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}