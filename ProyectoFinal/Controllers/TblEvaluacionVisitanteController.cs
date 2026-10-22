
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblEvaluacionVisitante;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblEvaluacionVisitanteController : ControllerBase
    {
        private readonly ITblEvaluacionVisitanteService _tblEvaluacionVisitanteService;
        private readonly NotificadorCambios _notificador;

        public TblEvaluacionVisitanteController(
            ITblEvaluacionVisitanteService tblEvaluacionVisitanteService,
            NotificadorCambios notificador)
        {
            _tblEvaluacionVisitanteService =
                tblEvaluacionVisitanteService;

            _notificador = notificador;
        }

        // =====================================
        // CREAR VOTO DE VISITANTE
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblEvaluacionVisitanteDTO visitante)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Crear(
                    visitante);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(
                    "El visitante ya emitió un voto"))
                {
                    return BadRequest(new
                    {
                        message =
                            "Ya has votado por este proyecto anteriormente."
                    });
                }

                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarEvaluacionesAsync();

            return StatusCode(201, new
            {
                message = "Voto registrado exitosamente"
            });
        }

        // =====================================
        // ACTUALIZAR VOTO DE VISITANTE
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblEvaluacionVisitanteDTO visitante)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Actualizar(
                    visitante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarEvaluacionesAsync();

            return Ok(new
            {
                message =
                    "Evaluación de visitante actualizada correctamente"
            });
        }

        // =====================================
        // ELIMINAR VOTO DE VISITANTE
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblEvaluacionVisitanteDTO dto)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Eliminar(
                    dto.lEvaluacion_id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarEvaluacionesAsync();

            return Ok(new
            {
                message = "Eliminado correctamente"
            });
        }

        // =====================================
        // OBTENER EVALUACIÓN POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idEvaluacionVisitante}")]
        public async Task<ActionResult> ObtenerPorId(
            int idEvaluacionVisitante)
        {
            try
            {
                var visitante =
                    await _tblEvaluacionVisitanteService.ObtenerPorId(
                        idEvaluacionVisitante);

                if (visitante == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"La evaluación de visitante con Id {idEvaluacionVisitante} no existe"
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
        // OBTENER TODAS LAS EVALUACIONES
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerEvaluacionesVisitantes()
        {
            try
            {
                var evaluaciones =
                    await _tblEvaluacionVisitanteService.ObtenerTodos();

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

        private async Task NotificarEvaluacionesAsync()
        {
            try
            {
                await _notificador.NotificarAsync("Evaluaciones");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    $"Error al notificar por SignalR: {ex.Message}");
            }
        }
    }
}
