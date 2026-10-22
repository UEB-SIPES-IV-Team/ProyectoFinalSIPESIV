using ProyectoFinal.Datos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblEvaluacionVisitanteRepository
    {
        public Task Crear(TblEvaluacionVisitante evaluacionVisitante);
        public Task<TblEvaluacionVisitante> ObtenerPorId(int idEvaluacionVisitante);
        public Task<List<TblEvaluacionVisitante>> ObtenerTodos();
        public Task Actualizar(TblEvaluacionVisitante evaluacionVisitante);
        public Task Eliminar(int idEvaluacionVisitante);

        public Task<List<TblEvaluacionVisitante>> ObtenerEvaluacionesPorEvento(int idEvento);
        public Task<List<TblEvaluacionVisitante>> ObtenerRecientesPorEvento(int idEvento, int limite = 5);
        public  Task<bool> ValidarEventoActivoPorProyecto(int idProyecto);
        public Task<TblEvaluacionVisitante?> ObtenerPorVisitanteYProyecto(int idVisitante, int idProyecto);
    }
}