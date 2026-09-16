using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblProyectoXPersona;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblProyectoXPersonaController : ControllerBase
    {
        private readonly ITblProyectoXPersonaService _tblProyectoXPersonaService;
        public TblProyectoXPersonaController(ITblProyectoXPersonaService tblProyectoXPersonaService)
        {
            _tblProyectoXPersonaService = tblProyectoXPersonaService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblProyectoXPersonaDTO proyectoXPersona)
        {
            try
            {
                await _tblProyectoXPersonaService.Crear(proyectoXPersona);
                return StatusCode(201, new { message = "Proyecto asignado a persona correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblProyectoXPersonaDTO proyectoXPersona)
        {
            try
            {
                await _tblProyectoXPersonaService.Actualizar(proyectoXPersona);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idProyectoXPersona)
        {
            try
            {
                await _tblProyectoXPersonaService.Eliminar(idProyectoXPersona);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idProyectoXPersona)
        {
            try
            {
                var proyectoXPersona = await _tblProyectoXPersonaService.ObtenerPorId(idProyectoXPersona);
                if (proyectoXPersona == null) return NotFound(new { message = $"El proyecto asignado a persona con Id {idProyectoXPersona} no existe" });
                return Ok(proyectoXPersona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerProyectosXPersona()
        {
            try
            {
                return Ok(await _tblProyectoXPersonaService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
