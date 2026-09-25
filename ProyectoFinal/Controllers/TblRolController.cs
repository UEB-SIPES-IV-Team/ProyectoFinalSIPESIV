using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblRol;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblRolController : ControllerBase
    {
        private readonly ITblRolService _tblRolService;
        public TblRolController(ITblRolService tblRolService)
        {
            _tblRolService = tblRolService;
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<ReadTblRolDTO>>> ObtenerTodos()
        {
            List<ReadTblRolDTO> roles = await _tblRolService.ObtenerTodos();
            return Ok(roles);
        }
        [HttpGet("ObtenerPorId/{idRol}")]
        public async Task<ActionResult<ReadTblRolDTO>> ObtenerPorId(int idRol)
        {
            ReadTblRolDTO rol = await _tblRolService.ObtenerPorId(idRol);
            return Ok(rol);
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblRolDTO dto)
        {
            try
            {
                await _tblRolService.Eliminar(dto.lRol_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblRolDTO rol)
        {
            await _tblRolService.Crear(rol);
            return StatusCode(201, new { message = "Rol creado correctamente" });
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblRolDTO rol)
        {
            await _tblRolService.Actualizar(rol);
            return StatusCode(201, new { message = "Rol actualizado correctamente" }); ;
        }
    }
}
