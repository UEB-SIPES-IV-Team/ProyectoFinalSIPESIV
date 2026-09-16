using Microsoft.AspNetCore.Mvc;
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
                return StatusCode(201, new { message = "Evaluación de visitante creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblEvaluacionVisitanteDTO visitante)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Actualizar(visitante);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idEvaluacionVisitante)
        {
            try
            {
                await _tblEvaluacionVisitanteService.Eliminar(idEvaluacionVisitante);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
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
    }
}
