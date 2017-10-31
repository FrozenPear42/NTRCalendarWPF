using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF
{
    public class WindowService : IWindowService
    {
        private Action<object> _action;

        public WindowService(Action<object> action)
        {
            _action = action;
        }

        public void ShowWindow(object context)
        {
            _action.Invoke(context);
        }
    }
}