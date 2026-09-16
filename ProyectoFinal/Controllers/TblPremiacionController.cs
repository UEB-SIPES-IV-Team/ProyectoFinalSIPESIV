using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblEvento;
using ProyectoFinal.Negocio.DTOs.TblPremiacion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPremiacionController : ControllerBase
    {
        private readonly ITblPremiacionService _tblPremiacionService;
        public TblPremiacionController(ITblPremiacionService tblPremiacionService)
        {
            _tblPremiacionService = tblPremiacionService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblPremiacionDTO premiacion)
        {
            try
            {
                await _tblPremiacionService.Crear(premiacion);
                return StatusCode(201, new { message = "Premiación creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblPremiacionDTO premiacion)
        {
            try
            {
                await _tblPremiacionService.Actualizar(premiacion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idPremiacion)
        {
            try
            {
                await _tblPremiacionService.Eliminar(idPremiacion);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idPremiacion)
        {
            try
            {
                var premiacion = await _tblPremiacionService.ObtenerPorId(idPremiacion);
                if (premiacion == null) return NotFound(new { message = $"La premiación con Id {idPremiacion} no existe" });
                return Ok(premiacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerPremiaciones()
        {
            try
            {
                return Ok(await _tblPremiacionService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
