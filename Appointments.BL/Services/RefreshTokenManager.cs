

using Appointments.BL.Abstracts;
using Appointments.BL.Security;
using Appointments.DAL;
using Appointments.DataObjects.Security;
using Microsoft.EntityFrameworkCore;

namespace Appointments.BL.Services
{
    public class RefreshTokenManager(AppointmentsDbContext dbContext, IHasher hasher) : IRefreshTokenManager
    {
        public async Task<string> GetRefreshToken (int userId)
        {
            var oldToken = await dbContext.Tokens.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if(oldToken != null)
                dbContext.Tokens.Remove(oldToken);

            var token = RefreshTokenGenerator.GetRefreshToken();
            var hashedToken = hasher.Hash(token);

            await dbContext.Tokens.AddAsync(new Token { UserId = userId, RefreshToken = hashedToken, CreatedAt = DateTime.UtcNow});
            await dbContext.SaveChangesAsync();
            return token;
        }
    }
}

