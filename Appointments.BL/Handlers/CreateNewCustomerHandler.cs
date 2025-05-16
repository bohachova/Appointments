

using Appointments.BL.Abstracts;
using Appointments.BL.Commands;
using Appointments.BL.Security;
using Appointments.DAL;
using Appointments.DataObjects;
using Appointments.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Appointments.BL.Handlers
{
    public class CreateNewCustomerHandler(AppointmentsDbContext dbContext, IHasher passwordHasher, IRefreshTokenManager refreshTokenManager)
        : IRequestHandler<CreateNewCustomerCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(CreateNewCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await dbContext.Customers.AsNoTracking().Where(x => x.Phone == request.Phone).FirstOrDefaultAsync(cancellationToken);
            if(existingUser == null)
            {
                if (!request.Password.IsNullOrEmpty())
                {
                    var password = passwordHasher.Hash(request.Password);
                    var customer = new Customer
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Phone = request.Phone,
                        Password = password
                    };
                    await dbContext.Customers.AddAsync(customer);
                    await dbContext.SaveChangesAsync();

                    var userId = await dbContext.Customers.AsNoTracking().Where(x => x.Phone == request.Phone).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);
                    var token = JWTTokenGenerator.GetToken(userId);
                    var refreshToken = await refreshTokenManager.GetRefreshToken(userId);

                    return new AuthResponse { IsSuccess = true, AccessToken = token, RefreshToken = refreshToken, UserId = userId };
                }
                else
                {
                    var customer = new Customer
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Phone = request.Phone
                    };
                    await dbContext.Customers.AddAsync(customer);
                    await dbContext.SaveChangesAsync();
                    return new AuthResponse { IsSuccess = true };
                }
            }
            else 
                return new AuthResponse { IsSuccess = false };
           
        }
    }
}
