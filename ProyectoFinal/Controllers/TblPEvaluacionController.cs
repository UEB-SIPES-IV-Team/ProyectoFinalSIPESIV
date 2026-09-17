using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblPEvaluacion;
using ProyectoFinal.Negocio.Interfaces;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPEvaluacionController : ControllerBase
    {
        private readonly ITblPEvaluacionService _tblPEvaluacionService;
        public TblPEvaluacionController(ITblPEvaluacionService tblPEvaluacionService)
        {
            _tblPEvaluacionService = tblPEvaluacionService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblPEvaluacionDTO evaluacion)
        {
            try
            {
                await _tblPEvaluacionService.Crear(evaluacion);
                return StatusCode(201, new { message = "Evaluación creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblPEvaluacionDTO evaluacion)
        {
            try
            {
                await _tblPEvaluacionService.Actualizar(evaluacion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idEvaluacion)
        {
            try
            {
                await _tblPEvaluacionService.Eliminar(idEvaluacion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idEvaluacion)
        {
            try
            {
                var evaluacion = await _tblPEvaluacionService.ObtenerPorId(idEvaluacion);
                if (evaluacion == null) return NotFound(new { message = $"La evaluación con Id {idEvaluacion} no existe" });
                return Ok(evaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerEvaluaciones()
        {
            try
            {
                return Ok(await _tblPEvaluacionService.ObtenerEvaluaciones());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
