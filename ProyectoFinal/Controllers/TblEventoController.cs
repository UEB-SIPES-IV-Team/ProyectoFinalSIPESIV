using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblEvento;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblEventoController : ControllerBase
    {
        private readonly ITblEventoService _tblEventoService;
        public TblEventoController(ITblEventoService tblEventoService)
        {
            _tblEventoService = tblEventoService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblEventoDTO evento)
        {
            try
            {
                await _tblEventoService.Crear(evento);
                return StatusCode(201, new { message = "Evento creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblEventoDTO evento)
        {
            try
            {
                await _tblEventoService.Actualizar(evento);
                return StatusCode(201, new { message = "Evento actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblEventoDTO dto)
        {
            try
            {
                await _tblEventoService.Eliminar(dto.lEvento_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId/{idEvento}")]
        public async Task<ActionResult> ObtenerPorId(int idEvento)
        {
            try
            {
                var evento = await _tblEventoService.ObtenerPorId(idEvento);
                if (evento == null) return NotFound(new { message = $"El evento con Id {idEvento} no existe" });
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerEventos()
        {
            try
            {
                return Ok(await _tblEventoService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
