using BibliotecaAPI.DAL.Entities;

namespace BibliotecaAPI.Domain.Interfaces
{
    public interface ILibroService
    {
        Task<IEnumerable<Libro>> GetLibrosAsync();
        Task<Libro> GetLibroByIdAsync();
        Task<Libro> CreateLibroAsync();
        Task<Libro> EditLibroAsync();
        Task<Libro> DeleteLibroAsync();
    }
}
