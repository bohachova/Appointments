

using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.DataObjects.Security
{
    public class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column("token")]
        public string RefreshToken { get; set; } = string.Empty;
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("expires_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ExpiresAt { get; set; }


    }
}
