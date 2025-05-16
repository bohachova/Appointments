
using Appointments.DataObjects.Responses;
using MediatR;

namespace Appointments.BL.Queries
{
    public class CheckCustomersRegistrationQuery : IRequest<RegistrationStatusResponse>
    {
        public string Phone { get; set; } = string.Empty;
    }
}
