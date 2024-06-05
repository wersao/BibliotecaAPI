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
                if (usuario.EstadoPrestamo == false && libro.EstadoPrestamo == false)
                {
                    prestamo.Id = Guid.NewGuid();
                    prestamo.CreatedDate = DateTime.Now;
                    prestamo.FechaPrestamo = DateTime.Now;
                    prestamo.IdLibro = Libroid;
                    prestamo.IdUsuario = Usuarioid;
                    prestamo.ModifiedDate = DateTime.Now;
                    usuario.EstadoPrestamo = true;
                    libro.EstadoPrestamo = true;
                    await _prestamo.SaveChangesAsync();
                    return prestamo;
                }

                else return null;


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
                prestamo.FechaDevolucion = DateTime.Now;
                var usuario = await _prestamo.Usuarios.FirstOrDefaultAsync(u => u.Id == prestamo.IdUsuario);
                var libro = await _prestamo.Libros.FirstOrDefaultAsync(l => l.Id == prestamo.IdLibro);
                if (usuario.EstadoPrestamo == true && libro.EstadoPrestamo == true)
                {
                    usuario.EstadoPrestamo = false;
                    libro.EstadoPrestamo = false;
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

        public Task<Prestamo> DeletePrestamoAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Prestamo> EditPrestamoAsync(Prestamo prestamo)
        {
            throw new NotImplementedException();
        }

        public Task<Prestamo> GetPrestamoByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
