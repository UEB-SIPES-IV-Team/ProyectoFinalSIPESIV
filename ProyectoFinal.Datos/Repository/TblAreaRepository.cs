using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblAreaRepository : ITblAreaRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblAreaRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task<int> Crear(TblArea area)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblarea_crear", new
            {
                lUnidadOrganizativa = area.lUnidadOrganizativa_id,
                sArea_nm = area.sArea_nm,
                sArea_Tipo = area.sArea_tipo,
            });
            return resultado.FirstOrDefault();
        }

        public async Task<int> Actualizar(TblArea area)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblarea_actualizar", new TblArea
            {
                lArea_id = area.lArea_id,
                lUnidadOrganizativa_id = area.lUnidadOrganizativa_id,
                sArea_nm = area.sArea_nm,
                sArea_tipo = area.sArea_tipo
            });
            return resultado.FirstOrDefault();

        }

        public async Task<int> Eliminar(int area)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblarea_eliminar", new
            {
                larea_id = area
            });
            return resultado.FirstOrDefault();
        }
        public async Task<List<TblArea>> ObtenerAreas()
        {
            IEnumerable<TblArea> resultado = await _database.GetData<TblArea>("fn_tblarea_obtener_areas");
            return resultado.ToList();
        }

        public async Task<TblArea> ObtenerPorId(int area)
        {
            IEnumerable<TblArea> resultado = await _database.GetData<TblArea>("fn_tblarea_obtener_por_id", new
            {
                lArea_id = area
            });
            return resultado.FirstOrDefault();
        }

    }
}
