
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblDetalleEvaluacionController : ControllerBase
    {
        private readonly ITblDetalleEvaluacionService _tblDetalleEvaluacionService;
        private readonly NotificadorCambios _notificador;
        private readonly ILogger<TblDetalleEvaluacionController> _logger;

        public TblDetalleEvaluacionController(
            ITblDetalleEvaluacionService tblDetalleEvaluacionService,
            NotificadorCambios notificador,
            ILogger<TblDetalleEvaluacionController> logger)
        {
            _tblDetalleEvaluacionService = tblDetalleEvaluacionService;
            _notificador = notificador;
            _logger = logger;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblDetalleEvaluacionDTO detalleEvaluacion)
        {
            try
            {
                await _tblDetalleEvaluacionService.Crear(
                    detalleEvaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarEvaluacionesAsync();

            return StatusCode(201, new
            {
                message = "Detalle de evaluación creado correctamente"
            });
        }

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateDetalleEvaluacionDTO detalleEvaluacion)
        {
            try
            {
                await _tblDetalleEvaluacionService.Actualizar(
                    detalleEvaluacion);
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
                message = "Detalle de evaluación actualizado correctamente"
            });
        }

 

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            int idDetalleEvaluacion)
        {
            try
            {
                await _tblDetalleEvaluacionService.Eliminar(
                    idDetalleEvaluacion);
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
                message = "Detalle de evaluación eliminado correctamente"
            });
        }

      

        [HttpGet("ObtenerPorId/{idDetalleEvaluacion}")]
        public async Task<ActionResult> ObtenerPorId(
            int idDetalleEvaluacion)
        {
            try
            {
                var detalleEvaluacion =
                    await _tblDetalleEvaluacionService.ObtenerPorId(
                        idDetalleEvaluacion);

                if (detalleEvaluacion == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"El detalle de evaluación con Id {idDetalleEvaluacion} no existe"
                    });
                }

                return Ok(detalleEvaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

       

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerDetallesEvaluacion()
        {
            try
            {
                var detalles =
                    await _tblDetalleEvaluacionService
                        .ObtenerDetallesEvaluacion();

                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }


        private async Task NotificarEvaluacionesAsync()
        {
            try
            {
                await _notificador.NotificarAsync("Evaluaciones");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "No se pudo enviar la notificación SignalR del módulo Evaluaciones.");
            }
        }
    }
}
