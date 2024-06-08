using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;
using BibliotecaAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")] //Este es el nombre inicial de mi RUTA, URL o PATH
    [ApiController]
    public class PrestamoController : Controller
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService PrestamoService)
        {
            _prestamoService = PrestamoService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamosAsync()
        {
            var prestamos = await _prestamoService.GetPrestamosAsync();

            if (prestamos == null || !prestamos.Any()) return NotFound();

            return Ok(prestamos);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<ActionResult<Prestamo>> GetPrestamoByIdAsync(Guid id)
        {
            var Prestamo = await _prestamoService.GetPrestamoByIdAsync(id);

            if (Prestamo == null) return NotFound(); //NotFound = Status Code 404

            return Ok(Prestamo); //Ok = Status Code 200
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<Prestamo>> CreatePrestamoAsync(Prestamo prestamo, Guid Usuarioid, Guid Libroid)
        {
            try
            {
                var newPrestamo = await _prestamoService.CreatePrestamoAsync(prestamo, Usuarioid, Libroid);
                if (newPrestamo == null) return NotFound();
                if (newPrestamo.EstadoLibro == false && newPrestamo.EstadoUsuario == false)
                {
                    return Ok("El Usuario y el Libro esta en prestamo");
                }
                if (newPrestamo.EstadoLibro == false) { 
                    return Ok("El Libro esta en prestamo");
                }
                if (newPrestamo.EstadoUsuario == false) {
                    return Ok("El Usuario esta en prestamo");
                }
                
                    return Ok(newPrestamo);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut, ActionName("Edit")]
        [Route("DevolverPrestamo")]
        public async Task<ActionResult<Prestamo>> DevolverPrestamoAsync(Guid guid)
        {
            try
            {
                var editPrestamo = await _prestamoService.DevolverPrestamoAsync(guid);
                if (editPrestamo == null) return NotFound();
                return Ok(editPrestamo);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Prestamo>> EditPrestamoAsync(Prestamo prestamo)
        {
            try
            {
                var editedPrestamo = await _prestamoService.EditPrestamoAsync(prestamo);
                if (editedPrestamo == null) return NotFound();
                return Ok(editedPrestamo);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", prestamo.Id));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Prestamo>> DeletePrestamoAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var deletedPrestamo = await _prestamoService.DeletePrestamoAsync(id);

            if (deletedPrestamo == null) return NotFound();

            return Ok("Borrado satisfactoriamente");

        }


    }
}
