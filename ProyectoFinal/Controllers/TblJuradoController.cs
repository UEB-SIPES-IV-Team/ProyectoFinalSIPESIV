
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblJurado;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblJuradoController : ControllerBase
    {
        private readonly ITblJuradoService _tblJuradoService;
        private readonly NotificadorCambios _notificador;

        public TblJuradoController(
            ITblJuradoService tblJuradoService,
            NotificadorCambios notificador)
        {
            _tblJuradoService = tblJuradoService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR JURADO
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblJuradoDTO jurado)
        {
            try
            {
                int idJurado =
                    await _tblJuradoService.Crear(jurado);

                await _notificador.NotificarAsync("Jurados");

                return StatusCode(201, new
                {
                    message = "Jurado creado correctamente",
                    lJurado_id = idJurado
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
        // ACTUALIZAR JURADO
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblJuradoDTO jurado)
        {
            try
            {
                await _tblJuradoService.Actualizar(jurado);

                await _notificador.NotificarAsync("Jurados");

                return Ok(new
                {
                    message = "Jurado actualizado correctamente"
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
        // ELIMINAR JURADO
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblJuradoDTO dto)
        {
            try
            {
                await _tblJuradoService.Eliminar(dto.lJurado_id);

                await _notificador.NotificarAsync("Jurados");

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
        public async Task<ActionResult> ObtenerPorId(
            int idJurado)
        {
            try
            {
                var jurado =
                    await _tblJuradoService.ObtenerPorId(idJurado);

                if (jurado == null)
                {
                    return NotFound(new
                    {
                        message =
                            $"El jurado con Id {idJurado} no existe"
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
        // OBTENER TODOS LOS JURADOS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerJurados()
        {
            try
            {
                var jurados =
                    await _tblJuradoService.ObtenerJurados();

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
