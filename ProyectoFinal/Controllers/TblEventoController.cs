
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblEvento;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;
using System;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblEventoController : ControllerBase
    {
        private readonly ITblEventoService _tblEventoService;
        private readonly NotificadorCambios _notificadorCambios;

        public TblEventoController(
            ITblEventoService tblEventoService,
            NotificadorCambios notificadorCambios)
        {
            _tblEventoService = tblEventoService;
            _notificadorCambios = notificadorCambios;
        }

        // =========================================
        // CREAR EVENTO
        // =========================================
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblEventoDTO evento)
        {
            try
            {
                await _tblEventoService.Crear(evento);

                await _notificadorCambios
                    .NotificarAsync("Eventos");

                return StatusCode(201, new
                {
                    message = "Evento creado correctamente"
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

        // =========================================
        // ACTUALIZAR EVENTO
        // =========================================
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblEventoDTO evento)
        {
            try
            {
                await _tblEventoService.Actualizar(evento);

                await _notificadorCambios
                    .NotificarAsync("Eventos");

                return Ok(new
                {
                    message = "Evento actualizado correctamente"
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

        // =========================================
        // ELIMINAR EVENTO
        // =========================================
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblEventoDTO dto)
        {
            try
            {
                await _tblEventoService.Eliminar(
                    dto.lEvento_id);

                await _notificadorCambios
                    .NotificarAsync("Eventos");

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

        // =========================================
        // OBTENER POR ID
        // =========================================
        [HttpGet("ObtenerPorId/{idEvento}")]
        public async Task<ActionResult> ObtenerPorId(
            int idEvento)
        {
            try
            {
                var evento = await _tblEventoService
                    .ObtenerPorId(idEvento);

                if (evento == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"El evento con Id {idEvento} no existe"
                    });
                }

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // =========================================
        // OBTENER TODOS
        // =========================================
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerEventos()
        {
            try
            {
                return Ok(
                    await _tblEventoService.ObtenerTodos()
                );
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
