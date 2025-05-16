
using System.ComponentModel.DataAnnotations;

namespace Appointments.DataObjects
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
    }
}
