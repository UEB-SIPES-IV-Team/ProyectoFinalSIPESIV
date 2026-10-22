
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblUnidadOrganizativa;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUnidadOrganizativaController : ControllerBase
    {
        private readonly ITblUnidadOrganizativaService _tblUnidadOrganizativaService;
        private readonly NotificadorCambios _notificador;

        public TblUnidadOrganizativaController(
            ITblUnidadOrganizativaService tblUnidadOrganizativaService,
            NotificadorCambios notificador)
        {
            _tblUnidadOrganizativaService = tblUnidadOrganizativaService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR UNIDAD ORGANIZATIVA
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblUnidadOrganizativaDTO unidadOrganizativa)
        {
            try
            {
                await _tblUnidadOrganizativaService.Crear(
                    unidadOrganizativa);

                await _notificador.NotificarAsync(
                    "UnidadesOrganizativas");

                return StatusCode(201, new
                {
                    message = "Unidad Organizativa creada correctamente"
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
        // ACTUALIZAR UNIDAD ORGANIZATIVA
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblUnidadOrganizativaDTO unidadOrganizativa)
        {
            try
            {
                await _tblUnidadOrganizativaService.Actualizar(
                    unidadOrganizativa);

                await _notificador.NotificarAsync(
                    "UnidadesOrganizativas");

                return Ok(new
                {
                    message = "Unidad Organizativa actualizada correctamente"
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
        // ELIMINAR UNIDAD ORGANIZATIVA
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(
            [FromBody] DeleteTblUnidadOrganizativaDTO dto)
        {
            try
            {
                await _tblUnidadOrganizativaService.Eliminar(
                    dto.lUnidadOrganizativa_id);

                await _notificador.NotificarAsync(
                    "UnidadesOrganizativas");

                return Ok(new
                {
                    message = "Unidad organizativa eliminada correctamente"
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
        // OBTENER TODAS LAS UNIDADES
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerUnidadesOrganizativas()
        {
            try
            {
                var lista = await _tblUnidadOrganizativaService
                    .ObtenerUnidadesOrganizativas();

                return Ok(lista);
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
        // OBTENER UNIDAD POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var result = await _tblUnidadOrganizativaService
                    .ObtenerPorId(id);

                if (result is null)
                {
                    return NotFound(new
                    {
                        message = "Unidad organizativa no encontrada"
                    });
                }

                return Ok(result);
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
