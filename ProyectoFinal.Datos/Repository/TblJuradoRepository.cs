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
            IEnumerable<int> juradoResult = await _database.GetData<int>("", new TblJurado
            {
                lJurado_id = jurado.lJurado_id,
                lPersona_id = jurado.lPersona_id,
                sJurado_institucion = jurado.sJurado_institucion,
                sJurado_gral = jurado.sJurado_gral
            });
        }
        public async Task Crear(TblJurado jurado)
        {
            IEnumerable<int> juradoResult = await _database.GetData<int>("", new TblJurado
            {
                lPersona_id = jurado.lPersona_id,
                sJurado_institucion = jurado.sJurado_institucion,
                sJurado_gral = jurado.sJurado_gral
            });
        }
        public async Task Eliminar(int idJurado)
        {
            IEnumerable<int> juradoResult = await _database.GetData<int>("", new TblJurado
            {
                lJurado_id = idJurado
            });
        }

        public async Task<TblJurado> ObtenerPorId(int idJurado)
        {
            IEnumerable<TblJurado> juradoResult = await _database.GetData<TblJurado>("", new TblJurado
            {
                lJurado_id = idJurado
            });
            return juradoResult.FirstOrDefault();
        }

        public async Task<List<TblJurado>> ObtenerTodos()
        {
            IEnumerable<TblJurado> juradoResult = await _database.GetData<TblJurado>("");
            return juradoResult.ToList();
        }
    }
}
