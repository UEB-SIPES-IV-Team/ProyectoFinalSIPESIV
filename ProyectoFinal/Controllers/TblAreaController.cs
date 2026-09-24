using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblAreaController : ControllerBase
    {      
        private readonly ITblAreaService _tblAreaService;
        public TblAreaController(ITblAreaService tblAreaService)
        {
            _tblAreaService = tblAreaService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblAreaDTO area)
        {
            try
            {
                await _tblAreaService.Crear(area);
                return StatusCode(201, new { message = "Area creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblAreaDTO area)
        {
            try
            {
                await _tblAreaService.Actualizar(area);
                return StatusCode(201, new { message = "Actualizado correctamente" });
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblAreaDTO dto)
        {
            try
            {
                await _tblAreaService.Eliminar(dto.IdArea);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId/{idArea}")]
        public async Task<ActionResult> ObtenerPorId(int idArea)
        {
            try
            {
                var area = await _tblAreaService.ObtenerPorId(idArea);
                if (area == null) return NotFound(new { message = $"El area con Id {idArea} no existe" });
                return Ok(area);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerAreas()
        {
            try
            {
                return Ok(await _tblAreaService.ObtenerAreas());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
