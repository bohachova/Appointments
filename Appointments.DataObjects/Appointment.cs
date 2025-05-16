
using Appointments.DataObjects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.DataObjects
{
    public class Appointment
    {
        public int Id { get; set; }
        public int Customer { get; set; }
        public int Specialist { get; set; }
        public int ServiceCode { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public int Price { get; set; }
    }
}
