

using Appointments.BL.Queries;
using Appointments.DAL;
using Appointments.DataObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class ViewCustomerAccountHandler(AppointmentsDbContext dbContext) :
        IRequestHandler<ViewCustomerAccountQuery, Customer>
    {
        public async Task<Customer> Handle (ViewCustomerAccountQuery request, CancellationToken cancellationToken)
        {
            var customer = await dbContext.Customers.Where(x => x.Id == request.CustomerId).FirstOrDefaultAsync(cancellationToken);
            customer.Password = "";
            return customer;
        }
    }
}
