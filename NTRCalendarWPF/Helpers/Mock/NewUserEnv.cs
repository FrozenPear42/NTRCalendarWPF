using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Helpers.Mock
{
    class NewUserEnv : IEnvironmentService {
        private readonly List<string> _args;

        public NewUserEnv(IEnumerable<string> args) {
            _args = new List<string>(args);
            _args.Insert(0, "PATH");
        }

        public IEnumerable<string> GetCommandlineArguments() {
            return _args;
        }
    }
}
