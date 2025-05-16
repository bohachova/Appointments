

namespace Appointments.DataObjects.Requests
{
    public class GetAvailableServicesRequest
    {
        public DateTime DateTime { get; set; }
        public List<int> Specialists { get; set; } = [];
    }
}
