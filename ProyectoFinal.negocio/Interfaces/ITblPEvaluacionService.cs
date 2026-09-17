using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblPEvaluacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblPEvaluacionService
    {
        public Task Crear(CreateTblPEvaluacionDTO pEvaluacion);
        public Task Actualizar(UpdateTblPEvaluacionDTO pEvaluacion);
        public Task Eliminar(int idPEvaluacion);
        public Task<ReadTblPEvaluacionDTO> ObtenerPorId(int idPEvaluacion);
        public Task<List<ReadTblPEvaluacionDTO>> ObtenerEvaluaciones();
    }

}