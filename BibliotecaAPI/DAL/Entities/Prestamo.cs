using Biblioteca.DAL.Entities;

namespace BibliotecaAPI.DAL.Entities
{
    public class Prestamo:AuditBase
    {

        public Guid IdUsuario { get; set; }
        public Guid IdLibro { get; set; }
        public bool EstadoPrestamo { get; set; }
        public bool EstadoUsuario { get; set; }
        public bool EstadoLibro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

    }
}
