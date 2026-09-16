using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblVisitante;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblVisitanteController : ControllerBase
    {
        private readonly ITblVisitanteService _tblVisitanteService;
        public TblVisitanteController(ITblVisitanteService tblVisitanteService)
        {
            _tblVisitanteService = tblVisitanteService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblVisitanteDTO visitante)
        {
            try
            {
                await _tblVisitanteService.Crear(visitante);
                return StatusCode(201, new { message = "Visitante creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblVisitanteDTO visitante)
        {
            try
            {
                await _tblVisitanteService.Actualizar(visitante);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idVisitante)
        {
            try
            {
                await _tblVisitanteService.Eliminar(idVisitante);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idVisitante)
        {
            try
            {
                var visitante = await _tblVisitanteService.ObtenerPorId(idVisitante);
                if (visitante == null) return NotFound(new { message = $"El visitante con Id {idVisitante} no existe" });
                return Ok(visitante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerVisitantes()
        {
            try
            {
                return Ok(await _tblVisitanteService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
