using Appointments.DataObjects;
using MediatR;

namespace Appointments.BL.Queries
{
    public class GetServicePricesQuery : IRequest<IEnumerable<int>>
    {
        public int ServiceId { get; set; }
        public int? SpecialistId { get; set; }
    }
}
