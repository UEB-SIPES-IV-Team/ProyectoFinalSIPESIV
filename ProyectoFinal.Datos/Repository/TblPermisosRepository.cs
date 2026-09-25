using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Repository
{
    public class TblPermisosRepository : ITblPermisosRepository
    {
        private readonly ProyectoFinalDatabase _database;
        public TblPermisosRepository(ProyectoFinalDatabase database)
        {
            _database = database;
        }

        public async Task Actualizar(TblPermisos permiso)
        {
            await _database.GetData<int>("fn_tblpermisos_actualizar", new
            {
                p_lpermisos_id = permiso.lPermisos_id,
                p_lrol_id = permiso.lRol_id,
                p_lprogram_id = permiso.lProgram_id,
                p_insertar = permiso.insertar,
                p_actualizar = permiso.actualizar,
                p_consultar = permiso.consultar,
                p_eliminar = permiso.eliminar
            });
        }

        public async Task Crear(TblPermisos permisos)
        {
            IEnumerable<int> permisosResult = await _database.GetData<int>("fn_tblpermisos_crear", new
            {
                p_lRol_id = permisos.lRol_id,
                p_lProgram_id = permisos.lProgram_id,
                p_Insertar = permisos.insertar,
                p_Actualizar = permisos.actualizar,
                p_Consultar = permisos.consultar,
                p_Eliminar = permisos.eliminar
            });
        }

        public async Task Eliminar(int idPermisos)
        {
            IEnumerable<int> permisosResult = await _database.GetData<int>("fn_tblpermisos_eliminar", new
            {
                p_lPermisos_id = idPermisos
            });
        }

        public async Task<TblPermisos> ObtenerPorId(int idPermisos)
        {
            IEnumerable<TblPermisos> permisosResult = await _database.GetData<TblPermisos>("fn_tblpermisos_obtenerporid", new
            {
                p_lPermisos_id = idPermisos
            });
            return permisosResult.FirstOrDefault();
        }

        public async Task<List<TblPermisos>> ObtenerTodos()
        {
            IEnumerable<TblPermisos> permisosResult = await _database.GetData<TblPermisos>("fn_tblpermisos_obtenertodos");
            return permisosResult.ToList();
        }
    }
}
