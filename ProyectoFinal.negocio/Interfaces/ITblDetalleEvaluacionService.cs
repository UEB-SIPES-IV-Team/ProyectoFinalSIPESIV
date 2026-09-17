using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblDetalleEvaluacionService
    {
        public Task Crear(CreateTblDetalleEvaluacionDTO DetalleEvaluacion);
        public Task Actualizar(UpdateDetalleEvaluacionDTO DetalleEvaluacion);
        public Task Eliminar(int idDetalleEvaluacion);
        public Task<ReadTblDetalleEvaluacionDTO> ObtenerPorId(int idDetalleEvaluacion);
        public Task<List<ReadTblDetalleEvaluacionDTO>> ObtenerDetallesEvaluacion();
    }
}
