using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblUsuario;
using ProyectoFinal.Negocio.Interfaces;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TblUsuarioController : ControllerBase
    {
        private readonly ITblUsuarioService _tblUsuarioService;
        public TblUsuarioController(ITblUsuarioService tblUsuarioService)
        {
            _tblUsuarioService = tblUsuarioService;
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<TblUsuarioReadDto>>> ObtenerTodos()
        {
            try
            {
                var usuarios = await _tblUsuarioService.ObtenerTodos();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        
        [HttpGet("ObtenerPorId/{idUsuario}")]
        public async Task<ActionResult> ObtenerPorId(int idUsuario)
        {
            try
            {
                var usuario = await _tblUsuarioService.ObtenerPorId(idUsuario);
                if (usuario == null)
                    return NotFound(new { message = $"El usuario con Id {idUsuario} no existe" });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idUsuario)
        {
            await _tblUsuarioService.Eliminar(idUsuario);
            return StatusCode(201, new { message = "Eliminado correctamente" });
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(TblUsuarioCreateDto usuario)
        {
            await _tblUsuarioService.Crear(usuario);
            return StatusCode(201, new { message = "Creado correctamente" });
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(TblUsuarioUpdateDto usuario)
        {
            await _tblUsuarioService.Actualizar(usuario);
            return StatusCode(201, new { message = "Actualizado correctamente" });
        }
    }
}
