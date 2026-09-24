using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.Repository
{
    public class TblSubParametroRepository : ITblSubParametroRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblSubParametroRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task<int> Actualizar(TblSubParametro subParametro)
        {
           
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblsubparametro_actualizar", new
            {
                lSubParametro_id = subParametro.lSubParametro_id,
                lParametro_id = subParametro.lParametro_id,
                sSubParametro_nm = subParametro.sSubParametro_nm,
                sSubParametro_desc = subParametro.sSubParametro_desc
            });

            return resultado.FirstOrDefault();
        }

        public async Task<int> Crear(TblSubParametro subParametro)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblsubparametro_crear", new
            {
                lParametro_id = subParametro.lParametro_id,
                sSubParametro_nm = subParametro.sSubParametro_nm,
                sSubParametro_desc = subParametro.sSubParametro_desc
            });
            return resultado.FirstOrDefault();
        }

        public async Task<int> Eliminar(int idSubParametro)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblsubparametro_eliminar", new
            {
                lSubParametro_id = idSubParametro
            });

            return resultado.FirstOrDefault();
        }

        public async Task<TblSubParametro> ObtenerPorId(int idSubParametro)
        {
            IEnumerable<TblSubParametro> resultado = await _database.GetData<TblSubParametro>("fn_tblsubparametro_obtenerporid", new
            {
                lSubParametro_id = idSubParametro
            });
            return resultado.FirstOrDefault();
        }

        public async Task<List<TblSubParametro>> ObtenerSubParametros()
        {
            IEnumerable<TblSubParametro> resultado = await _database.GetData<TblSubParametro>("fn_tblsubparametro_obtenertodos");
            return resultado.ToList();
        }
    }
}