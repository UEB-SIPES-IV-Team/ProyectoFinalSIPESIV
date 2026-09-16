using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblAreaService
    {
        public Task Crear(CreateTblAreaDTO area);
        public Task Actualizar(UpdateTblAreaDTO area);
        public Task Eliminar(int idArea);
        public Task<ReadTblAreaDTO> ObtenerPorId(int idArea);
        public Task<List<ReadTblAreaDTO>> ObtenerAreas();
    }
}
