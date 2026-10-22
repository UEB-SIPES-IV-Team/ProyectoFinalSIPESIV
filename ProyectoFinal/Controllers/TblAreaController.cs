
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblAreaController : ControllerBase
    {
        private readonly ITblAreaService _tblAreaService;
        private readonly NotificadorCambios _notificador;

        public TblAreaController(
            ITblAreaService tblAreaService,
            NotificadorCambios notificador)
        {
            _tblAreaService = tblAreaService;
            _notificador = notificador;
        }

        // =====================================
        // CREAR ÁREA
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(
            CreateTblAreaDTO area)
        {
            try
            {
                await _tblAreaService.Crear(area);

                await _notificador.NotificarAsync("Areas");

                return StatusCode(201, new
                {
                    message = "Area creada correctamente"
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
        // ACTUALIZAR ÁREA
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(
            UpdateTblAreaDTO area)
        {
            try
            {
                await _tblAreaService.Actualizar(area);

                await _notificador.NotificarAsync("Areas");

                return Ok(new
                {
                    message = "Actualizado correctamente"
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
        // ELIMINAR ÁREA
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblAreaDTO dto)
        {
            try
            {
                await _tblAreaService.Eliminar(dto.lArea_id);

                await _notificador.NotificarAsync("Areas");

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
        // OBTENER ÁREA POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idArea}")]
        public async Task<ActionResult> ObtenerPorId(
            int idArea)
        {
            try
            {
                var area = await _tblAreaService.ObtenerPorId(idArea);

                if (area == null)
                {
                    return NotFound(new
                    {
                        message = $"El area con Id {idArea} no existe"
                    });
                }

                return Ok(area);
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
        // OBTENER TODAS LAS ÁREAS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerAreas()
        {
            try
            {
                var areas = await _tblAreaService.ObtenerAreas();

                return Ok(areas);
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
