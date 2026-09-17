using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.Interfaces;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblDetalleEvaluacionController : ControllerBase
    {
        private readonly ITblDetalleEvaluacionService _tblDetalleEvaluacionService;
        public TblDetalleEvaluacionController(ITblDetalleEvaluacionService tblDetalleEvaluacionService)
        {
            _tblDetalleEvaluacionService = tblDetalleEvaluacionService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblDetalleEvaluacionDTO detalleEvaluacion)
        {
            try
            {
                await _tblDetalleEvaluacionService.Crear(detalleEvaluacion);
                return StatusCode(201, new { message = "Detalle de evaluación creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateDetalleEvaluacionDTO detalleEvaluacion)
        {
            try
            {
                await _tblDetalleEvaluacionService.Actualizar(detalleEvaluacion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idDetalleEvaluacion)
        {
            try
            {
                await _tblDetalleEvaluacionService.Eliminar(idDetalleEvaluacion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idDetalleEvaluacion)
        {
            try
            {
                var detalleEvaluacion = await _tblDetalleEvaluacionService.ObtenerPorId(idDetalleEvaluacion);
                if (detalleEvaluacion == null) return NotFound(new { message = $"El detalle de evaluación con Id {idDetalleEvaluacion} no existe" });
                return Ok(detalleEvaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerDetallesEvaluacion()
        {
            try
            {
                return Ok(await _tblDetalleEvaluacionService.ObtenerDetallesEvaluacion());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
