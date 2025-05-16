using Appointments.DataObjects;
using Appointments.DataObjects.MappedResponses;
using MediatR;

namespace Appointments.BL.Queries
{
    public class GetServiceListQuery : IRequest<IEnumerable<ServiceResponseModel>>
    {
        public int? SpecialistId { get; set; }
        public int? Timing { get; set; }
        public List<int> AvailableSpecialists { get; set; } = [];
    }
}
