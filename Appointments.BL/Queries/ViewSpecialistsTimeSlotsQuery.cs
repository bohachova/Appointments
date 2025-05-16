using Appointments.DataObjects;
using MediatR;

namespace Appointments.BL.Queries
{
    public class ViewSpecialistsTimeSlotsQuery : IRequest<IEnumerable<TimeSlot>>
    {
        public int SpecialistId { get; set; }
        public DateTime Date { get; set; }
        public int DayOfWeek { get; set; }
        public int? IntervalsCount { get; set; }
        public bool ServiceSelected { get; set; }
        public int? ServiceId { get; set; }
    }
}
