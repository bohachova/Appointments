using MediatR;

namespace Appointments.BL.Queries
{
    public class CheckTimeSlotDurationQuery : IRequest<int>
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public List<int> AvailableSpecialists { get; set; } = []; 
    }
}
