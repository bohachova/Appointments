using Appointments.BL.Queries;
using Appointments.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class ViewSpecialistsWorkingDaysHandler(AppointmentsDbContext dbContext)
        : IRequestHandler<ViewSpecialistsWorkingDaysQuery, IEnumerable<int>>
    {
        public async Task<IEnumerable<int>> Handle(ViewSpecialistsWorkingDaysQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Schedule.AsNoTracking().Where(x => x.Specialist == request.SpecialistId).Select(i => i.DayOfTheWeek).ToListAsync(cancellationToken);
        }
    }
}
