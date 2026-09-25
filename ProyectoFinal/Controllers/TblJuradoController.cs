using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblJurado;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblJuradoController : ControllerBase
    {
        private readonly ITblJuradoService _tblJuradoService;
        public TblJuradoController(ITblJuradoService tblJuradoService)
        {
            _tblJuradoService = tblJuradoService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblJuradoDTO jurado)
        {
            try
            {
                await _tblJuradoService.Crear(jurado);
                return StatusCode(201, new { message = "Jurado creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblJuradoDTO jurado)
        {
            try
            {
                await _tblJuradoService.Actualizar(jurado);
                return StatusCode(201, new { message = "Jurado actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblJuradoDTO dto)
        {
            try
            {
                await _tblJuradoService.Eliminar(dto.lJurado_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId/{idJurado}")]
        public async Task<ActionResult> ObtenerPorId(int idJurado)
        {
            try
            {
                var jurado = await _tblJuradoService.ObtenerPorId(idJurado);
                if (jurado == null) return NotFound(new { message = $"El jurado con Id {idJurado} no existe" });
                return Ok(jurado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerJurados()
        {
            try
            {
                return Ok(await _tblJuradoService.ObtenerJurados());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}