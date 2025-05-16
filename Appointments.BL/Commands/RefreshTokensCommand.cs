

using Appointments.DataObjects.Responses;
using MediatR;

namespace Appointments.BL.Commands
{
    public class RefreshTokensCommand : IRequest<AuthResponse>
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
