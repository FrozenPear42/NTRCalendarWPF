using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {

    public delegate void RepositoryChangedDelegate();

    public interface ICalendarEventRepository {
        event RepositoryChangedDelegate EventRepositoryChanged;

        bool AddEvent(UserAppointment appointment);
        bool RemoveEvent(UserAppointment appointment);
        bool ReplaceEvent(UserAppointment oldAppointment, UserAppointment newAppointment);
        List<UserAppointment> GetEvents();

    }
}