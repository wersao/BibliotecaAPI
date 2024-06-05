using BibliotecaAPI.DAL.Entities;

namespace BibliotecaAPI.Domain.Interfaces
{
    public interface ILibroService
    {
        Task<IEnumerable<Libro>> GetLibrosAsync();
        Task<Libro> GetLibroByIdAsync(Guid id);
        Task<Libro> CambiarEstadoLibro(Guid id);
        Task<Libro> CreateLibroAsync(Libro libro);
        Task<Libro> EditLibroAsync(Libro libro);
        Task<Libro> DeleteLibroAsync(Guid id);
    }
}
