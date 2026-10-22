
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblAsignatura;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblAsignaturaController : ControllerBase
    {
        private readonly ITblAsignaturaService _tblAsignaturaService;
        private readonly NotificadorCambios _notificador;

        public TblAsignaturaController(
            ITblAsignaturaService tblAsignaturaService,
            NotificadorCambios notificador)
        {
            _tblAsignaturaService = tblAsignaturaService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR ASIGNATURA
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblAsignaturaDTO asignatura)
        {
            try
            {
                await _tblAsignaturaService.Crear(asignatura);

                await _notificador.NotificarAsync("Asignaturas");

                return StatusCode(201, new
                {
                    message = "Asignatura creada correctamente"
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
        // ACTUALIZAR ASIGNATURA
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblAsignaturaDTO asignatura)
        {
            try
            {
                await _tblAsignaturaService.Actualizar(asignatura);

                await _notificador.NotificarAsync("Asignaturas");

                return Ok(new
                {
                    message = "Asignatura actualizada correctamente"
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
        // ELIMINAR ASIGNATURA
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblAsignaturaDTO dto)
        {
            try
            {
                await _tblAsignaturaService.Eliminar(
                    dto.lAsignatura_id);

                await _notificador.NotificarAsync("Asignaturas");

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
        // OBTENER ASIGNATURA POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idAsignatura}")]
        public async Task<ActionResult> ObtenerPorId(
            int idAsignatura)
        {
            try
            {
                var asignatura =
                    await _tblAsignaturaService.ObtenerPorId(
                        idAsignatura);

                if (asignatura == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"La asignatura con Id {idAsignatura} no existe"
                    });
                }

                return Ok(asignatura);
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
        // OBTENER TODAS LAS ASIGNATURAS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerAsignaturas()
        {
            try
            {
                var asignaturas =
                    await _tblAsignaturaService.ObtenerAsignaturas();

                return Ok(asignaturas);
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
