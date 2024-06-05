using BibliotecaAPI.DAL.Entities;

namespace BibliotecaAPI.Domain.Interfaces
{
    public interface IPrestamoService
    {
        Task<IEnumerable<Prestamo>> GetPrestamosAsync();
        Task<Prestamo> GetPrestamoByIdAsync(Guid id);
        Task<Prestamo> CreatePrestamoAsync(Prestamo prestamo,   Guid Usuarioid, Guid Libroid);
        Task<Prestamo> EditPrestamoAsync(Prestamo prestamo);
        Task<Prestamo> DeletePrestamoAsync(Guid id);
    }
}
