using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblUsuario;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;
using System.Security.Claims;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TblUsuarioController : ControllerBase
    {
        private readonly ITblUsuarioService _tblUsuarioService;
        private readonly NotificadorCambios _notificador;

        public TblUsuarioController(
            ITblUsuarioService tblUsuarioService,
            NotificadorCambios notificador)
        {
            _tblUsuarioService = tblUsuarioService;
            _notificador = notificador;
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
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("ObtenerPorId/{idUsuario}")]
        public async Task<ActionResult> ObtenerPorId(int idUsuario)
        {
            try
            {
                var usuario = await _tblUsuarioService.ObtenerPorId(idUsuario);

                if (usuario == null)
                {
                    return NotFound(new
                    {
                        message = $"El usuario con Id {idUsuario} no existe"
                    });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Super Administrador,Superadministrador,Administrador")]
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idUsuario)
        {
            await _tblUsuarioService.Eliminar(idUsuario);

            await _notificador.NotificarAsync("Usuarios");

            return Ok(new
            {
                message = "Eliminado correctamente"
            });
        }

        [Authorize(Roles = "Super Administrador,Superadministrador,Administrador")]
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(TblUsuarioCreateDto usuario)
        {
            await _tblUsuarioService.Crear(usuario);

            await _notificador.NotificarAsync("Usuarios");

            return StatusCode(201, new
            {
                message = "Creado correctamente"
            });
        }

        [Authorize(Roles = "Super Administrador,Superadministrador,Administrador")]
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(TblUsuarioUpdateDto usuario)
        {
            await _tblUsuarioService.Actualizar(usuario);

            await _notificador.NotificarAsync("Usuarios");

            return Ok(new
            {
                message = "Actualizado correctamente"
            });
        }

        [Authorize]
        [HttpGet("MiPerfil")]
        public async Task<ActionResult> ObtenerMiPerfil()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdClaim, out int idUsuario))
                    return Unauthorized(new { message = "Token inválido o sin identificador de usuario." });

                var perfil = await _tblUsuarioService.ObtenerPerfil(idUsuario);

                if (perfil == null)
                    return NotFound(new { message = "No se encontraron datos de perfil para este usuario." });

                return Ok(perfil);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("CambiarPassword")]
        public async Task<ActionResult> CambiarPassword([FromBody] CambiarPasswordDto dto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdClaim, out int idUsuario))
                    return Unauthorized(new { message = "Token inválido." });

                bool actualizado = await _tblUsuarioService.CambiarPassword(idUsuario, dto.PasswordActual, dto.NuevaPassword);

                if (!actualizado)
                    return BadRequest(new { message = "La contraseña actual es incorrecta." });

                return Ok(new { message = "Contraseña actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}