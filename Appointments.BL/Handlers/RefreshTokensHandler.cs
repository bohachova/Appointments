

using Appointments.BL.Abstracts;
using Appointments.BL.Commands;
using Appointments.BL.Security;
using Appointments.DAL;
using Appointments.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class RefreshTokensHandler(AppointmentsDbContext dbContext, IHasher hasher, IRefreshTokenManager refreshTokenManager) 
        : IRequestHandler<RefreshTokensCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var token = await dbContext.Tokens.AsNoTracking().Where(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);
            if (token != null && hasher.Verify(request.RefreshToken, token.RefreshToken) && token.ExpiresAt > DateTime.Now)
            {
                var newRefreshToken = await refreshTokenManager.GetRefreshToken(request.UserId);
                var accessToken = JWTTokenGenerator.GetToken(request.UserId);
                return new AuthResponse { IsSuccess = true, UserId = request.UserId, AccessToken = accessToken, RefreshToken = newRefreshToken };
            }
            return new AuthResponse { IsSuccess = false };
        }
    }
}