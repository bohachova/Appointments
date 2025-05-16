

using Appointments.DataObjects.Enums;

namespace Appointments.DataObjects.Responses
{
    public class RegistrationStatusResponse
    {
        public RegistrationStatus Status { get; set; }
        public int CustomerId { get; set; }
    }
}
