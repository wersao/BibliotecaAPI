using BibliotecaAPI.DAL.Entities;
using BibliotecaAPI.Domain.Interfaces;
using BibliotecaAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")] //Este es el nombre inicial de mi RUTA, URL o PATH
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosAsync()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();

            if (usuarios == null || !usuarios.Any()) return NotFound();

            return Ok(usuarios);
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<ActionResult<Usuario>> GetUsuarioByIdAsync(Guid id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null) return NotFound(); //NotFound = Status Code 404

            return Ok(usuario); //Ok = Status Code 200
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<Usuario>> CreateUsuarioAsync(Usuario usuario)
        {
            try
            {
                var newUsuario = await _usuarioService.CreateUsuarioAsync(usuario);
                if (newUsuario == null) return NotFound();
                return Ok(newUsuario);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", usuario.Correo));

                return Conflict(ex.Message);
            }
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Usuario>> EditUsuarioAsync(Usuario usuario)
        {
            try
            {
                var editedusuario = await _usuarioService.EditUsuarioAsync(usuario);
                if (editedusuario == null) return NotFound();
                return Ok(editedusuario);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", usuario.Correo));

                return Conflict(ex.Message);
            }
        }

        [HttpPut, ActionName("Edit")]
        [Route("CambiarEstadoUsuario")]
        public async Task<ActionResult<Usuario>> CambiarEstadoUsuario(Guid guid)
        {
            try
            {
                var editEstado = await _usuarioService.CambiarEstadoUsuario(guid);
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
        public async Task<ActionResult<Usuario>> DeleteUsuarioAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var deletedUsuario = await _usuarioService.DeleteUsuarioAsync(id);

            if (deletedUsuario == null) return NotFound();

            return Ok("Borrado satisfactoriamente");

        }
    }
}
