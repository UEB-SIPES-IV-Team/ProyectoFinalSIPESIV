
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblPremiacion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPremiacionController : ControllerBase
    {
        private readonly ITblPremiacionService _tblPremiacionService;
        private readonly NotificadorCambios _notificador;

        public TblPremiacionController(
            ITblPremiacionService tblPremiacionService,
            NotificadorCambios notificador)
        {
            _tblPremiacionService = tblPremiacionService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR PREMIACIÓN
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblPremiacionDTO premiacion)
        {
            try
            {
                await _tblPremiacionService.Crear(premiacion);

                await _notificador.NotificarAsync("Premiaciones");

                return StatusCode(201, new
                {
                    message = "Premiación creada correctamente"
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

        // =====================================
        // ACTUALIZAR PREMIACIÓN
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblPremiacionDTO premiacion)
        {
            try
            {
                await _tblPremiacionService.Actualizar(premiacion);

                await _notificador.NotificarAsync("Premiaciones");

                return Ok(new
                {
                    message = "Premiación actualizada correctamente"
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

        // =====================================
        // ELIMINAR PREMIACIÓN
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblPremiacionDTO dto)
        {
            try
            {
                await _tblPremiacionService.Eliminar(
                    dto.lPremiacion_id);

                await _notificador.NotificarAsync("Premiaciones");

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

        // =====================================
        // OBTENER PREMIACIÓN POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idPremiacion}")]
        public async Task<ActionResult> ObtenerPorId(
            int idPremiacion)
        {
            try
            {
                var premiacion =
                    await _tblPremiacionService.ObtenerPorId(
                        idPremiacion);

                if (premiacion == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"La premiación con Id {idPremiacion} no existe"
                    });
                }

                return Ok(premiacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }

        // =====================================
        // OBTENER TODAS LAS PREMIACIONES
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerPremiaciones()
        {
            try
            {
                var premiaciones =
                    await _tblPremiacionService.ObtenerTodos();

                return Ok(premiaciones);
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
