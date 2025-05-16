using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Appointments.BL.Handlers
{
    public class GetAllAvailableTimeSlotsHandler(AppointmentsDbContext dbContext, IMediator mediator) 
        : IRequestHandler<GetAllAvailableTimeSlotsQuery, IEnumerable<TimeSlot>>
    {
        public async Task<IEnumerable<TimeSlot>> Handle(GetAllAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var specialists = await dbContext.Specialists.AsNoTracking().Select(x => x.Id).ToListAsync(cancellationToken);
            var timeslots = new List<TimeSlot>();
            foreach (var specialist in specialists)
            {
                var query = new ViewSpecialistsTimeSlotsQuery { Date = request.Date, DayOfWeek = (int)request.DayOfWeek, SpecialistId = specialist };
                var result = await mediator.Send(query);
                timeslots.AddRange(result);
            }
            return timeslots.OrderBy(x => x.Start);
        }
    }
}

