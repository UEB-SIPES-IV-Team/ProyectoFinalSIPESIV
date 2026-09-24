using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblAsignatura;
using ProyectoFinal.Negocio.Interfaces;


namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblAsignaturaController : ControllerBase
    {
        private readonly ITblAsignaturaService _tblAsignaturaService;
        public TblAsignaturaController(ITblAsignaturaService tblAsignaturaService)
        {
            _tblAsignaturaService = tblAsignaturaService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblAsignaturaDTO asignatura)
        {
            try
            {
                await _tblAsignaturaService.Crear(asignatura);
                return StatusCode(201, new { message = "Asignatura creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblAsignaturaDTO asignatura)
        {
            try
            {
                await _tblAsignaturaService.Actualizar(asignatura);
                return StatusCode(201, new { message = "Asignatura actualizada correctamente" });
             
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblAsignaturaDTO dto)
        {
            try
            {
                await _tblAsignaturaService.Eliminar(dto.lAsignatura_id);
                return StatusCode(200, new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId/{idAsignatura}")]
        public async Task<ActionResult> ObtenerPorId(int idAsignatura)
        {
            try
            {
                var asignatura = await _tblAsignaturaService.ObtenerPorId(idAsignatura);
                if (asignatura == null)
                    return NotFound(new { message = $"La asignatura con Id {idAsignatura} no existe" });

                return Ok(asignatura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerAsignaturas()
        {
            try
            {
                return Ok(await _tblAsignaturaService.ObtenerAsignaturas());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
