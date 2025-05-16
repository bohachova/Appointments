using Appointments.BL.Commands;
using Appointments.DAL;
using Appointments.DataObjects;
using Appointments.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class EditCustomerProfileHandler(AppointmentsDbContext dbContext)
        : IRequestHandler<EditCustomerProfileCommand, Customer>
    {
        public async Task<Customer> Handle(EditCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            var customer = await dbContext.Customers.Where(x => x.Id == request.CustomerId).FirstOrDefaultAsync(cancellationToken);
            if(customer != null)
            {
                var changedProperty = typeof(Customer).GetProperties().FirstOrDefault(p => p.Name.Equals(request.PropertyName, StringComparison.OrdinalIgnoreCase));
                changedProperty?.SetValue(customer, request.NewValue);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            return customer;
        }
    }
}
