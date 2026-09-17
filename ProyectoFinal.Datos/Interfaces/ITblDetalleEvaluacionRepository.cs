using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblDetalleEvaluacionRepository
    {
        // CRUD
        public Task Crear(TblDetalleEvaluacion detalleEvaluacion);
        public Task<TblDetalleEvaluacion> ObtenerPorId(int idDetalleEvaluacion);
        public Task<List<TblDetalleEvaluacion>> ObtenerTodos();
        public Task Actualizar(TblDetalleEvaluacion detalleEvaluacion);
        public Task Eliminar(int idDetalleEvaluacion);
    }
}
