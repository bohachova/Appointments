

namespace Appointments.DataObjects.Requests
{
    public class EditCustomerProfileRequest
    {
        public int CustomerId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
    }
}
