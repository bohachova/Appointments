using Appointments.DataObjects;
using MediatR;

namespace Appointments.BL.Queries
{
    public class GetAllAvailableTimeSlotsQuery : IRequest<IEnumerable<TimeSlot>>
    {
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Date { get; set; }
    }
}
