

using Appointments.DataObjects.Responses;
using MediatR;

namespace Appointments.BL.Commands
{
    public class CancelAppointmentCommand: IRequest<Response>
    {
        public int AppointmentId { get; set; }
    }
}
