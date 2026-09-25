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
            // Usamos GetData<TblEvaluacionVisitante> pero aseguramos la recompilación
            IEnumerable<TblEvaluacionVisitante> personaResult = await _database.GetData<TblEvaluacionVisitante>("fn_tblevaluacionvisitante_obtenertodos");
            return personaResult.ToList();
        }
    }
}