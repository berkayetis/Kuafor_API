
namespace KuaforRandevu.Core.Models
{
    public class Stylist
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
