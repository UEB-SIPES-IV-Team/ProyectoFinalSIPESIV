using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblPEvaluacionRepository : ITblPEvaluacionRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblPEvaluacionRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }
        public async Task Actualizar(TblPEvaluacion evaluacion)
        {
            IEnumerable<int> evaluacionResult = await _database.GetData<int>("", new TblPEvaluacion
            {
                lPEvaluacion_id = evaluacion.lPEvaluacion_id,
                sPEvaluacion_nm = evaluacion.sPEvaluacion_nm,
                sPEvaluacion_desc = evaluacion.sPEvaluacion_desc,
                sPEvaluacion_peso = evaluacion.sPEvaluacion_peso
            });
        }
        public async Task Crear(TblPEvaluacion evaluacion)
        {
            IEnumerable<int> evaluacionResult = await _database.GetData<int>("", new TblPEvaluacion
            {
                sPEvaluacion_nm = evaluacion.sPEvaluacion_nm,
                sPEvaluacion_desc = evaluacion.sPEvaluacion_desc,
                sPEvaluacion_peso = evaluacion.sPEvaluacion_peso
            });
        }


        public async Task Eliminar(int idEvaluacion)
        {
            IEnumerable<int> evaluacionResult = await _database.GetData<int>("", new TblPEvaluacion
            {
                lPEvaluacion_id = idEvaluacion
            });
        }

        public async Task<TblPEvaluacion> ObtenerPorId(int idEvaluacion)
        {
            IEnumerable<TblPEvaluacion> evaluacionResult = await _database.GetData<TblPEvaluacion>("", new TblPEvaluacion
            {
                lPEvaluacion_id = idEvaluacion
            });
            return evaluacionResult.FirstOrDefault();
        }

        public async Task<List<TblPEvaluacion>> ObtenerTodos()
        {
            IEnumerable<TblPEvaluacion> evaluacionResult = await _database.GetData<TblPEvaluacion>("");
            return evaluacionResult.ToList();
        }

    }
}
