using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;
using BibliotecaAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<Prestamo>> CreatePrestamoAsync(Prestamo prestamo, Guid Usuarioid, Guid Libroid)
        {
            try
            {
                var newPrestamo = await _prestamoService.CreatePrestamoAsync(prestamo, Usuarioid, Libroid);
                if (newPrestamo == null) return NotFound();
                return Ok(newPrestamo);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
