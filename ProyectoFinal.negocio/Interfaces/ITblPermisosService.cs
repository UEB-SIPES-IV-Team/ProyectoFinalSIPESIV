using ProyectoFinal.Negocio.DTOs.TblPermiso;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblPermisosService
    {
        public Task Crear(TblPermisosCreateDto permisos);
        public Task<TblPermisosReadDto> ObtenerPorId(int idPermisos);
        public Task<List<TblPermisosReadDto>> ObtenerTodos();
        public Task Actualizar(TblPermisosUpdateDto permisos);
        public Task Eliminar(int idPermisos);
        public Task<List<TblPermisosReadDto>> ObtenerPorRol(int idRol);
    }
}
