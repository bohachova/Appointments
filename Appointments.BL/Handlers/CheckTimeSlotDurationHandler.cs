using Appointments.BL.Queries;
using Appointments.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class CheckTimeSlotDurationHandler(AppointmentsDbContext dbContext) 
        : IRequestHandler<CheckTimeSlotDurationQuery, int>
    {
        
        public async Task<int> Handle(CheckTimeSlotDurationQuery request, CancellationToken cancellationToken)
        {

            int maxAvailableDuration = 0;
            foreach (var specialist in request.AvailableSpecialists)
            {
                var interval = new TimeSpan();

                var specialistsEndTime = await dbContext.Schedule.AsNoTracking()
                                                                 .Where(x => x.Specialist == specialist 
                                                                          && x.DayOfTheWeek == (int)request.Date.DayOfWeek)
                                                                 .Select(x => x.EndTime)
                                                                 .FirstOrDefaultAsync(cancellationToken);

                var appointment = await dbContext.Appointments.AsNoTracking()
                                                              .Where(x => x.Date == request.Date 
                                                                       && x.Specialist == specialist 
                                                                       && x.StartTime > request.Time)
                                                              .OrderBy(x => x.StartTime)
                                                              .FirstOrDefaultAsync(cancellationToken);
            
                interval = appointment != null ? appointment.StartTime - request.Time : specialistsEndTime - request.Time;

                if (maxAvailableDuration < interval.TotalMinutes)
                    maxAvailableDuration = (int)interval.TotalMinutes;
            }
            return maxAvailableDuration;
        }
    }
}
