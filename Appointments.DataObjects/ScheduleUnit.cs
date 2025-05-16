

namespace Appointments.DataObjects
{
    public class ScheduleUnit
    {
        public int Id { get; set; }
        public int DayOfTheWeek { get; set; }
        public int Specialist { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
