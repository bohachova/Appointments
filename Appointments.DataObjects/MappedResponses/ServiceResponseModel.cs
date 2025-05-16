

using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.DataObjects.MappedResponses
{
    public class ServiceResponseModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int Timing { get; set; }
        public int? SpecialistId { get; set; }
        public List<int> Prices { get; set; }
    }
}
