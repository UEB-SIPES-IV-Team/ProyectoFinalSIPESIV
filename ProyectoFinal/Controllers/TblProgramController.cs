
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblProgram;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblProgramController : ControllerBase
    {
        private readonly ITblProgramService _tblProgramService;
        private readonly NotificadorCambios _notificador;
        private readonly ILogger<TblProgramController> _logger;

        public TblProgramController(
            ITblProgramService tblProgramService,
            NotificadorCambios notificador,
            ILogger<TblProgramController> logger)
        {
            _tblProgramService = tblProgramService;
            _notificador = notificador;
            _logger = logger;
        }

        // =====================================
        // OBTENER TODOS LOS PROGRAMAS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<ReadTblProgramDTO>>> ObtenerTodos()
        {
            try
            {
                List<ReadTblProgramDTO> programs =
                    await _tblProgramService.ObtenerTodos();

                return Ok(programs);
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
        // OBTENER PROGRAMA POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idProgram}")]
        public async Task<ActionResult<ReadTblProgramDTO>> ObtenerPorId(
            int idProgram)
        {
            try
            {
                ReadTblProgramDTO program =
                    await _tblProgramService.ObtenerPorId(idProgram);

                if (program == null)
                {
                    return NotFound(new
                    {
                        message = $"El programa con ID {idProgram} no existe"
                    });
                }

                return Ok(program);
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
        // CREAR PROGRAMA
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblProgramDTO program)
        {
            try
            {
                await _tblProgramService.Crear(program);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarCambiosAsync();

            return StatusCode(201, new
            {
                message = "Programa creado correctamente"
            });
        }

        // =====================================
        // ACTUALIZAR PROGRAMA
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblProgramDTO program)
        {
            try
            {
                await _tblProgramService.Actualizar(program);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarCambiosAsync();

            return Ok(new
            {
                message = "Programa actualizado correctamente"
            });
        }

        // =====================================
        // ELIMINAR PROGRAMA
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblProgramDTO dto)
        {
            try
            {
                await _tblProgramService.Eliminar(
                    dto.lProgram_id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }

            await NotificarCambiosAsync();

            return Ok(new
            {
                message = "Eliminado correctamente"
            });
        }

        // =====================================
        // NOTIFICAR CAMBIOS POR SIGNALR
        // =====================================

        private async Task NotificarCambiosAsync()
        {
            try
            {
                await _notificador.NotificarAsync("Programas");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "No se pudo enviar la notificación SignalR del módulo Programas.");
            }
        }
    }
}
