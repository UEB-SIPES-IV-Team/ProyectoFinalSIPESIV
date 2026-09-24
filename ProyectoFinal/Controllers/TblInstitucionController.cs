using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblInstitucion;
using ProyectoFinal.Negocio.Interfaces;


namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblInstitucionController : ControllerBase
    {
        private readonly ITblInstitucionService _tblInstitucionService;
        public TblInstitucionController(ITblInstitucionService tblInstitucionService)
        {
            _tblInstitucionService = tblInstitucionService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblInstitucionDTO institucion)
        {
            try
            {
                await _tblInstitucionService.Crear(institucion);
                return StatusCode(201, new { message = "Institucion creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblInstitucionDTO institucion)
        {
            try
            {
                await _tblInstitucionService.Actualizar(institucion);
                return StatusCode(201, new { message = "Institucion actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idInstitucion)
        {
            try
            {
                await _tblInstitucionService.Eliminar(idInstitucion);
                return StatusCode(201, new { message = "Institucion eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idInstitucion)
        {
            try
            {
                var institucion = await _tblInstitucionService.ObtenerPorId(idInstitucion);
                if (institucion == null) return NotFound(new { message = $"La institucion con Id {idInstitucion} no existe" });
                return Ok(institucion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerInstituciones()
        {
            try
            {
                return Ok(await _tblInstitucionService.ObtenerInstituciones ());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
