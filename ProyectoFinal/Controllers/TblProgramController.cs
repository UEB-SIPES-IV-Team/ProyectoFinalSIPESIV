using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Negocio.DTOs.TblArea;
using ProyectoFinal.Negocio.DTOs.TblProgram;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblProgramController : ControllerBase
    {
        private readonly ITblProgramService _tblProgramService;
        public TblProgramController(ITblProgramService tblProgramService)
        {
            _tblProgramService = tblProgramService;
        }
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<ReadTblProgramDTO>>> ObtenerTodos()
        {
            List<ReadTblProgramDTO> programs = await _tblProgramService.ObtenerTodos();
            return Ok(programs);
        }
        [HttpGet("ObtenerPorId/{idProgram}")]
        public async Task<ActionResult<ReadTblProgramDTO>> ObtenerPorId(int idProgram)
        {
            ReadTblProgramDTO program = await _tblProgramService.ObtenerPorId(idProgram);
            return Ok(program);
        }
        [HttpDelete("Eliminar")]
        public async Task<ActionResult> Eliminar([FromBody] DeleteTblProgramDTO dto)
        {
            try
            {
                await _tblProgramService.Eliminar(dto.lProgram_id);
                return Ok(new { message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(CreateTblProgramDTO program)
        {
            await _tblProgramService.Crear(program);
            return StatusCode(201, new { message = "Programa creado correctamente" });
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar(UpdateTblProgramDTO program)
        {
            await _tblProgramService.Actualizar(program);
            return StatusCode(201, new { message = "Programa actualizado correctamente" });
        }
    }
}
