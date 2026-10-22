
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblInstitucion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblInstitucionController : ControllerBase
    {
        private readonly ITblInstitucionService _tblInstitucionService;
        private readonly NotificadorCambios _notificador;

        public TblInstitucionController(
            ITblInstitucionService tblInstitucionService,
            NotificadorCambios notificador)
        {
            _tblInstitucionService = tblInstitucionService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR INSTITUCIÓN
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblInstitucionDTO institucion)
        {
            try
            {
                await _tblInstitucionService.Crear(institucion);

                await _notificador.NotificarAsync("Instituciones");

                return StatusCode(201, new
                {
                    message = "Institucion creada correctamente"
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
        // ACTUALIZAR INSTITUCIÓN
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblInstitucionDTO institucion)
        {
            try
            {
                await _tblInstitucionService.Actualizar(institucion);

                await _notificador.NotificarAsync("Instituciones");

                return Ok(new
                {
                    message = "Institucion actualizada correctamente"
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
        // ELIMINAR INSTITUCIÓN
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblInstitucionDTO dto)
        {
            try
            {
                await _tblInstitucionService.Eliminar(
                    dto.lInstitucion_id);

                await _notificador.NotificarAsync("Instituciones");

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
        // OBTENER INSTITUCIÓN POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idInstitucion}")]
        public async Task<ActionResult> ObtenerPorId(
            int idInstitucion)
        {
            try
            {
                var institucion =
                    await _tblInstitucionService.ObtenerPorId(
                        idInstitucion);

                if (institucion == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"La institucion con Id {idInstitucion} no existe"
                    });
                }

                return Ok(institucion);
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
        // OBTENER TODAS LAS INSTITUCIONES
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerInstituciones()
        {
            try
            {
                var instituciones =
                    await _tblInstitucionService.ObtenerInstituciones();

                return Ok(instituciones);
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
