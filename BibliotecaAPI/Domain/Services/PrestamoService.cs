using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;

namespace BibliotecaAPI.Domain.Services
{
    public class PrestamoService : IPrestamoService
    {

        public Task<IEnumerable<Prestamo>> GetPrestamosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Prestamo> GetPrestamoByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Prestamo> CreatePrestamoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Prestamo> EditPrestamoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Prestamo> DeletePrestamoAsync()
        {
            throw new NotImplementedException();
        }

    }
}
