using Appointments.DataObjects.MappedResponses;
using MediatR;

namespace Appointments.BL.Queries
{
    public class ViewSpecialistsQuery : IRequest<IEnumerable<SpecialistResponseModel>>
    {
        public int? ServiceCode { get; set; }
        public List<int> RequestedSpecialists { get; set; } = [];
    }
}
