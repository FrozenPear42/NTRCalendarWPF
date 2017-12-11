using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using NTRCalendarWPF.Helpers;
using NTRCalendarWPF.Model;
using NTRCalendarWPF.View;

namespace NTRCalendarWPF {
    public partial class App : Application {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            if (AttachConsole(ATTACH_PARENT_PROCESS)) {
                Console.Out.WriteLine("");

                var env = new ProductionEnvironmentService();
                var repo = new CalendarRepository();

                var args = env.GetCommandlineArguments().ToArray();
                var argsCount = args.Length;
                Person person = null;
                if (argsCount == 2) {
                    var userID = args[1];
                    person = repo.GetPersonByUserID(userID);
                    if (person == null) {
                        Console.Out.WriteLine(
                            "UserID not found\nUse: Calendar <userID> <firstName> <secondName> to create");
                        log.InfoFormat("UserID {0} not found, exiting", userID);
                        FreeConsole();
                        Shutdown();
                    }
                    log.InfoFormat("Running with user ID: {0}", userID);
                }
                else if (argsCount == 4) {
                    var userID = args[1];
                    var firstName = args[2];
                    var lastName = args[3];
                    try {
                        person = repo.AddPerson(firstName, lastName, userID);
                        log.InfoFormat("Created User {1} {2} with UserID: {0}", userID, firstName, lastName);
                    }
                    catch (Exception) {
                        log.InfoFormat("Username {0} exists, using it", userID);
                        person = repo.GetPersonByUserID(userID);
                    }
                }
                else {
                    Console.Out.WriteLine("Usage: Calendar <userID> || Calendar <userID> <firstName> <secondName>");
                    log.InfoFormat("Wrong params, exiting");
                    FreeConsole();
                    Shutdown();
                }

                Console.Out.WriteLine("");
                FreeConsole();
                var window = new MainWindow {
                    CalendarViewModel = {EventRepository = new DBCalendarEventRepository(person, repo), Person = person}
                };
                window.Show();
            }
            else {
                Shutdown();
            }
        }

        private const int ATTACH_PARENT_PROCESS = -1;

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}