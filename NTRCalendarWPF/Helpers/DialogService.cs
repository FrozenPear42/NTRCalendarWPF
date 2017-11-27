using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Helpers {
    public class DialogService : IDialogService {
        private Action<string, string> _showAction;

        public DialogService(Action<string, string> showAction) {
            _showAction = showAction;
        }

        public bool ShowDialog(string title, string message) {
            _showAction?.Invoke(title, message);
            return true;
        }
    }
}