using BibliotecaAPI.DAL.Entities;

namespace BibliotecaAPI.Domain.Interfaces
{
    public interface IPrestamoService
    {
        Task<IEnumerable<Prestamo>> GetPrestamosAsync();
        Task<Prestamo> GetPrestamoByIdAsync();
        Task<Prestamo> CreatePrestamoAsync();
        Task<Prestamo> EditPrestamoAsync();
        Task<Prestamo> DeletePrestamoAsync();
    }
}
