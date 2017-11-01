using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Win32;

namespace NTRCalendarWPF.Model {
    public class FileCalendarEventRepository : ICalendarEventRepository {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string FileName { get; }
        private List<CalendarEvent> CalendarEvents { get; set; }

        public FileCalendarEventRepository(string fileName) {
            CalendarEvents = new List<CalendarEvent>();
            FileName = fileName;
            LoadFromFile();
        }

        public event RepositoryAddRemoveDelegate EventAdded;
        public event RepositoryAddRemoveDelegate EventRemoved;
        public event RepositoryReplaceDelegate EventReplaced;

        public bool AddEvent(CalendarEvent calendarEvent) {
            CalendarEvents.Add(calendarEvent);
            log.InfoFormat("Added event to Repository {0}", calendarEvent);
            SaveFile();
            EventAdded?.Invoke(calendarEvent);
            return true;
        }

        bool ICalendarEventRepository.RemoveEvent(CalendarEvent calendarEvent) {
            if (!CalendarEvents.Remove(calendarEvent)) return false;
            log.InfoFormat("Removed event from Repository {0}", calendarEvent);
            SaveFile();
            EventRemoved?.Invoke(calendarEvent);
            return true;
        }

        public bool ReplaceEvent(CalendarEvent oldEvent, CalendarEvent newEvent) {
            if (!CalendarEvents.Remove(oldEvent)) return false;
            CalendarEvents.Add(newEvent);
            log.InfoFormat("Replaced event {0} with {1}", oldEvent, newEvent);
            SaveFile();
            EventReplaced?.Invoke(oldEvent, newEvent);
            return true;
        }

        public List<CalendarEvent> GetEvents() {
            return CalendarEvents;
        }

        private bool SaveFile() {
            using (var stream = File.Open(FileName, FileMode.Create)) {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, CalendarEvents);
                log.InfoFormat("Saved events to file {0}", FileName);
                return true;
            }
        }

        private bool LoadFromFile() {
            try {
                using (var stream = File.Open(FileName, FileMode.Open)) {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    CalendarEvents = (List<CalendarEvent>) formatter.Deserialize(stream);
                    log.InfoFormat("Loaded {0} events from file {1}", CalendarEvents.Count, FileName);
                    return true;
                }
            }
            catch (Exception) {
                log.WarnFormat("Could not load events from file {0}", FileName);
                return false;
            }
        }
    }
}