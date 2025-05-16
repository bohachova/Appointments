

using Appointments.DataObjects.Enums;

namespace Appointments.DataObjects.Requests
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Active;
    }
}
