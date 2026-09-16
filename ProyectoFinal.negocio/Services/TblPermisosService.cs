using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblPermiso;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblPermisosService : ITblPermisosService
    {
        private readonly ITblPermisosRepository _tblPermisosRepository;
        public TblPermisosService(ITblPermisosRepository tblPermisosRepository)
        {
            _tblPermisosRepository = tblPermisosRepository;
        }

        public async Task Actualizar(TblPermisosUpdateDto permisos)
        {
            TblPermisos objPermisos = new TblPermisos
            {
                lPermisos_id = permisos.lPermisos_id,
                lRol_id = permisos.lRol_id,
                lProgram_id = permisos.lProgram_id,
                insertar = permisos.insertar,
                actualizar = permisos.actualizar,
                consultar = permisos.consultar,
                eliminar = permisos.eliminar
            };
            await _tblPermisosRepository.Actualizar(objPermisos);
        }

        public async Task Crear(TblPermisosCreateDto permisos)
        {
            TblPermisos objPermisos = new TblPermisos
            {
             
                lRol_id = permisos.lRol_id,
                lProgram_id = permisos.lProgram_id,
                insertar = permisos.insertar,
                actualizar = permisos.actualizar,
                consultar = permisos.consultar,
                eliminar = permisos.eliminar
            };
            await _tblPermisosRepository.Crear(objPermisos);
        }

        public async Task Eliminar(int idPermisos)
        {
            await _tblPermisosRepository.Eliminar(idPermisos);
        }

        public async Task<TblPermisosReadDto> ObtenerPorId(int idPermisos)
        {
            var result = await _tblPermisosRepository.ObtenerPorId(idPermisos);
            if (result is null) throw new InvalidOperationException("Permisos not found");

            return new TblPermisosReadDto
            {
                lPermisos_id = result.lPermisos_id,
                lRol_id = result.lRol_id,
                lProgram_id = result.lProgram_id,
                insertar = result.insertar,
                actualizar = result.actualizar,
                consultar = result.consultar,
                eliminar = result.eliminar
            };
        }

        public async Task<List<TblPermisosReadDto>> ObtenerTodos()
        {
            var permisos = await _tblPermisosRepository.ObtenerTodos();
            var permisosDTO = new List<TblPermisosReadDto>();

            foreach (var permiso in permisos)
            {
                var permisoDTO = new TblPermisosReadDto
                {
                    lPermisos_id = permiso.lPermisos_id,
                    lRol_id = permiso.lRol_id,
                    lProgram_id = permiso.lProgram_id,
                    insertar = permiso.insertar,
                    actualizar = permiso.actualizar,
                    consultar = permiso.consultar,
                    eliminar = permiso.eliminar
                };
                permisosDTO.Add(permisoDTO);
            }

            return permisosDTO;
        }

     
    }
}
