
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblRol;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblRolController : ControllerBase
    {
        private readonly ITblRolService _tblRolService;
        private readonly NotificadorCambios _notificador;

        public TblRolController(
            ITblRolService tblRolService,
            NotificadorCambios notificador)
        {
            _tblRolService = tblRolService;
            _notificador = notificador;
        }

        // =====================================
        // OBTENER TODOS
        // =====================================

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<ReadTblRolDTO>>> ObtenerTodos()
        {
            List<ReadTblRolDTO> roles =
                await _tblRolService.ObtenerTodos();

            return Ok(roles);
        }

        // =====================================
        // OBTENER POR ID
        // =====================================

        [HttpGet("ObtenerPorId/{idRol}")]
        public async Task<ActionResult<ReadTblRolDTO>> ObtenerPorId(int idRol)
        {
            ReadTblRolDTO rol =
                await _tblRolService.ObtenerPorId(idRol);

            return Ok(rol);
        }

        // =====================================
        // ELIMINAR
        // =====================================

        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar(
            [FromBody] DeleteTblRolDTO dto)
        {
            try
            {
                await _tblRolService.Eliminar(dto.lRol_id);

                await _notificador.NotificarAsync("Roles");

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
        // CREAR
        // =====================================

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblRolDTO rol)
        {
            await _tblRolService.Crear(rol);

            await _notificador.NotificarAsync("Roles");

            return StatusCode(201, new
            {
                message = "Rol creado correctamente"
            });
        }

        // =====================================
        // ACTUALIZAR
        // =====================================

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblRolDTO rol)
        {
            await _tblRolService.Actualizar(rol);

            await _notificador.NotificarAsync("Roles");

            return Ok(new
            {
                message = "Rol actualizado correctamente"
            });
        }
    }
}
