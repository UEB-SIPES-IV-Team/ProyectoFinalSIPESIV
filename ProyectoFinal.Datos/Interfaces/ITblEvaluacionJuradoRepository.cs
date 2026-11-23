using ProyectoFinal.Datos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblEvaluacionJuradoRepository
    {
        public Task<int> Crear(TblEvaluacionJurado evaluacion);
        public Task Actualizar(TblEvaluacionJurado evaluacionJurado);
        public Task Eliminar(int idEvaluacionJurado);
        public Task<TblEvaluacionJurado> ObtenerPorId(int idEvaluacionJurado);
        public Task<List<TblEvaluacionJurado>> ObtenerTodos();
        public Task<List<TblEvaluacionJurado>> ObtenerPorJurado(int juradoId);
    }
}