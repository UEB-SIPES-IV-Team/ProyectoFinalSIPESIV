using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblEvaluacionJuradoRepository
    {
        public Task<int> Crear(TblEvaluacionJurado evaluacion); // Cambiar Task por Task<int>
        public Task Actualizar(TblEvaluacionJurado evaluacionJurado);
        public Task Eliminar(int idEvaluacionJurado);
        public Task<TblEvaluacionJurado> ObtenerPorId(int idEvaluacionJurado);
        public Task<List<TblEvaluacionJurado>> ObtenerTodos();
    }
}
