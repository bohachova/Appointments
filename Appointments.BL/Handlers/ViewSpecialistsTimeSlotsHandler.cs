using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Appointments.BL.Handlers
{
    public class ViewSpecialistsTimeSlotsHandler(AppointmentsDbContext dbContext, IConfiguration configuration): IRequestHandler<ViewSpecialistsTimeSlotsQuery, IEnumerable<TimeSlot>>
    {
        private readonly TimeSpan slotDurationMinutes = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("TimeSlotDurationMinutes").Value));

        public async Task<IEnumerable<TimeSlot>> Handle(ViewSpecialistsTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var scheduleForDay = await dbContext.Schedule.AsNoTracking().Where(x => x.Specialist == request.SpecialistId && x.DayOfTheWeek == request.DayOfWeek)
                                                         .FirstOrDefaultAsync(cancellationToken);
            var serviceTiming = 0;
            if(request.ServiceSelected)
                serviceTiming = await dbContext.ServiceList.Where(x => x.Id == request.ServiceId).Select(x => x.Timing).FirstOrDefaultAsync(cancellationToken);

            if (scheduleForDay != null)
            {
                var freeSlots = new List<TimeSlot>();
                var availableStart = scheduleForDay.StartTime;
                var appointmentsForDay = await dbContext.Appointments.AsNoTracking().Where(x => x.Specialist == request.SpecialistId && x.Date == request.Date)
                                                                                    .OrderBy(x => x.StartTime).ToListAsync(cancellationToken);
                if (appointmentsForDay.Any())
                {
                    // from the start of the day to the first appointment + intervals between appointments 
                    foreach (var appointment in appointmentsForDay)
                    {
                        if (request.ServiceSelected)
                        {
                            while (availableStart.Add(TimeSpan.FromMinutes(serviceTiming)) <= appointment.StartTime)
                            {
                                freeSlots.Add(new TimeSlot { Start = availableStart, SpecialistId = request.SpecialistId });
                                availableStart = availableStart.Add(slotDurationMinutes);
                            }
                        }
                        else
                        {
                            while (availableStart < appointment.StartTime)
                            {
                                freeSlots.Add(new TimeSlot { Start = availableStart, SpecialistId = request.SpecialistId });
                                availableStart = availableStart.Add(slotDurationMinutes);
                            }
                        }
                        availableStart = appointment.EndTime;
                    }

                    // from the last appointment to the end of the day 
                    if (request.ServiceSelected)
                    {
                        while (availableStart.Add(TimeSpan.FromMinutes(serviceTiming)) <= scheduleForDay.EndTime)
                        {
                            freeSlots.Add(new TimeSlot { Start = availableStart, SpecialistId = request.SpecialistId });
                            availableStart = availableStart.Add(slotDurationMinutes);
                        }
                    }
                    else
                    {
                        while (availableStart < scheduleForDay.EndTime)
                        {
                            freeSlots.Add(new TimeSlot { Start = availableStart, SpecialistId = request.SpecialistId });
                            availableStart = availableStart.Add(slotDurationMinutes);
                        }
                    }
                }
                else
                {
                    while (availableStart < scheduleForDay.EndTime)
                    {
                        freeSlots.Add(new TimeSlot { Start = availableStart, SpecialistId = request.SpecialistId });
                        availableStart = availableStart.Add(slotDurationMinutes);
                    }
                }
               
                if(request.Date.Date == DateTime.Now.Date)
                {
                    freeSlots = freeSlots.Where(x => x.Start > DateTime.Now.TimeOfDay).ToList();
                }
                return request.IntervalsCount == null ? freeSlots : freeSlots.Take((int)request.IntervalsCount);
            }

            return [];
        }
    }
}