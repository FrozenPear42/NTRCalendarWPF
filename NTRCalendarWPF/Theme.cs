using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NTRCalendarWPF {
    public struct Theme {
        public string Name { get; }
        public string ButtonColor { get; }
        public string LabelColor { get; }
        public string FontColor { get; }
        public string AccentColor { get; }

        public Theme(string name, string buttonColor, string labelColor, string fontColor, string accentColor) {
            Name = name;
            ButtonColor = buttonColor;
            LabelColor = labelColor;
            FontColor = fontColor;
            AccentColor = accentColor;
        }
    }
}