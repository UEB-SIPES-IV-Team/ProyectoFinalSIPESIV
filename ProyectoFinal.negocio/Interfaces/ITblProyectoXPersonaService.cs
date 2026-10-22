using ProyectoFinal.Negocio.DTOs.TblEvaluacionVisitante;
using ProyectoFinal.Negocio.DTOs.TblProyectoXPersona;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblProyectoXPersonaService
    {
        public Task Crear(CreateTblProyectoXPersonaDTO proyectoXPersona);
        public Task Actualizar(UpdateTblProyectoXPersonaDTO proyectoXPersona);
        public Task Eliminar(int idProyectoXPersona);
        public Task<List<ReadTblProyectoXPersonaDTO>> ObtenerTodos();
        public Task<ReadTblProyectoXPersonaDTO> ObtenerPorId(int idProyectoXPersona);
        public Task<List<ReadTblProyectoXPersonaDTO>> ObtenerIntegrantesPorProyectoAsync(long idProyecto);
        
    }
}
