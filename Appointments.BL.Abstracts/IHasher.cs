

namespace Appointments.BL.Abstracts
{
    public interface IHasher
    {
        string Hash (string password);
        bool Verify (string password, string savedHash);
    }
}
