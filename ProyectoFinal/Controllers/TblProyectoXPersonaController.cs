
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblProyectoXPersona;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblProyectoXPersonaController : ControllerBase
    {
        private readonly ITblProyectoXPersonaService _tblProyectoXPersonaService;
        private readonly NotificadorCambios _notificador;
        private readonly ILogger<TblProyectoXPersonaController> _logger;

        public TblProyectoXPersonaController(
            ITblProyectoXPersonaService tblProyectoXPersonaService,
            NotificadorCambios notificador,
            ILogger<TblProyectoXPersonaController> logger)
        {
            _tblProyectoXPersonaService = tblProyectoXPersonaService;
            _notificador = notificador;
            _logger = logger;
        }

        // =====================================
        // CREAR RELACIÓN PROYECTO - PERSONA
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblProyectoXPersonaDTO proyectoXPersona)
        {
            try
            {
                await _tblProyectoXPersonaService.Crear(
                    proyectoXPersona);
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
                message = "Proyecto asignado a persona correctamente"
            });
        }

        // =====================================
        // ACTUALIZAR RELACIÓN PROYECTO - PERSONA
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblProyectoXPersonaDTO proyectoXPersona)
        {
            try
            {
                await _tblProyectoXPersonaService.Actualizar(
                    proyectoXPersona);
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
                message = "Proyecto actualizado a persona correctamente"
            });
        }

        // =====================================
        // ELIMINAR RELACIÓN PROYECTO - PERSONA
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblProyectoXPersonaDTO dto)
        {
            try
            {
                await _tblProyectoXPersonaService.Eliminar(
                    dto.lProyectoXPersona_id);
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
        // OBTENER RELACIÓN POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idProyectoXPersona}")]
        public async Task<ActionResult> ObtenerPorId(
            int idProyectoXPersona)
        {
            try
            {
                var proyectoXPersona =
                    await _tblProyectoXPersonaService.ObtenerPorId(
                        idProyectoXPersona);

                if (proyectoXPersona == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"El proyecto asignado a persona con Id {idProyectoXPersona} no existe"
                    });
                }

                return Ok(proyectoXPersona);
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
        // OBTENER TODAS LAS RELACIONES
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerProyectosXPersona()
        {
            try
            {
                var relaciones =
                    await _tblProyectoXPersonaService.ObtenerTodos();

                return Ok(relaciones);
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
        // NOTIFICAR CAMBIOS POR SIGNALR
        // =====================================

        private async Task NotificarCambiosAsync()
        {
            try
            {
                await _notificador.NotificarAsync("Proyectos");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "No se pudo enviar la notificación SignalR del módulo Proyectos.");
            }
        }
    }
}
