using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.Repository
{
    public class TblEvaluacionVisitanteRepository : ITblEvaluacionVisitanteRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblEvaluacionVisitanteRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task Actualizar(TblEvaluacionVisitante entidad)
        {
            await _database.GetData<dynamic>(
                "fn_tblevaluacionvisitante_actualizar",
                new
                {
                    p_levaluacion_id = entidad.lEvaluacion_id,
                    p_lproyecto_id = entidad.lProyecto_id,
                    p_lvisitante_id = entidad.lVisitante_id,
                    p_spuntaje = entidad.sPuntaje,
                    p_sevaluacion_fecha = entidad.sEvaluacion_fecha,
                    p_sevaluacion_desc = entidad.sEvaluacion_desc
                }
            );
        }

        public async Task Crear(TblEvaluacionVisitante evaluacionVisitante)
        {
            await _database.GetData<int>(
                "fn_tblevaluacionvisitante_crear",
                new
                {
                    p_lproyecto_id = evaluacionVisitante.lProyecto_id,
                    p_lvisitante_id = evaluacionVisitante.lVisitante_id,
                    p_spuntaje = evaluacionVisitante.sPuntaje,
                    p_sevaluacion_fecha = evaluacionVisitante.sEvaluacion_fecha,
                    p_sevaluacion_desc = evaluacionVisitante.sEvaluacion_desc
                }
            );
        }

        public async Task Eliminar(int idEvaluacionVisitante)
        {
            await _database.GetData<dynamic>(
                "fn_tblevaluacionvisitante_eliminar",
                new { p_levaluacion_id = idEvaluacionVisitante }
            );
        }

        public async Task<TblEvaluacionVisitante> ObtenerPorId(int idEvaluacionVisitante)
        {
            IEnumerable<TblEvaluacionVisitante> personaResult = await _database.GetData<TblEvaluacionVisitante>(
                "fn_tblevaluacionvisitante_obtenerporid",
                new { p_levaluacion_id = idEvaluacionVisitante }
            );
            return personaResult.FirstOrDefault();
        }

        public async Task<List<TblEvaluacionVisitante>> ObtenerTodos()
        {
            IEnumerable<TblEvaluacionVisitante> personaResult = await _database.GetData<TblEvaluacionVisitante>("fn_tblevaluacionvisitante_obtenertodos");
            return personaResult.ToList();
        }

 
        public async Task<TblEvaluacionVisitante?> ObtenerPorVisitanteYProyecto(int idVisitante, int idProyecto)
        {
            IEnumerable<TblEvaluacionVisitante> result = await _database.GetData<TblEvaluacionVisitante>(
                "fn_tblevaluacionvisitante_obtenerporvisitanteyproyecto",
                new
                {
                    p_lvisitante_id = idVisitante,
                    p_lproyecto_id = idProyecto
                }
            );

            return result?.FirstOrDefault();
        }

      
        public async Task<List<TblEvaluacionVisitante>> ObtenerEvaluacionesPorEvento(int idEvento)
        {
            IEnumerable<TblEvaluacionVisitante> result = await _database.GetData<TblEvaluacionVisitante>(
                "fn_tblevaluacionvisitante_obtenerporevento",
                new { p_levento_id = idEvento }
            );

            return result?.ToList() ?? new List<TblEvaluacionVisitante>();
        }

        public async Task<List<TblEvaluacionVisitante>> ObtenerRecientesPorEvento(int idEvento, int limite = 100)
        {
            IEnumerable<TblEvaluacionVisitante> result = await _database.GetData<TblEvaluacionVisitante>(
                "fn_tblevaluacionvisitante_obtenerporevento",
                new { p_levento_id = idEvento }
            );

            if (result == null) return new List<TblEvaluacionVisitante>();

            // 🟢 ORDENA PRIMERO POR FECHA DE EVALUACIÓN DESCENDENTE
            return result
                .Where(x => x != null && x.lProyecto_id > 0)
                .OrderByDescending(x => x.sEvaluacion_fecha)
                .Take(limite)
                .ToList();
        }

        public async Task<bool> ValidarEventoActivoPorProyecto(int idProyecto)
        {
            IEnumerable<bool> result = await _database.GetData<bool>(
                "fn_tblevento_validaractivo_por_proyecto",
                new { p_lproyecto_id = idProyecto }
            );
            return result.FirstOrDefault();
        }
    }
}