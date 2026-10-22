
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblVisitante;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblVisitanteController : ControllerBase
    {
        private readonly ITblVisitanteService _tblVisitanteService;
        private readonly NotificadorCambios _notificador;
        private readonly ILogger<TblVisitanteController> _logger;

        public TblVisitanteController(
            ITblVisitanteService tblVisitanteService,
            NotificadorCambios notificador,
            ILogger<TblVisitanteController> logger)
        {
            _tblVisitanteService = tblVisitanteService;
            _notificador = notificador;
            _logger = logger;
        }

        // =====================================
        // CREAR VISITANTE
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblVisitanteDTO visitante)
        {
            try
            {
                await _tblVisitanteService.Crear(visitante);
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
                message = "Visitante creado correctamente"
            });
        }

        // =====================================
        // ACTUALIZAR VISITANTE
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblVisitanteDTO visitante)
        {
            try
            {
                await _tblVisitanteService.Actualizar(visitante);
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
                message = "Visitante actualizado correctamente"
            });
        }

        // =====================================
        // ELIMINAR VISITANTE
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(
            [FromBody] DeleteTblVisitanteDTO dto)
        {
            try
            {
                await _tblVisitanteService.Eliminar(
                    dto.lVisitante_id);
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
                message = "Visitante eliminado correctamente"
            });
        }

        // =====================================
        // OBTENER VISITANTE POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idVisitante}")]
        public async Task<ActionResult> ObtenerPorId(
            int idVisitante)
        {
            try
            {
                var visitante =
                    await _tblVisitanteService.ObtenerPorId(
                        idVisitante);

                if (visitante == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"El visitante con Id {idVisitante} no existe"
                    });
                }

                return Ok(visitante);
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
        // OBTENER TODOS LOS VISITANTES
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerVisitantes()
        {
            try
            {
                var visitantes =
                    await _tblVisitanteService.ObtenerTodos();

                return Ok(visitantes);
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
                await _notificador.NotificarAsync("Visitantes");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "No se pudo enviar la notificación SignalR del módulo Visitantes.");
            }
        }
    }
}
