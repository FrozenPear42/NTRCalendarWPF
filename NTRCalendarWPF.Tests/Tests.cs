using System;
using NUnit.Framework;
using Rhino.Mocks;
using NTRCalendarWPF.Model;

namespace NTRCalendarWPF.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void RepoShouldAddEvents() {
            var mockRepository = MockRepository.GenerateMock<FileCalendarEventRepository>();
            var newEvent = new CalendarEvent();
            mockRepository.AddEvent(newEvent);
        }
    }
}
