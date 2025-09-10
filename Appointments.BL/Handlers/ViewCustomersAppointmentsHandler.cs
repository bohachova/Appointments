

using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects;
using Appointments.DataObjects.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class ViewCustomersAppointmentsHandler(AppointmentsDbContext dbContext)
        : IRequestHandler<ViewCustomersAppointmentsQuery, IEnumerable<AppointmentResponseModel>>
    {
        public async Task<IEnumerable<AppointmentResponseModel>> Handle(ViewCustomersAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var records = await dbContext.Appointments.Where(x => x.Customer == request.CustomerId
                                                                              && x.Status == AppointmentStatus.Active
                                                                              && x.Date < DateTime.Now)
                                                                     .ToListAsync(cancellationToken);
            foreach(var record in records)
            {
                record.Status = AppointmentStatus.Archive;
                await dbContext.SaveChangesAsync();
            }

            var appointments = await dbContext.Appointments.AsNoTracking().Where(x => x.Customer == request.CustomerId
                                                                       && x.Status == request.Status)
                                                              .ToListAsync(cancellationToken);

            var result = new List<AppointmentResponseModel>();

            foreach (var appointment in appointments)
            {
                var specialist = await dbContext.Specialists.Where(x => x.Id == appointment.Specialist).Select(x => x.FirstName + ' ' + x.LastName).FirstOrDefaultAsync(cancellationToken);
                var service = await dbContext.ServiceList.Where(x => x.Id == appointment.ServiceCode).Select(x => x.ServiceName).FirstOrDefaultAsync(cancellationToken);
                result.Add(new AppointmentResponseModel { Id = appointment.Id, Specialist = specialist, Service = service, DateTime = DateTime.SpecifyKind(appointment.Date.Add(appointment.StartTime), DateTimeKind.Utc), Price = appointment.Price });
            }

            return result;
        }
    }
}
