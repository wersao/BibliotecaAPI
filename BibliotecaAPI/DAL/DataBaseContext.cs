using BibliotecaAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.DAL
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        #region DbSets

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        #endregion

    }
}
