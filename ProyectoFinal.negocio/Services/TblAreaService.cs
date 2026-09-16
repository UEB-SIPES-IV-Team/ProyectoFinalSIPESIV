using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblUnidadOrganizativa;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblAreaService : ITblAreaService
    {
        private readonly ITblAreaRepository _tblAreaRepository;

        public TblAreaService(ITblAreaRepository tblAreaRepository)
        {
            _tblAreaRepository = tblAreaRepository;
        }
        public async Task Actualizar(UpdateTblAreaDTO area)
        {
            TblArea areaActualizada = new TblArea
            {
                lArea_id = area.lArea_id,
                lUnidadOrganizativa_id = area.lUnidadOrganizativa_id,
                sArea_nm = area.sArea_nm,
                sArea_tipo = area.sArea_tipo
            };
            int resultado = await _tblAreaRepository.Actualizar(areaActualizada);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al actualizar el área");
            }
        }
               

        public async Task Crear(CreateTblAreaDTO area)
        {
            TblArea areaCreada = new TblArea
            {
                lUnidadOrganizativa_id = area.lUnidadOrganizativa_id,
                sArea_nm = area.sArea_nm,
                sArea_tipo = area.sArea_tipo
            };
            int resultado = await _tblAreaRepository.Crear(areaCreada);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al crear el área");
            }
        }

        public async Task Eliminar(int idArea)
        {
            int resultado = await _tblAreaRepository.Eliminar(idArea);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al eliminar el área");
            }
        }

        public async Task<ReadTblAreaDTO> ObtenerPorId(int idArea)
        {
            var area = await _tblAreaRepository.ObtenerPorId(idArea);
            if (area == null)
            {
                return null;
            }
            return new ReadTblAreaDTO
            {
                lArea_id = area.lArea_id,
                lUnidadOrganizativa_id = area.lUnidadOrganizativa_id,
                sArea_nm = area.sArea_nm,
                sArea_tipo = area.sArea_tipo
            };
        }

        public async Task<List<ReadTblAreaDTO>> ObtenerAreas()
        {
            var areas = await _tblAreaRepository.ObtenerAreas();
            return areas.Select(a => new ReadTblAreaDTO
            {
                lArea_id = a.lArea_id,
                lUnidadOrganizativa_id = a.lUnidadOrganizativa_id,
                sArea_nm = a.sArea_nm,
                sArea_tipo = a.sArea_tipo
            }).ToList();
        }
    }
}
