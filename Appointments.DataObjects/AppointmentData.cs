

namespace Appointments.DataObjects
{
    public class AppointmentData
    {
        public Customer Customer { get; set; }
        public int ServiceId { get; set; }
        public int SpecialistId { get; set; }
        public DateTime DateTime { get; set; }
    }
}