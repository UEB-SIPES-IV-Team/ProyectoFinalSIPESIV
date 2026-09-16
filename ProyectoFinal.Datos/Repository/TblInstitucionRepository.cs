using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblInstitucionRepository :ITblInstitucionRepository
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
                lParametro_id = institucion.lParametro_id,
                sInstitucion_slug = institucion.sInstitucion_slug,
                sInstitucion_nm = institucion.sInstitucion_nm,
            });
            return resultado.FirstOrDefault();
        }

        public async Task<int> Actualizar(TblInstitucion institucion)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblinstitucion_actualizar", new TblInstitucion
            {
                lInstitucion_id = institucion.lInstitucion_id,
                lParametro_id = institucion.lParametro_id,
                sInstitucion_slug = institucion.sInstitucion_slug,
                sInstitucion_nm = institucion.sInstitucion_nm,
            });
            return resultado.FirstOrDefault();
        }

        public async Task<int> Eliminar(int institucion)
        {
            IEnumerable<int> resultado = await _database.GetData<int>("fn_tblinstitucion_eliminar", new
            {
                lInstitucion_id = institucion
            });
            return resultado.FirstOrDefault();
        }
        public async Task<List<TblInstitucion>> ObtenerInstituciones()
        {
            IEnumerable<TblInstitucion> resultado = await _database.GetData<TblInstitucion>("fn_tblinstitucion_obtenertodos");
            return resultado.ToList();
        }

        public async Task<TblInstitucion> ObtenerPorId(int institucion)
        {
            IEnumerable<TblInstitucion> resultado = await _database.GetData<TblInstitucion>("fn_tblinstitucion_obtener_por_id", new
            {
                lInstitucion_id = institucion
            });
            return resultado.FirstOrDefault();
        }

    }
}

           
   
