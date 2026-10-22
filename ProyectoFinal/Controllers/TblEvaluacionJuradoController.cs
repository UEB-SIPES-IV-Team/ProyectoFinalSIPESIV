
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblEvaluacionJurado;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblEvaluacionJuradoController : ControllerBase
    {
        private readonly ITblEvaluacionJuradoService _tblEvaluacionJuradoService;
        private readonly NotificadorCambios _notificador;

        public TblEvaluacionJuradoController(
            ITblEvaluacionJuradoService tblEvaluacionJuradoService,
            NotificadorCambios notificador)
        {
            _tblEvaluacionJuradoService = tblEvaluacionJuradoService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR EVALUACIÓN DE JURADO
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblEvaluacionJuradoDTO evaluacionJurado)
        {
            try
            {
                await _tblEvaluacionJuradoService.Crear(
                    evaluacionJurado);

                await _notificador.NotificarAsync("Evaluaciones");

                return StatusCode(201, new
                {
                    message = "Evaluación de jurado creada correctamente"
                });
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
        // ACTUALIZAR EVALUACIÓN DE JURADO
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblEvaluacionJuradoDTO evaluacionJurado)
        {
            try
            {
                await _tblEvaluacionJuradoService.Actualizar(
                    evaluacionJurado);

                await _notificador.NotificarAsync("Evaluaciones");

                return Ok(new
                {
                    message = "Evaluación de jurado actualizada correctamente"
                });
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
        // ELIMINAR EVALUACIÓN DE JURADO
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            int idEvaluacionJurado)
        {
            try
            {
                await _tblEvaluacionJuradoService.Eliminar(
                    idEvaluacionJurado);

                await _notificador.NotificarAsync("Evaluaciones");

                return Ok(new
                {
                    message = "Evaluación de jurado eliminada correctamente"
                });
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
        // OBTENER EVALUACIÓN POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idEvaluacionJurado}")]
        public async Task<ActionResult> ObtenerPorId(
            int idEvaluacionJurado)
        {
            try
            {
                var evaluacionJurado =
                    await _tblEvaluacionJuradoService.ObtenerPorId(
                        idEvaluacionJurado);

                if (evaluacionJurado == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"La evaluación de jurado con Id {idEvaluacionJurado} no existe"
                    });
                }

                return Ok(evaluacionJurado);
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
        public async Task<ActionResult> ObtenerTodos()
        {
            try
            {
                var evaluaciones =
                    await _tblEvaluacionJuradoService.ObtenerTodos();

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
    }
}
