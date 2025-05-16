

using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.DataObjects
{
    public class TimeSlot
    {
        public TimeSpan Start { get; set; }
        public int? SpecialistId { get; set; }
        [NotMapped]
        public DateTime? Date { get; set; }
    }
}
