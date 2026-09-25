using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblPEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

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
                return StatusCode(201, new { message = "Evaluación actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblPEvaluacionDTO dto)
        {
            try
            {
                await _tblPEvaluacionService.Eliminar(dto.lPEvaluacion_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] int id)
        {
            try
            {
                var result = await _tblPEvaluacionService.ObtenerPorId(id);

                if (result == null)
                {
                    return NotFound(new { message = $"No se encontró la evaluación con ID {id}" });
                }

                return Ok(result); 
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
