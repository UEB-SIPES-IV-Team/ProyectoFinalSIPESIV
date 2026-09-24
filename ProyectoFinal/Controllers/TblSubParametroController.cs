using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblSubParametro;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblSubParametroController : ControllerBase
    {
        private readonly ITblSubParametroService _tblSubParametroService;
        public TblSubParametroController(ITblSubParametroService tblSubParametroService)
        {
            _tblSubParametroService = tblSubParametroService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblSubParametroDTO subParametro)
        {
            try
            {
                await _tblSubParametroService.Crear(subParametro);
                return StatusCode(201, new { message = "SubParametro creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblSubParametroDTO subParametro)
        {
            try
            {
                await _tblSubParametroService.Actualizar(subParametro);
                return StatusCode(201, new { message = "SubParametro Actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar([FromBody] DeleteTblSubParametroDTO dto)
        {
            try
            {
                await _tblSubParametroService.Eliminar(dto.lSubParametro_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
      
        [HttpGet("ObtenerTodos")] 
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var resultado = await _tblSubParametroService.ObtenerSubParametros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId/{idSubParametro}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] int idSubParametro)
        {
            try
            {
                var resultado = await _tblSubParametroService.ObtenerPorId(idSubParametro);
                if (resultado == null)
                    return NotFound(new { message = "Subparámetro no encontrado" });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
