using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblParametro;
using ProyectoFinal.Negocio.Interfaces;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblParametroController : ControllerBase
    {
        private readonly ITblParametroService _tblParametroService;
        public TblParametroController(ITblParametroService tblParametroService)
        {
            _tblParametroService = tblParametroService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblParametroDTO parametro)
        {
            try
            {
                await _tblParametroService.Crear(parametro);
                return StatusCode(201, new { message = "Parametro creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblParametroDTO parametro)
        {
            try
            {
                await _tblParametroService.Actualizar(parametro);
                return StatusCode(201, new { message = "Parametro actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idParametro)
        {
            try
            {
                await _tblParametroService.Eliminar(idParametro);
                return StatusCode(201, new { message = "Parametro eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idParametro)
        {
            try
            {
                var parametro = await _tblParametroService.ObtenerPorId(idParametro);
                if (parametro == null) return NotFound(new { message = $"El parametro con Id {idParametro} no existe" });
                return Ok(parametro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerParametros()
        {
            try
            {
                return Ok(await _tblParametroService.ObtenerParametros());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
