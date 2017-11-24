using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace NTRCalendarWPF.Helpers {
    public class ProductionEnvironmentService : IEnvironmentService {
        
        public IEnumerable<string> GetCommandlineArguments() {
            return Environment.GetCommandLineArgs();     
        }
    }
}