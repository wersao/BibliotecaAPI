using BibliotecaAPI.DAL;
using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BibliotecaAPI.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataBaseContext _usuario;

        public UsuarioService(DataBaseContext context)
        {
            _usuario = context;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            try
            {
                var usuarios = await _usuario.Usuarios.ToListAsync();
                return usuarios;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Usuario> GetUsuarioByIdAsync(Guid id)
        {
            try
            {
                var usuario = await _usuario.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
                //Otras dos formas de traerme un objeto desde la BD
                var country1 = await _usuario.Usuarios.FindAsync(id);
                var country2 = await _usuario.Usuarios.FirstAsync(c => c.Id == id);

                return usuario;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Usuario> CambiarEstadoUsuario(Guid id)
        {
            try
            {
                var usuario = await _usuario.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
                if (usuario.EstadoPrestamo == true)
                {
                    usuario.EstadoPrestamo = false;
                    await _usuario.SaveChangesAsync();
                    return usuario;
                }
                if (usuario.EstadoPrestamo == false)
                {
                    usuario.EstadoPrestamo = true;
                    await _usuario.SaveChangesAsync();
                    return usuario;
                }
                else return null;
            }
             catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }

        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            try
            {
                usuario.Id = Guid.NewGuid();
                usuario.CreatedDate = DateTime.Now;
                usuario.EstadoPrestamo = false;
                _usuario.Usuarios.Add(usuario); //El Método Add() me permite crear el objeto en el contexto de mi BD

                await _usuario.SaveChangesAsync(); //Este método me permite guardar el país en mi tabla COUNTRY

                return usuario;

            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Usuario> EditUsuarioAsync(Usuario usuario)
        {
            try
            {
                usuario.ModifiedDate = DateTime.Now;

                _usuario.Usuarios.Update(usuario); //Virtualizo mi objeto
                await _usuario.SaveChangesAsync();

                return usuario;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Usuario> DeleteUsuarioAsync(Guid id)
        {
            try
            {
                var usuario = await GetUsuarioByIdAsync(id);

                if (usuario == null)
                {
                    return null;
                }

                _usuario.Usuarios.Remove(usuario); //Creación del query "Delete from Countries Where Id = @id";
                await _usuario.SaveChangesAsync(); //La ejecución del Query

                return usuario;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

    }
}
