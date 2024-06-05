using Biblioteca.DAL.Entities;

namespace BibliotecaAPI.DAL.Entities
{
    public class Usuario:AuditBase
    {

        public String Correo { get; set; }
        public Boolean EstadoPrestamo { get; set; }

    }
}
