using Appointments.DataObjects;
using MediatR;

namespace Appointments.BL.Commands
{
    public class EditCustomerProfileCommand : IRequest<Customer>
    {
        public int CustomerId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
    }
}
