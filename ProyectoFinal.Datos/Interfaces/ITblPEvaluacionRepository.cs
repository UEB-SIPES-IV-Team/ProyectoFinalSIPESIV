using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblPEvaluacionRepository
    {
        // CRUD
        public Task Crear(TblPEvaluacion evaluacion);
        public Task<TblPEvaluacion> ObtenerPorId(int idEvaluacion);
        public Task<List<TblPEvaluacion>> ObtenerTodos();
        public Task Actualizar(TblPEvaluacion evaluacion);
        public Task Eliminar(int idEvaluacion);
    }
}
