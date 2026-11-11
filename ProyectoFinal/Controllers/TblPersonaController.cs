
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblPersona;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPersonaController : ControllerBase
    {
        private readonly ITblPersonaService _tblPersonaService;
        private readonly NotificadorCambios _notificador;

        public TblPersonaController(
            ITblPersonaService tblPersonaService,
            NotificadorCambios notificador)
        {
            _tblPersonaService = tblPersonaService;
            _notificador = notificador;
        }

        

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblPersonaDTO persona)
        {
            try
            {
                await _tblPersonaService.Crear(persona);

                await _notificador.NotificarAsync("Personas");

                return StatusCode(201, new
                {
                    message = "Persona creada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

       

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblPersonaDTO persona)
        {
            try
            {
                await _tblPersonaService.Actualizar(persona);

                await _notificador.NotificarAsync("Personas");

                return Ok(new
                {
                    message = "Persona actualizada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblPersonaDTO dto)
        {
            try
            {
                await _tblPersonaService.Eliminar(dto.lPersona_id);

                await _notificador.NotificarAsync("Personas");

                return Ok(new
                {
                    message = "Eliminado correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        

        [HttpGet("ObtenerPorId/{idPersona}")]
        public async Task<IActionResult> ObtenerPorId(
            [FromRoute] int idPersona)
        {
            try
            {
                var persona =
                    await _tblPersonaService.ObtenerPorId(idPersona);

                if (persona == null)
                {
                    return NotFound(new
                    {
                        message = $"La persona con ID {idPersona} no existe."
                    });
                }

                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerTodos()
        {
            try
            {
                var personas =
                    await _tblPersonaService.ObtenerTodos();

                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }
    }
}
