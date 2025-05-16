

namespace Appointments.DataObjects.Requests
{
    public class TimeSlotsRequest
    {
        public int SpecialistId { get; set; }
        public DateTime Date { get; set; }
        public int? IntervalsCount { get; set; }
        public bool ServiceSelected { get; set; }
        public int? ServiceId { get; set; }
    }
}
