using Appointments.DataObjects.Enums;

namespace Appointments.DataObjects.MappedResponses
{
    public class SpecialistResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Grade Grade { get; set; }
        public int? Price { get; set; }
    }
}
