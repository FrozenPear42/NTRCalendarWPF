﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Helpers
{
    public interface IDialogService {
        bool ShowDialog(string title, string message);
    }
}