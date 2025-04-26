using Core.Interfaces;

namespace KuaforRandevu.Core.Interfaces
{
    /// <summary>
    /// Tüm repository'leri merkezi bir noktada toplayan arayüz ve
    /// veritabanı işlemlerini (SaveChanges) tek yerden yönetir.
    /// </summary>
    public interface IRepositoryManager
    {
        IAppointmentRepository AppointmentRepo { get; }
        IStylistRepository StylistRepo { get; }
        ICustomerRepository CustomerRepo { get; }
        Task SaveAsync();
    }
}
