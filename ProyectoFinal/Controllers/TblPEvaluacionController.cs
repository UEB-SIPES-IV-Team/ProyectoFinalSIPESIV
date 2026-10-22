
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblPEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPEvaluacionController : ControllerBase
    {
        private readonly ITblPEvaluacionService _tblPEvaluacionService;
        private readonly NotificadorCambios _notificador;
        private readonly ILogger<TblPEvaluacionController> _logger;

        public TblPEvaluacionController(
            ITblPEvaluacionService tblPEvaluacionService,
            NotificadorCambios notificador,
            ILogger<TblPEvaluacionController> logger)
        {
            _tblPEvaluacionService = tblPEvaluacionService;
            _notificador = notificador;
            _logger = logger;
        }

        // =====================================
        // CREAR PARÁMETRO DE EVALUACIÓN
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblPEvaluacionDTO evaluacion)
        {
            try
            {
                await _tblPEvaluacionService.Crear(evaluacion);
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
                message = "Evaluación creada correctamente"
            });
        }

        // =====================================
        // ACTUALIZAR PARÁMETRO DE EVALUACIÓN
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblPEvaluacionDTO evaluacion)
        {
            try
            {
                await _tblPEvaluacionService.Actualizar(evaluacion);
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
                message = "Evaluación actualizada correctamente"
            });
        }

        // =====================================
        // ELIMINAR PARÁMETRO DE EVALUACIÓN
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblPEvaluacionDTO dto)
        {
            try
            {
                await _tblPEvaluacionService.Eliminar(
                    dto.lPEvaluacion_id);
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
        // OBTENER PARÁMETRO POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(
            [FromRoute] int id)
        {
            try
            {
                var result =
                    await _tblPEvaluacionService.ObtenerPorId(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"No se encontró la evaluación con ID {id}"
                    });
                }

                return Ok(result);
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
        // OBTENER TODOS LOS PARÁMETROS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerEvaluaciones()
        {
            try
            {
                var evaluaciones =
                    await _tblPEvaluacionService.ObtenerEvaluaciones();

                return Ok(evaluaciones);
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
                await _notificador.NotificarAsync(
                    "ParametrosEvaluacion");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "No se pudo enviar la notificación SignalR de ParametrosEvaluacion.");
            }
        }
    }
}
