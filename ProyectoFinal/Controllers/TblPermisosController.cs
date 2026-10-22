
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs;
using ProyectoFinal.Negocio.DTOs.TblPermiso;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPermisosController : ControllerBase
    {
        private readonly ITblPermisosService _tblPermisosService;
        private readonly NotificadorCambios _notificador;
        private readonly ILogger<TblPermisosController> _logger;

        public TblPermisosController(
            ITblPermisosService tblPermisosService,
            NotificadorCambios notificador,
            ILogger<TblPermisosController> logger)
        {
            _tblPermisosService = tblPermisosService;
            _notificador = notificador;
            _logger = logger;
        }

        // =====================================
        // OBTENER TODOS LOS PERMISOS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<TblPermisosReadDto>>> ObtenerObtenerTodos()
        {
            try
            {
                List<TblPermisosReadDto> permisos =
                    await _tblPermisosService.ObtenerTodos();

                return Ok(permisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // =====================================
        // OBTENER PERMISO POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idPermisos}")]
        public async Task<ActionResult<TblPermisosReadDto>> ObtenerPorId(
            int idPermisos)
        {
            try
            {
                TblPermisosReadDto permisos =
                    await _tblPermisosService.ObtenerPorId(idPermisos);

                if (permisos == null)
                {
                    return NotFound(new
                    {
                        message = $"El permiso con ID {idPermisos} no existe"
                    });
                }

                return Ok(permisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // =====================================
        // CREAR PERMISO
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            TblPermisosCreateDto permisos)
        {
            try
            {
                await _tblPermisosService.Crear(permisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarCambiosAsync();

            return StatusCode(201, new
            {
                message = "Permisos creado correctamente"
            });
        }

        // =====================================
        // ACTUALIZAR PERMISO
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            TblPermisosUpdateDto permisos)
        {
            try
            {
                await _tblPermisosService.Actualizar(permisos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarCambiosAsync();

            return Ok(new
            {
                message = "Permisos actualizados correctamente"
            });
        }

        // =====================================
        // ELIMINAR PERMISO
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblPermisoDTO dto)
        {
            try
            {
                await _tblPermisosService.Eliminar(
                    dto.lPermisos_id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarCambiosAsync();

            return Ok(new
            {
                message = "Eliminado correctamente"
            });
        }

        // =====================================
        // NOTIFICAR CAMBIOS POR SIGNALR
        // =====================================

        private async Task NotificarCambiosAsync()
        {
            try
            {
                await _notificador.NotificarAsync("Permisos");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "No se pudo enviar la notificación SignalR del módulo Permisos.");
            }
        }
    }
}
