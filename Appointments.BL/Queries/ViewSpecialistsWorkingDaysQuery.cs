using MediatR;

namespace Appointments.BL.Queries
{
    public class ViewSpecialistsWorkingDaysQuery : IRequest<IEnumerable<int>>
    {
        public int SpecialistId { get; set; }
    }
}
