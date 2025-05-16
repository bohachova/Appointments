

namespace Appointments.BL.Abstracts
{
    public interface IRefreshTokenManager
    {
        Task<string> GetRefreshToken(int userId);
        
    }
}
