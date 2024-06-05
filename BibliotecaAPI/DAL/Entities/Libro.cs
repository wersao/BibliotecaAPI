using Biblioteca.DAL.Entities;

namespace BibliotecaAPI.DAL.Entities
{
    public class Libro:AuditBase
    {

        public String Nombre { get; set; }
        public String Autor { get; set; }
        public Boolean EstadoPrestamo { get; set; }

    }
}
