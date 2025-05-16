

using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.DataObjects
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int Timing { get; set; }

        [NotMapped]
        public int? SpecialistId { get; set; }
    }
}
