using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;

namespace BibliotecaAPI.Domain.Services
{
    public class UsuarioService : IUsuarioService
    { 

        public Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuarioByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> CreateUsuarioAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> EditUsuarioAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> DeleteUsuarioAsync()
        {
            throw new NotImplementedException();
        }

    }
}
