using ProyectoFinal.Negocio.DTOs.TblProyecto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblProyectoService
    {
        public Task Crear (CreateTblProyectoDTO proyecto);
        public Task Actualizar(UpdateTblProyectoDTO proyecto);
        public Task Eliminar(int idProyecto);
        public Task<List<ReadTblProyectoDTO>> ObtenerTodos();
        public Task <ReadTblProyectoDTO> ObtenerPorId(int idProyecto);
       public Task<IEnumerable<ReadTblProyectoDTO>> ObtenerPorEventoAsync(int eventoId);

    }
}
