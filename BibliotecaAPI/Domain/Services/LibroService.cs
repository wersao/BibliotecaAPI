using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;

namespace BibliotecaAPI.Domain.Services
{
    public class LibroService : ILibroService
    {
        public Task<IEnumerable<Libro>> GetLibrosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Libro> GetLibroByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Libro> CreateLibroAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Libro> EditLibroAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Libro> DeleteLibroAsync()
        {
            throw new NotImplementedException();
        }

    }
}
