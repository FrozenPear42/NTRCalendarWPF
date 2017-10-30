using System;
using System.Windows;
using System.Windows.Input;

namespace NTRCalendarWPF.View
{
    public delegate void OpenWindowDelegate();
    public partial class MainWindow : Window {
        public ICommand ShowWindow { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            ShowWindow = new RelayCommand(e => {
                Console.Out.WriteLine(e);
            });
        }
    }
}