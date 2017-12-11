﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Rhino.Mocks;
using NTRCalendarWPF.Model;
using NTRCalendarWPF.ViewModel;

namespace NTRCalendarWPF.Tests {
    [TestFixture]
    public class Tests {

        private CalendarRepository _dbMock;
        private Person _personMock;
        private ICalendarEventRepository _repoMock;

        [SetUp]
        public void SetUp() {
            _personMock = MockRepository.GenerateMock<Person>();
            _dbMock = MockRepository.GenerateStrictMock<CalendarRepository>();
            _repoMock = MockRepository.GenerateMock<ICalendarEventRepository>();
            _repoMock.Stub(a => a.GetEvents()).Return(new List<UserAppointment>());
        }

        [Test]
        public void ClickingOnBlankSpaceTriggersEditWindow() {
            var windowServiceMock = MockRepository.GenerateMock<IWindowService>();
            var calendarViewModel = new CalendarViewModel();
            var day = DateTime.Today;

            calendarViewModel.WindowService = windowServiceMock;
            calendarViewModel.CommandAddEvent.Execute(day);
            windowServiceMock.AssertWasCalled(e => e.ShowWindow(day));
        }

        [Test]
        public void ClickingOnEventTriggersEditWindow() {
            var windowServiceMock = MockRepository.GenerateMock<IWindowService>();
            var calendarViewModel = new CalendarViewModel();
            var eventMock = MockRepository.GenerateStub<UserAppointment>();
            calendarViewModel.WindowService = windowServiceMock;
            calendarViewModel.CommandEditEvent.Execute(eventMock);
            windowServiceMock.AssertWasCalled(e => e.ShowWindow(eventMock));
        }

        [Test]
        public void ClickingSaveOnNewEventAddsEventToRepository() {
            var calendarRepositoryMock = MockRepository.GenerateMock<ICalendarEventRepository>();
            var editViewModel = new EditDetailsViewModel();
            var day = DateTime.Today;
            editViewModel.CalendarEventRepository = calendarRepositoryMock;
            editViewModel.Day = day;
            editViewModel.OldEvent = null;
            editViewModel.CommandSave.Execute(null);
            calendarRepositoryMock.AssertWasCalled(e => e.AddEvent(editViewModel.CurrentEvent));
        }

        [Test]
        public void ClickingSaveOnExistingEventReplacesEventInRepository() {
            var calendarRepositoryMock = MockRepository.GenerateMock<ICalendarEventRepository>();
            var editViewModel = new EditDetailsViewModel();
            var eventMock = new UserAppointment();
            editViewModel.CalendarEventRepository = calendarRepositoryMock;
            editViewModel.OldEvent = eventMock;
            var newEventMock = editViewModel.CurrentEvent;
            editViewModel.CommandSave.Execute(null);
            calendarRepositoryMock.AssertWasCalled(e => e.ReplaceEvent(eventMock, newEventMock));
        }

        [Test]
        public void ClickingNextButtonShiftsFirstDayBy7Days() {
            var calendarViewModel = new CalendarViewModel();
            var firstDay = calendarViewModel.FirstDay;
            calendarViewModel.EventRepository = _repoMock;
            calendarViewModel.CommandNext.Execute(null);
            Assert.AreEqual(firstDay.AddDays(7), calendarViewModel.FirstDay);
        }

        [Test]
        public void TimePickerTextHoursDontApply() {
            var timepickerViewModel = new TimePickerViewModel();
            var time = timepickerViewModel.Time;
            var hours = timepickerViewModel.Hours;
            timepickerViewModel.Hours = "asd";
            Assert.AreEqual(time, timepickerViewModel.Time);
            Assert.AreEqual(hours, timepickerViewModel.Hours);
        }

        [Test]
        public void DayPanelChangingDayChangesEvents() {
            var dayPanelViewModel = new DayPanelViewModel();
            var day = DateTime.Today;
            var nextDay = day.AddDays(1);
            var evt1 = new UserAppointment {AppointmentDate = day};
            var evt2 = new UserAppointment {AppointmentDate = nextDay};
            dayPanelViewModel.Day = day;
            dayPanelViewModel.EventsSource = new ObservableCollection<UserAppointment> {evt1, evt2};
            Assert.AreEqual(1, dayPanelViewModel.Events.Count);
            Assert.Contains(evt1, dayPanelViewModel.Events);
            dayPanelViewModel.Day = nextDay;
            Assert.AreEqual(1, dayPanelViewModel.Events.Count);
            Assert.Contains(evt2, dayPanelViewModel.Events);
        }

        [Test]
        public void TimePickerParseProperly() {
            var timepickerViewModel = new TimePickerViewModel();
            var time = timepickerViewModel.Time;
            timepickerViewModel.Hours = "22";
            timepickerViewModel.Minutes = "45";

            Assert.AreEqual(22, timepickerViewModel.Time.Hours);
            Assert.AreEqual(45, timepickerViewModel.Time.Minutes);
        }


        [Test]
        public void SavingEventClosesWindow() {
            var editDetailsViewModel = new EditDetailsViewModel();
            var mockCloseAction = MockRepository.GenerateMock<Action>();
            var eventMock = MockRepository.GenerateStub<UserAppointment>();
            var calendarRepositoryMock = _repoMock;
            editDetailsViewModel.CalendarEventRepository = calendarRepositoryMock;
            editDetailsViewModel.CloseAction = mockCloseAction;
            editDetailsViewModel.CommandSave.Execute(eventMock);

            mockCloseAction.AssertWasCalled(e => e.Invoke());
        }

        [Test]
        public void CancelingEditWindowDoesNotAffectRepository() {
            var editDetailsViewModel = new EditDetailsViewModel();
            var calendarRepositoryMock = _repoMock;
            editDetailsViewModel.CalendarEventRepository = calendarRepositoryMock;
            editDetailsViewModel.CommandCancel.Execute(null);
            calendarRepositoryMock.AssertWasNotCalled(e => e.AddEvent(null), a => a.IgnoreArguments());
            calendarRepositoryMock.AssertWasNotCalled(e => e.RemoveEvent(null), a => a.IgnoreArguments());
            calendarRepositoryMock.AssertWasNotCalled(e => e.ReplaceEvent(null, null), a => a.IgnoreArguments());
        }
    }
}