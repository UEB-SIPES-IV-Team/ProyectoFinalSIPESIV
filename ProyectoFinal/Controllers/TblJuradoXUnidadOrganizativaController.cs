
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblJuradoXUnidadOrganzativa;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblJuradoXUnidadOrganizativaController : ControllerBase
    {
        private readonly ITblJuradoXUnidadOrganizativaService _tblJuradoXUnidadOrganizativaService;
        private readonly NotificadorCambios _notificador;

        public TblJuradoXUnidadOrganizativaController(
            ITblJuradoXUnidadOrganizativaService tblJuradoXUnidadOrganizativaService,
            NotificadorCambios notificador)
        {
            _tblJuradoXUnidadOrganizativaService =
                tblJuradoXUnidadOrganizativaService;

            _notificador = notificador;
        }

        // =====================================
        // CREAR JURADO POR UNIDAD ORGANIZATIVA
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblJuradoXUnidadOrganzativaDTO jurado)
        {
            try
            {
                await _tblJuradoXUnidadOrganizativaService.Crear(jurado);

                await _notificador.NotificarAsync(
                    "JuradoUnidad");

                return StatusCode(201, new
                {
                    message = "Jurado U.O. creado correctamente"
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
        // ACTUALIZAR JURADO POR UNIDAD
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblJuradoXUnidadOrganzativaDTO jurado)
        {
            try
            {
                await _tblJuradoXUnidadOrganizativaService.Actualizar(jurado);

                await _notificador.NotificarAsync(
                    "JuradoUnidad");

                return Ok(new
                {
                    message = "Jurado U.O. actualizado correctamente"
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
        // ELIMINAR JURADO POR UNIDAD
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblJuradoXUnidadOrganizativaDTO dto)
        {
            try
            {
                await _tblJuradoXUnidadOrganizativaService.Eliminar(
                    dto.lJuradoXFacultad_id);

                await _notificador.NotificarAsync(
                    "JuradoUnidad");

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
        // OBTENER JURADO POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idJurado}")]
        public async Task<ActionResult> ObtenerPorId(int idJurado)
        {
            try
            {
                var jurado =
                    await _tblJuradoXUnidadOrganizativaService
                        .ObtenerPorId(idJurado);

                if (jurado == null)
                {
                    return NotFound(new
                    {
                        message = $"El jurado con Id {idJurado} no existe"
                    });
                }

                return Ok(jurado);
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
        // OBTENER TODOS LOS JURADOS POR UNIDAD
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerJurados()
        {
            try
            {
                var jurados =
                    await _tblJuradoXUnidadOrganizativaService
                        .ObtenerTodos();

                return Ok(jurados);
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
