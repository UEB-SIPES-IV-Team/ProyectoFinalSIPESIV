using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.DTOs.TblInstitucion;
using ProyectoFinal.Negocio.DTOs.TblParametro;
using ProyectoFinal.Negocio.DTOs.TblSubParametro;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblInstitucionService : ITblInstitucionService
    {
        private readonly ITblInstitucionRepository _tblInstitucionRepository;
        public TblInstitucionService(ITblInstitucionRepository tblInstitucionRepository)
        {
            _tblInstitucionRepository = tblInstitucionRepository;
        }
        public async Task Actualizar(UpdateTblInstitucionDTO institucion)
        {
            TblInstitucion institucionActualizada = new TblInstitucion
            {
                lInstitucion_id = institucion.lInstitucion_id,
                sInstitucion_nm = institucion.sInstitucion_nm,
                sInstitucion_slug = institucion.sInstitucion_slug
            };
            int resultado = await _tblInstitucionRepository.Actualizar(institucionActualizada);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al actualizar la institución");
            }
        }

   
        public async Task Crear(CreateTblInstitucionDTO institucion)
        {
            TblInstitucion institucionCreada = new TblInstitucion
            {
                sInstitucion_nm = institucion.sInstitucion_nm,
                sInstitucion_slug = institucion.sInstitucion_slug
            };
            int resultado = await _tblInstitucionRepository.Crear(institucionCreada);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al crear la institución");
            }
        }

           
        public async Task Eliminar(int idInstitucion)
        {
            int resultado = await _tblInstitucionRepository.Eliminar(idInstitucion);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al eliminar la institución");
            }
        }

        public async Task<ReadTblInstitucionDTO> ObtenerPorId(int idInstitucion)
        {
            var institucion = await _tblInstitucionRepository.ObtenerPorId(idInstitucion);
            if (institucion == null)
            {
                return null;
            }
            return new ReadTblInstitucionDTO
            {
                lInstitucion_id = institucion.lInstitucion_id,
                sInstitucion_nm = institucion.sInstitucion_nm,
                sInstitucion_slug = institucion.sInstitucion_slug
            };
        }

        public async Task<List<ReadTblInstitucionDTO>> ObtenerInstituciones()
        {
            var instituciones = await _tblInstitucionRepository.ObtenerInstituciones();
            return instituciones.Select(instit => new ReadTblInstitucionDTO
            {
                lInstitucion_id = instit.lInstitucion_id,
                sInstitucion_nm = instit.sInstitucion_nm,
                sInstitucion_slug = instit.sInstitucion_slug
            }).ToList();
        }
    }
}

