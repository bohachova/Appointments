

using Appointments.DataObjects.Responses;
using MediatR;

namespace Appointments.BL.Commands
{
    public class CustomerAuthorizationCommand : IRequest<AuthResponse>
    {
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
