

using Appointments.DataObjects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.DataObjects
{
    public class Price
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Grade? Grade { get; set; }
        [Column("Price")]
        public int PriceValue { get; set; }
    }
}
