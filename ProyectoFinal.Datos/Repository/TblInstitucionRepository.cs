using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Datos.Repository
{
    public class TblInstitucionRepository : ITblInstitucionRepository
    {
        private readonly ProyectoFinalDatabase _database;

        public TblInstitucionRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task<int> Crear(TblInstitucion institucion)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblinstitucion_crear", new
            {
                p_sinstitucion_tipo = institucion.sInstitucion_tipo,
                p_sinstitucion_slug = institucion.sInstitucion_slug,
                p_sinstitucion_nm = institucion.sInstitucion_nm,
                p_binstitucion_estado = true
            });
            return resultado.FirstOrDefault();
        }

        public async Task<int> Actualizar(TblInstitucion entidad)
        {
            var result = await _database.GetData<int?>("fn_tblinstitucion_actualizar", new
            {
                p_linstitucion_id = entidad.lInstitucion_id,
                p_sinstitucion_tipo = entidad.sInstitucion_tipo,
                p_sinstitucion_nm = entidad.sInstitucion_nm,
                p_sinstitucion_slug = entidad.sInstitucion_slug,
                p_binstitucion_estado = entidad.bInstitucion_estado
            });

            return result.FirstOrDefault() ?? 0;
        }

        public async Task<int> Eliminar(int idInstitucion)
        {
            IEnumerable<int?> resultado = await _database.GetData<int?>("fn_tblinstitucion_eliminar", new
            {
                p_linstitucion_id = idInstitucion
            });

            return resultado.FirstOrDefault() ?? 0;
        }

        public async Task<List<TblInstitucion>> ObtenerInstituciones()
        {
            IEnumerable<TblInstitucion> resultado = await _database.GetData<TblInstitucion>("fn_tblinstitucion_obtenertodos");
            return resultado.ToList();
        }

        public async Task<TblInstitucion?> ObtenerPorId(int idInstitucion)
        {
            IEnumerable<TblInstitucion> resultado = await _database.GetData<TblInstitucion>("fn_tblinstitucion_obtener_por_id", new
            {
                p_linstitucion_id = idInstitucion
            });
            return resultado.FirstOrDefault();
        }
    }
}