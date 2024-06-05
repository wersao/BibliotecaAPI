using BibliotecaAPI.DAL;
using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Domain.Services
{
    public class LibroService : ILibroService
    {
        private readonly DataBaseContext _libro;
        public LibroService(DataBaseContext context)
        {
            _libro = context;
        }
        public async Task<IEnumerable<Libro>> GetLibrosAsync()
        {
            try
            {
                var libros = await _libro.Libros.ToListAsync();
                return libros;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Libro> GetLibroByIdAsync(Guid id)
        {
            try
            {
                var libro = await _libro.Libros.FirstOrDefaultAsync(c => c.Id == id);
                //Otras dos formas de traerme un objeto desde la BD
                return libro;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Libro> CambiarEstadoLibro(Guid id)
        {
            try
            {
                var libro = await _libro.Libros.FirstOrDefaultAsync(c => c.Id == id);
                if (libro.EstadoPrestamo == true)
                {
                    libro.EstadoPrestamo = false;
                    await _libro.SaveChangesAsync();
                    return libro;
                }
                if (libro.EstadoPrestamo == false)
                {
                    libro.EstadoPrestamo = true;
                    await _libro.SaveChangesAsync();
                    return libro;
                }
                else return null;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }

        }

        public async Task<Libro> CreateLibroAsync(Libro libro)
        {
            try
            {
                libro.Id = Guid.NewGuid();
                libro.CreatedDate = DateTime.Now;
                libro.EstadoPrestamo = false;
                _libro.Libros.Add(libro); //El Método Add() me permite crear el objeto en el contexto de mi BD

                await _libro.SaveChangesAsync(); //Este método me permite guardar el país en mi tabla COUNTRY

                return libro;

            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Libro> EditLibroAsync(Libro libro)
        {
            try
            {
                libro.ModifiedDate = DateTime.Now;

                _libro.Libros.Update(libro); //Virtualizo mi objeto
                await _libro.SaveChangesAsync();

                return libro;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Libro> DeleteLibroAsync(Guid id)
        {
            try
            {
                var libro = await GetLibroByIdAsync(id);

                if (libro == null)
                {
                    return null;
                }

                _libro.Libros.Remove(libro); //Creación del query "Delete from Countries Where Id = @id";
                await _libro.SaveChangesAsync(); //La ejecución del Query

                return libro;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}
