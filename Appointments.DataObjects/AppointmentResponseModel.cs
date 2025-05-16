namespace Appointments.DataObjects
{
    public class AppointmentResponseModel
    {
        public int Id { get; set; }
        public string Specialist { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty ;
        public DateTime DateTime { get; set; }
        public int Price { get; set; }
    }
}
