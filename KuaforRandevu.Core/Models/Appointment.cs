namespace KuaforRandevu.Core.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid StylistId { get; set; }
        public Stylist Stylist { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ServiceType { get; set; }
    }
}
