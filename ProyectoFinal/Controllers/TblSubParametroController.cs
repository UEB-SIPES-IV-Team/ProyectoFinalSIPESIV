
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblSubParametro;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblSubParametroController : ControllerBase
    {
        private readonly ITblSubParametroService _tblSubParametroService;
        private readonly NotificadorCambios _notificador;

        public TblSubParametroController(
            ITblSubParametroService tblSubParametroService,
            NotificadorCambios notificador)
        {
            _tblSubParametroService = tblSubParametroService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR SUBPARÁMETRO
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblSubParametroDTO subParametro)
        {
            try
            {
                await _tblSubParametroService.Crear(subParametro);

                await _notificador.NotificarAsync(
                    "ParametrosAcademicos");

                return StatusCode(201, new
                {
                    message = "SubParametro creado correctamente"
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
        // ACTUALIZAR SUBPARÁMETRO
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblSubParametroDTO subParametro)
        {
            try
            {
                await _tblSubParametroService.Actualizar(subParametro);

                await _notificador.NotificarAsync(
                    "ParametrosAcademicos");

                return Ok(new
                {
                    message = "SubParametro actualizado correctamente"
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
        // ELIMINAR SUBPARÁMETRO
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(
            [FromBody] DeleteTblSubParametroDTO dto)
        {
            try
            {
                await _tblSubParametroService.Eliminar(
                    dto.lSubParametro_id);

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
        // OBTENER TODOS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var resultado =
                    await _tblSubParametroService.ObtenerSubParametros();

                return Ok(resultado);
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
        // OBTENER POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idSubParametro}")]
        public async Task<IActionResult> ObtenerPorId(
            [FromRoute] int idSubParametro)
        {
            try
            {
                var resultado =
                    await _tblSubParametroService.ObtenerPorId(
                        idSubParametro);

                if (resultado == null)
                {
                    return NotFound(new
                    {
                        message = "Subparámetro no encontrado"
                    });
                }

                return Ok(resultado);
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
        // OBTENER TIPOS DE ÁREA
        // =====================================

        [HttpGet("tipo-area")]
        public async Task<IActionResult> ObtenerTipoArea()
        {
            var resultado =
                await _tblSubParametroService.ObtenerSubparametrosTipoArea();

            return Ok(resultado);
        }

        // =====================================
        // OBTENER TIPOS DE UNIDAD
        // =====================================

        [HttpGet("tipo-unidad")]
        public async Task<IActionResult> ObtenerSubParametrosTipoUnidad()
        {
            try
            {
                var lista =
                    await _tblSubParametroService
                        .ObtenerSubParametrosTipoUnidadAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"Error interno: {ex.Message}");
            }
        }

        // =====================================
        // OBTENER TIPOS DE INSTITUCIÓN
        // =====================================

        [HttpGet("tipo-institucion")]
        public async Task<IActionResult> ObtenerSubParametrosTipoInstitucion()
        {
            try
            {
                var lista =
                    await _tblSubParametroService
                        .ObtenerSubParametrosTipoInstitucionAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"Error interno: {ex.Message}");
            }
        }
    }
}
