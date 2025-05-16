
using Appointments.DataObjects;
using Appointments.DataObjects.Enums;
using MediatR;

namespace Appointments.BL.Queries
{
    public class ViewCustomersAppointmentsQuery : IRequest<IEnumerable<AppointmentResponseModel>>
    {
        public int CustomerId { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
