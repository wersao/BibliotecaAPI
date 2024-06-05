using BibliotecaAPI.DAL.Entities;

namespace BibliotecaAPI.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(Guid id);
        Task<Usuario> CambiarEstadoUsuario(Guid id);
        Task<Usuario> CreateUsuarioAsync(Usuario usuario);
        Task<Usuario> EditUsuarioAsync(Usuario usuario);
        Task<Usuario> DeleteUsuarioAsync(Guid id);

    }
}
