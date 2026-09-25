using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblPermiso;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblPermisosController : ControllerBase
    {
        private readonly ITblPermisosService _tblPermisosService;
        public TblPermisosController(ITblPermisosService tblPermisosService)
        {
            _tblPermisosService = tblPermisosService;
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<TblPermisosReadDto>>> ObtenerObtenerTodos()
        {
            List<TblPermisosReadDto> permisos = await _tblPermisosService.ObtenerTodos();
            return Ok(permisos);
        }
        [HttpGet("ObtenerPorId/{idPermisos}")]
        public async Task<ActionResult<TblPermisosReadDto>> ObtenerPorId(int idPermisos)
        {
            TblPermisosReadDto permisos = await _tblPermisosService.ObtenerPorId(idPermisos);
            return Ok(permisos);
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblPermisoDTO dto)
        {
            try
            {
                await _tblPermisosService.Eliminar(dto.lPermisos_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(TblPermisosCreateDto permisos)
        {
            await _tblPermisosService.Crear(permisos);
            return StatusCode(201, new { message = "Permisos creado correctamente" }); ;
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(TblPermisosUpdateDto permisos)
        {
            await _tblPermisosService.Actualizar(permisos);
            return StatusCode(201, new { message = "Permisos actualizados correctamente" });
        }
    }
}
