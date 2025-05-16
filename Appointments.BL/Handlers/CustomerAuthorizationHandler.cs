using Appointments.BL.Abstracts;
using Appointments.BL.Commands;
using Appointments.BL.Security;
using Appointments.DAL;
using Appointments.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Handlers
{
    public class CustomerAuthorizationHandler(AppointmentsDbContext dbContext, IHasher passwordHasher, IRefreshTokenManager refreshTokenManager)
        : IRequestHandler<CustomerAuthorizationCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(CustomerAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var customer = await dbContext.Customers.AsNoTracking().Where(x => x.Phone == request.Phone).FirstOrDefaultAsync(cancellationToken);

            if(customer != null && passwordHasher.Verify(request.Password, customer.Password))
            {
                var token = JWTTokenGenerator.GetToken(customer.Id);
                var refreshToken = await refreshTokenManager.GetRefreshToken(customer.Id);
                return new AuthResponse { IsSuccess = true, AccessToken = token, RefreshToken = refreshToken, UserId = customer.Id };
            }

            return new AuthResponse { IsSuccess = false };
        }
    }
}
