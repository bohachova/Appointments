

namespace Appointments.DataObjects.Responses
{
    public class AuthResponse : Response
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
