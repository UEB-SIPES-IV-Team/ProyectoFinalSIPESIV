using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblJuradoXUnidadOrganzativa;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblJuradoXUnidadOrganizativaController : ControllerBase
    {
        private readonly ITblJuradoXUnidadOrganizativaService _tblJuradoXUnidadOrganizativaService;
        public TblJuradoXUnidadOrganizativaController(ITblJuradoXUnidadOrganizativaService tblJuradoXUnidadOrganizativaService)
        {
            _tblJuradoXUnidadOrganizativaService = tblJuradoXUnidadOrganizativaService;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblJuradoXUnidadOrganzativaDTO jurado)
        {
            try
            {
                await _tblJuradoXUnidadOrganizativaService.Crear(jurado);
                return StatusCode(201, new { message = "Jurado U.O. creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblJuradoXUnidadOrganzativaDTO jurado)
        {
            try
            {
                await _tblJuradoXUnidadOrganizativaService.Actualizar(jurado);
                return StatusCode(201, new { message = "Jurado U.O. actualizado  correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(int idJurado)
        {
            try
            {
                await _tblJuradoXUnidadOrganizativaService.Eliminar(idJurado);
                return StatusCode(201, new { message = "Jurado U.O. eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerPorId")]
        public async Task<ActionResult> ObtenerPorId(int idJurado)
        {
            try
            {
                var jurado = await _tblJuradoXUnidadOrganizativaService.ObtenerPorId(idJurado);
                if (jurado == null) return NotFound(new { message = $"El jurado con Id {idJurado} no existe" });
                return Ok(jurado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult> ObtenerJurados()
        {
            try
            {
                return Ok(await _tblJuradoXUnidadOrganizativaService.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
