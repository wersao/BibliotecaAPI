using BibliotecaAPI.DAL;
using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BibliotecaAPI.Domain.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly DataBaseContext _prestamo;
        public PrestamoService(DataBaseContext context)
        {
            _prestamo = context;
        }
        public async Task<IEnumerable<Prestamo>> GetPrestamosAsync()
        {
            try
            {
                var prestamos = await _prestamo.Prestamos.ToListAsync();
                return prestamos;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
        public async Task<Prestamo> CreatePrestamoAsync(Prestamo prestamo, Guid Usuarioid, Guid Libroid)
        {
            try
            {                
                var usuario = await _prestamo.Usuarios.FirstOrDefaultAsync(u => u.Id == Usuarioid);
                var libro = await _prestamo.Libros.FirstOrDefaultAsync(l => l.Id == Libroid);
                prestamo.EstadoLibro = !libro.EstadoPrestamo;
                prestamo.EstadoUsuario = !usuario.EstadoPrestamo;
                if (usuario.EstadoPrestamo == false && libro.EstadoPrestamo == false)
                {
                    prestamo.Id = Guid.NewGuid();
                    prestamo.CreatedDate = DateTime.Now;
                    prestamo.FechaPrestamo = DateTime.Now;
                    prestamo.IdLibro = Libroid; 
                    prestamo.IdUsuario= Usuarioid;
                    usuario.EstadoPrestamo = true;
                    libro.EstadoPrestamo = true;
                    prestamo.EstadoLibro = true;
                    prestamo.EstadoLibro = true;
                    _prestamo.Prestamos.Add(prestamo); 
                    await _prestamo.SaveChangesAsync();
                    return prestamo;
                }
                else return prestamo;


            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Prestamo> DevolverPrestamoAsync(Guid guid)
        {
            try
            {
                var prestamo = await _prestamo.Prestamos.FirstOrDefaultAsync(c => c.Id == guid);
                if (prestamo == null)
                {
                    return null;
                }
                else { 
                var usuario = await _prestamo.Usuarios.FirstOrDefaultAsync(u => u.Id == prestamo.IdUsuario);
                var libro = await _prestamo.Libros.FirstOrDefaultAsync(l => l.Id == prestamo.IdLibro);
                if (usuario.EstadoPrestamo == true && libro.EstadoPrestamo == true)
                {
                    usuario.EstadoPrestamo = false;
                    libro.EstadoPrestamo = false;
                    prestamo.EstadoLibro = false;
                    prestamo.EstadoUsuario = false;
                    prestamo.EstadoPrestamo = false;
                    await _prestamo.SaveChangesAsync();
                    return prestamo;
                }
                    return prestamo;
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Prestamo> DeletePrestamoAsync(Guid id)
        {
            try
            {
                var prestamo = await GetPrestamoByIdAsync(id);

                if (prestamo == null)
                {
                    return null;
                }

                _prestamo.Prestamos.Remove(prestamo); 
                await _prestamo.SaveChangesAsync(); 

                return prestamo;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Prestamo> EditPrestamoAsync(Prestamo prestamo)
        {
            try
            {
                prestamo.ModifiedDate = DateTime.Now;

                _prestamo.Prestamos.Update(prestamo); //Virtualizo mi objeto
                await _prestamo.SaveChangesAsync();

                return prestamo;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Prestamo> GetPrestamoByIdAsync(Guid id)
        {
            try
            {
                var prestamo = await _prestamo.Prestamos.FirstOrDefaultAsync(c => c.Id == id);

                return prestamo;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

    }
}
