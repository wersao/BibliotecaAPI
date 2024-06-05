using BibliotecaAPI.DAL.Entities;

namespace BibliotecaAPI.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync();
        Task<Usuario> CreateUsuarioAsync();
        Task<Usuario> EditUsuarioAsync();
        Task<Usuario> DeleteUsuarioAsync();

    }
}
