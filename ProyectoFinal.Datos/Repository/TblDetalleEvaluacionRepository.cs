using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblDetalleEvaluacionRepository : ITblDetalleEvaluacionRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblDetalleEvaluacionRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task Actualizar(TblDetalleEvaluacion detalleEvaluacion)
        {
            IEnumerable<int> detalleEvaluacionResult = await _database.GetData<int>("fn_tbldetalleevaluacion_actualizar", new TblDetalleEvaluacion
            {
                lDetalleEvaluacion_id = detalleEvaluacion.lDetalleEvaluacion_id,
                lPEvaluacion_id = detalleEvaluacion.lPEvaluacion_id,
                lEvaluacionJurado_id = detalleEvaluacion.lEvaluacionJurado_id,
                sPuntaje = detalleEvaluacion.sPuntaje
            });
        }
        public async Task Crear(TblDetalleEvaluacion detalleEvaluacion)
        {
            IEnumerable<int> detalleEvaluacionResult = await _database.GetData<int>("fn_tbldetalleevaluacion_crear", new TblDetalleEvaluacion
            {
                lPEvaluacion_id = detalleEvaluacion.lPEvaluacion_id,
                lEvaluacionJurado_id = detalleEvaluacion.lEvaluacionJurado_id,
                sPuntaje = detalleEvaluacion.sPuntaje
            });
        }

        public async Task Eliminar(int idDetalleEvaluacion)
        {
            // ✅ CORREGIDO: Objeto anónimo con un solo parámetro
            await _database.GetData<int>("fn_tbldetalleevaluacion_eliminar", new
            {
                ldetalleevaluacionid = idDetalleEvaluacion
            });
        }

        public async Task<TblDetalleEvaluacion> ObtenerPorId(int idDetalleEvaluacion)
        {
            IEnumerable<TblDetalleEvaluacion> resultado = await _database.GetData<TblDetalleEvaluacion>(
                "fn_tbldetalleevaluacion_obtenerporid",
                new
                {
                    ldetalleevaluacionid = idDetalleEvaluacion
                }
            );

            return resultado.FirstOrDefault();
        }
        public async Task<List<TblDetalleEvaluacion>> ObtenerTodos()
        {
            IEnumerable<TblDetalleEvaluacion> detalleEvaluacionResult = await _database.GetData<TblDetalleEvaluacion>("fn_tbldetalleevaluacion_obtenertodos");
            return detalleEvaluacionResult.ToList();
        }

    }
}
             
 
