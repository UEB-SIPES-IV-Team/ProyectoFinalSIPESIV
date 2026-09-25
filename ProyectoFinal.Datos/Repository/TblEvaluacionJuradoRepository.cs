using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblEvaluacionJuradoRepository : ITblEvaluacionJuradoRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblEvaluacionJuradoRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task Actualizar(TblEvaluacionJurado evaluacionJurado)
        {
            IEnumerable<int> personaResult = await _database.GetData<int>("fn_tblevaluacionjurado_actualizar", new TblEvaluacionJurado
            {
                lEvaluacionJurado_id = evaluacionJurado.lEvaluacionJurado_id,
                lProyecto_id = evaluacionJurado.lProyecto_id,
                lJurado_id = evaluacionJurado.lJurado_id,
                sEvaluacionJurado_fecha = evaluacionJurado.sEvaluacionJurado_fecha,
                sEvaluacionJurado_obs = evaluacionJurado.sEvaluacionJurado_obs
            });
        }

        public async Task<int> Crear(TblEvaluacionJurado evaluacion)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblevaluacionjurado_crear", new
            {
                lProyecto_id = evaluacion.lProyecto_id,
                lJurado_id = evaluacion.lJurado_id,
                sEvaluacionJurado_fecha = evaluacion.sEvaluacionJurado_fecha,
                sEvaluacionJurado_obs = evaluacion.sEvaluacionJurado_obs
            });
            return resultado.FirstOrDefault();
        }

        
        public async Task Eliminar(int idEvaluacionJurado)
        {
            await _database.GetData<int>("fn_tblevaluacionjurado_eliminar", new
            {
                levaluacionjuradoid = idEvaluacionJurado
            });
        }

        public async Task<TblEvaluacionJurado> ObtenerPorId(int idEvaluacionJurado)
        {
            IEnumerable<TblEvaluacionJurado> resultado = await _database.GetData<TblEvaluacionJurado>(
                "fn_tblevaluacionjurado_obtenerporid",
                new { p_levaluacionjurado_id = idEvaluacionJurado }
            );

            return resultado.FirstOrDefault();
        }
        public async Task<List<TblEvaluacionJurado>> ObtenerTodos()
        {
            IEnumerable<TblEvaluacionJurado> personaResult = await _database.GetData<TblEvaluacionJurado>("fn_tblevaluacionjurado_obtenertodos");
            return personaResult.ToList();
        }
    }
}
