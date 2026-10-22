
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblProyecto;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblProyectoController : ControllerBase
    {
        private readonly ITblProyectoService _tblProyectoService;
        private readonly NotificadorCambios _notificador;

        public TblProyectoController(
            ITblProyectoService tblProyectoService,
            NotificadorCambios notificador)
        {
            _tblProyectoService = tblProyectoService;
            _notificador = notificador;
        }

        
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblProyectoDTO proyecto)
        {
            try
            {
                await _tblProyectoService.Crear(proyecto);

                await _notificador.NotificarAsync("Proyectos");

                return StatusCode(201, new
                {
                    message = "Proyecto creado correctamente"
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
            UpdateTblProyectoDTO proyecto)
        {
            try
            {
                await _tblProyectoService.Actualizar(proyecto);

                await _notificador.NotificarAsync("Proyectos");

                return Ok(new
                {
                    message = "Proyecto actualizado correctamente"
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
            [FromBody] DeleteTblProyectoDTO dto)
        {
            try
            {
                await _tblProyectoService.Eliminar(dto.lProyecto_id);

                await _notificador.NotificarAsync("Proyectos");

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

        [HttpGet("ObtenerPorId/{idProyecto}")]
        public async Task<ActionResult> ObtenerPorId(int idProyecto)
        {
            try
            {
                var proyecto = await _tblProyectoService.ObtenerPorId(idProyecto);
                if (proyecto == null) return NotFound(new { message = $"El proyecto con Id {idProyecto} no existe" });
                return Ok(proyecto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerProyectos()
        {
            try
            {
                return Ok(await _tblProyectoService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // ENDPOINT CORREGIDO PARA OBTENER PROYECTOS POR EVENTO
        [HttpGet("ObtenerPorEvento/{eventoId}")]
        public async Task<ActionResult> ObtenerPorEvento(int eventoId)
        {
            try
            {
                var proyectos = await _tblProyectoService.ObtenerPorEventoAsync(eventoId);
                return Ok(proyectos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
