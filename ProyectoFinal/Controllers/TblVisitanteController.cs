using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblVisitante;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblVisitanteController : ControllerBase
    {
        private readonly ITblVisitanteService _tblVisitanteService;
        public TblVisitanteController(ITblVisitanteService tblVisitanteService)
        {
            _tblVisitanteService = tblVisitanteService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblVisitanteDTO visitante)
        {
            try
            {
                await _tblVisitanteService.Crear(visitante);
                return StatusCode(201, new { message = "Visitante creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblVisitanteDTO visitante)
        {
            try
            {
                await _tblVisitanteService.Actualizar(visitante);
                return StatusCode(201, new { message = "Visitante actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar([FromBody] DeleteTblVisitanteDTO dto)
        {
            try
            {
                await _tblVisitanteService.Eliminar(dto.lVisitante_id);
                return Ok(new { message = "Visitante eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId/{idVisitante}")]
        public async Task<ActionResult> ObtenerPorId(int idVisitante)
        {
            try
            {
                var visitante = await _tblVisitanteService.ObtenerPorId(idVisitante);
                if (visitante == null) return NotFound(new { message = $"El visitante con Id {idVisitante} no existe" });
                return Ok(visitante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerVisitantes()
        {
            try
            {
                return Ok(await _tblVisitanteService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("ObtenerPorCi")]
        public async Task<IActionResult> ObtenerPorCi([FromQuery] string ci, [FromQuery] int eventoId)
        {
            // Ver los valores exactos que llegan a la API
            Console.WriteLine($"===> Consulta recibida en API -> CI: '{ci}', EventoId: {eventoId}");

            var visitante = await _tblVisitanteService.ObtenerPorCiYEvento(ci, eventoId);

            if (visitante == null)
            {
                Console.WriteLine($"===> Resultado: NULL (No encontrado en BD)");
                return NotFound(new { message = "No se encontró ningún visitante registrado con ese Carnet para este evento." });
            }

            Console.WriteLine($"===> Resultado: Encontrado -> {visitante.sVisitante_nm}");
            return Ok(visitante);
        }
    }
}
