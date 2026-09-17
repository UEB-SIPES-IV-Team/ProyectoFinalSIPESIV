using Microsoft.AspNetCore.Mvc;

using ProyectoFinal.Negocio.DTOs.TblPersona;
using ProyectoFinal.Negocio.Interfaces;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPersonaController : ControllerBase
    {
        private readonly ITblPersonaService _tblPersonaService;
        public TblPersonaController(ITblPersonaService tblPersonaService)
        {
            _tblPersonaService = tblPersonaService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblPersonaDTO   persona)
        {
            try
            {
                await _tblPersonaService.Crear(persona);
                return StatusCode(201, new { message = "Persona creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblPersonaDTO persona)
        {
            try
            {
                await _tblPersonaService.Actualizar(persona);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idPersona)
        {
            try
            {
                await _tblPersonaService.Eliminar(idPersona);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idPersona)
        {
            try
            {
                var persona = await _tblPersonaService.ObtenerPorId(idPersona);
                if (persona == null) return NotFound(new { message = $"La persona con Id {idPersona} no existe" });
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerTodos()
        {
            try
            {
                return Ok(await _tblPersonaService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
