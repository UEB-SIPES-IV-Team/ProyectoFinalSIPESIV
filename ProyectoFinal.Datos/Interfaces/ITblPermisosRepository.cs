using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblPermisosRepository
    {
        public Task Crear(TblPermisos permisos);
        public Task<TblPermisos> ObtenerPorId(int idPermisos);
        public Task<List<TblPermisos>> ObtenerTodos();
        public Task Actualizar(TblPermisos permisos);
        public Task Eliminar(int idPermisos);

        public Task<List<TblPermisos>> ObtenerPorRol(int idRol);
    }
}
