using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")] //Este es el nombre inicial de mi RUTA, URL o PATH
    [ApiController]
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;
        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosAsync()
        {
            var libros = await _libroService.GetLibrosAsync();

            if (libros == null || !libros.Any()) return NotFound();

            return Ok(libros);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<ActionResult<Libro>> GetLibroByIdAsync(Guid id)
        {
            var libro = await _libroService.GetLibroByIdAsync(id);

            if (libro == null) return NotFound(); //NotFound = Status Code 404

            return Ok(libro); //Ok = Status Code 200
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<Libro>> CreateLibroAsync(Libro libro)
        {
            try
            {
                var newLibro = await _libroService.CreateLibroAsync(libro);
                if (newLibro == null) return NotFound();
                return Ok(newLibro);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", libro.Nombre));

                return Conflict(ex.Message);
            }
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Libro>> EditLibroAsync(Libro libro)
        {
            try
            {
                var editedlibro = await _libroService.EditLibroAsync(libro);
                if (editedlibro == null) return NotFound();
                return Ok(editedlibro);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", libro.Nombre));

                return Conflict(ex.Message);
            }
        }

        [HttpPut, ActionName("Edit")]
        [Route("CambiarEstadoLibro")]
        public async Task<ActionResult<Libro>> CambiarEstadoLibro(Guid guid)
        {
            try
            {
                var editEstado = await _libroService.CambiarEstadoLibro(guid);
                if (editEstado == null) return NotFound();
                return Ok(editEstado);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Libro>> DeleteLibroAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var deletedLibro = await _libroService.DeleteLibroAsync(id);

            if (deletedLibro == null) return NotFound();

            return Ok("Borrado satisfactoriamente");

        }
    }
}
