
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblParametro;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblParametroController : ControllerBase
    {
        private readonly ITblParametroService _tblParametroService;
        private readonly NotificadorCambios _notificador;

        public TblParametroController(
            ITblParametroService tblParametroService,
            NotificadorCambios notificador)
        {
            _tblParametroService = tblParametroService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR PARÁMETRO
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblParametroDTO parametro)
        {
            try
            {
                await _tblParametroService.Crear(parametro);

                await _notificador.NotificarAsync(
                    "ParametrosAcademicos");

                return StatusCode(201, new
                {
                    message = "Parametro creado correctamente"
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
        // ACTUALIZAR PARÁMETRO
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblParametroDTO parametro)
        {
            try
            {
                await _tblParametroService.Actualizar(parametro);

                await _notificador.NotificarAsync(
                    "ParametrosAcademicos");

                return Ok(new
                {
                    message = "Parametro actualizado correctamente"
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
        // ELIMINAR PARÁMETRO
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblParametroDTO dto)
        {
            try
            {
                await _tblParametroService.Eliminar(
                    dto.lParametro_id);

                await _notificador.NotificarAsync(
                    "ParametrosAcademicos");

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
        // OBTENER PARÁMETRO POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idParametro}")]
        public async Task<ActionResult> ObtenerPorId(
            int idParametro)
        {
            try
            {
                var parametro =
                    await _tblParametroService.ObtenerPorId(
                        idParametro);

                if (parametro == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"El parametro con Id {idParametro} no existe"
                    });
                }

                return Ok(parametro);
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
        // OBTENER TODOS LOS PARÁMETROS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerParametros()
        {
            try
            {
                var parametros =
                    await _tblParametroService.ObtenerParametros();

                return Ok(parametros);
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
