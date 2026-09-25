using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblJuradoRepository : ITblJuradoRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblJuradoRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }
        public async Task Actualizar(TblJurado jurado)
        {
            IEnumerable<int> juradoResult = await _database.GetData<int>("fn_tbljurado_actualizar", new TblJurado
            {
                lJurado_id = jurado.lJurado_id,
                lPersona_id = jurado.lPersona_id,
                sJurado_institucion = jurado.sJurado_institucion,
                sJurado_gral = jurado.sJurado_gral
            });
        }
        public async Task<int> Crear(TblJurado jurado)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tbljurado_crear", new
            {
                ljurado_id = jurado.lJurado_id,
                lpersona_id = jurado.lPersona_id,
                sjurado_institucion = jurado.sJurado_institucion,
                sjurado_gral = jurado.sJurado_gral
            });
            return resultado.FirstOrDefault();
        }
        public async Task Eliminar(int idJurado)
        {
            await _database.GetData<dynamic>("fn_tbljurado_eliminar", new
            {
                p_ljurado_id = idJurado
            });
        }

        public async Task<TblJurado> ObtenerPorId(int idJurado)
        {
            IEnumerable<TblJurado> result = await _database.GetData<TblJurado>(
                "fn_tbljurado_obtenerporid",
                new { p_ljurado_id = idJurado } 
            );

            return result.FirstOrDefault();
        }

        public async Task<List<TblJurado>> ObtenerTodos()
        {
            IEnumerable<TblJurado> juradoResult = await _database.GetData<TblJurado>("fn_tbljurado_obtenertodos");
            return juradoResult.ToList();
        }
    }
}
