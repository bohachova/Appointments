

using Appointments.DataObjects;
using MediatR;

namespace Appointments.BL.Queries
{
    public class ViewCustomerAccountQuery : IRequest<Customer>
    {
        public int CustomerId { get; set; } 
    }
}
