using System.Collections.Generic;

namespace NTRCalendarWPF.Helpers {
    public interface IEnvironmentService {
        IEnumerable<string> GetCommandlineArguments();
    }
}