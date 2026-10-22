using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblEvaluacionVisitante;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblEvaluacionVisitanteController : ControllerBase
    {
        private readonly ITblEvaluacionVisitanteService _tblEvaluacionVisitanteService;

        public TblEvaluacionVisitanteController(ITblEvaluacionVisitanteService tblEvaluacionVisitanteService)
        {
            _tblEvaluacionVisitanteService = tblEvaluacionVisitanteService;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblEvaluacionVisitanteDTO visitante)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Crear(visitante);
                return StatusCode(201, new { message = "Voto registrado exitosamente" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("El visitante ya emitió un voto"))
                {
                    return BadRequest(new { message = "Ya has votado por este proyecto anteriormente." });
                }

                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblEvaluacionVisitanteDTO visitante)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Actualizar(visitante);
                return StatusCode(201, new { message = "Evaluación de visitante Actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblEvaluacionVisitanteDTO dto)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Eliminar(dto.lEvaluacion_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("ObtenerPorId/{idEvaluacionVisitante}")]
        public async Task<ActionResult> ObtenerPorId(int idEvaluacionVisitante)
        {
            try
            {
                var visitante = await _tblEvaluacionVisitanteService.ObtenerPorId(idEvaluacionVisitante);
                if (visitante == null) return NotFound(new { message = $"La evaluación de visitante con Id {idEvaluacionVisitante} no existe" });
                return Ok(visitante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerEvaluacionesVisitantes()
        {
            try
            {
                return Ok(await _tblEvaluacionVisitanteService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // 🟢 1. OBTENER RANKING Y PODIO DE PROYECTOS POR EVENTO (ORDENADO DE MAYOR A MENOR)
        [HttpGet("ObtenerRanking/{idEvento}")]
        public async Task<ActionResult> ObtenerRanking(int idEvento)
        {
            try
            {
                var ranking = await _tblEvaluacionVisitanteService.ObtenerRanking(idEvento);
                return Ok(ranking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // 🟢 2. OBTENER MOVIMIENTOS Y VOTOS RECIENTES POR EVENTO
        [HttpGet("ObtenerRecientes/{idEvento}")]
        public async Task<ActionResult> ObtenerRecientes(int idEvento)
        {
            try
            {
                var recientes = await _tblEvaluacionVisitanteService.ObtenerRecientes(idEvento);
                return Ok(recientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // 🟢 OBTENER EVALUACIÓN PREVIA POR VISITANTE Y PROYECTO
        [HttpGet("ObtenerPorVisitanteYProyecto/{idVisitante}/{idProyecto}")]
        public async Task<ActionResult> ObtenerPorVisitanteYProyecto(int idVisitante, int idProyecto)
        {
            try
            {
                var evaluacion = await _tblEvaluacionVisitanteService.ObtenerPorVisitanteYProyecto(idVisitante, idProyecto);

                if (evaluacion == null)
                {
                    return NotFound(new { message = "El visitante no ha evaluado este proyecto aún." });
                }

                return Ok(evaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}