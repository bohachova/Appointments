

using Appointments.BL.Commands;
using Appointments.DAL;
using Appointments.DataObjects.Enums;
using Appointments.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class CancelAppointmentHandler(AppointmentsDbContext dbContext)
        : IRequestHandler<CancelAppointmentCommand, Response>
    {
        public async Task<Response> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await dbContext.Appointments.Where(x => x.Id == request.AppointmentId).FirstOrDefaultAsync(cancellationToken);
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Cancelled;
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            return new Response { IsSuccess = false };
        }
    }
}
