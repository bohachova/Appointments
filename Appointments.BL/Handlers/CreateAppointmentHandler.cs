

using Appointments.BL.Commands;
using Appointments.DAL;
using Appointments.DataObjects;
using Appointments.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Appointments.DataObjects.Enums;
using Appointments.BL.Queries;

namespace Appointments.BL.Handlers
{
    public class CreateAppointmentHandler(AppointmentsDbContext dbContext, IMediator mediator)
        : IRequestHandler<CreateAppointmentCommand, Response>
    {
        public async Task<Response> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var bookedTime = await dbContext.Appointments.AsNoTracking().Where(x => x.Date == request.Date 
                                                                  && x.Specialist == request.SpecialistId 
                                                                  && x.StartTime == request.Time)
                                                         .FirstOrDefaultAsync(cancellationToken);
            if(bookedTime == null)
            {
                var serviceTiming = await dbContext.ServiceList.AsNoTracking().Where(x => x.Id == request.ServiceId).Select(x => x.Timing).FirstOrDefaultAsync(cancellationToken);
                var query = new GetServicePricesQuery { ServiceId = request.ServiceId, SpecialistId = request.SpecialistId };
                var price = await mediator.Send(query);
                var appointment = new Appointment
                {
                    Customer = request.CustomerId,
                    Date = request.Date,
                    Specialist = request.SpecialistId,
                    ServiceCode = request.ServiceId,
                    StartTime = request.Time,
                    EndTime = request.Time.Add(TimeSpan.FromMinutes(serviceTiming)),
                    Status = AppointmentStatus.Active,
                    Price = price.First()
                };
                await dbContext.Appointments.AddAsync(appointment);
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            return new Response { IsSuccess = false };
        }
    }
}
