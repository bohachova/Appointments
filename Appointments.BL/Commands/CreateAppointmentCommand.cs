

using Appointments.DataObjects.Responses;
using MediatR;

namespace Appointments.BL.Commands
{
    public class CreateAppointmentCommand : IRequest<Response>
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int SpecialistId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
