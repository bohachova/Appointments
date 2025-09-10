using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects.Enums;
using Appointments.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class CheckCustomersRegistrationHandler(AppointmentsDbContext dbContext) : IRequestHandler<CheckCustomersRegistrationQuery, RegistrationStatusResponse>
    {
        public async Task<RegistrationStatusResponse> Handle(CheckCustomersRegistrationQuery request, CancellationToken cancellationToken)
        {
            var response = new RegistrationStatusResponse();
            var user = await dbContext.Customers.AsNoTracking().Where(x => x.Phone == request.Phone)
                                                .FirstOrDefaultAsync(cancellationToken);
            if(user != null)
            {
                response.Status = user.Password != null ? RegistrationStatus.Registered : RegistrationStatus.NotCompletedRegistration;
                response.CustomerId = (int)user.Id;
                return response;
            }
            response.Status = RegistrationStatus.NewCustomer;
            return response;
        }
    }
}
