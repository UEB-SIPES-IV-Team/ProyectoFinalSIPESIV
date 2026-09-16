using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblPremiacion;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblPremiacionService : ITblPremiacionService
    {
        private readonly ITblPremiacionRepository _tblPremiacionRepository;
        public TblPremiacionService(ITblPremiacionRepository tblPremiacionRepository)
        {
            _tblPremiacionRepository = tblPremiacionRepository;
        }
        public async Task Actualizar(UpdateTblPremiacionDTO premiacion)
        {
            TblPremiacion objPremiacion = new TblPremiacion
            {
                lPremiacion_id = premiacion.lPremiacion_id,
                lProyecto_id = premiacion.lProyecto_id,
                lEvento_id = premiacion.lEvento_id,
                sPremiacion_tipo = premiacion.sPremiacion_tipo,
                sPosicion = premiacion.sPosicion
            };
            await _tblPremiacionRepository.Actualizar(objPremiacion);
        }

        public async Task Crear(CreateTblPremiacionDTO premiacion)
        {
            TblPremiacion objPremiacion = new TblPremiacion
            {
                lProyecto_id = premiacion.lProyecto_id,
                lEvento_id = premiacion.lEvento_id,
                sPremiacion_tipo = premiacion.sPremiacion_tipo,
                sPosicion = premiacion.sPosicion
            };
            await _tblPremiacionRepository.Crear(objPremiacion);
        }

        public async Task Eliminar(int idPremiacion)
        {
            await _tblPremiacionRepository.Eliminar(idPremiacion);
        }

        public async Task<ReadTblPremiacionDTO> ObtenerPorId(int idPremiacion)
        {
            var result = await _tblPremiacionRepository.ObtenerPorId(idPremiacion);
            if (result is null) return null;
            return new ReadTblPremiacionDTO
            {
                lPremiacion_id = result.lPremiacion_id,
                lProyecto_id = result.lProyecto_id,
                lEvento_id = result.lEvento_id,
                sPremiacion_tipo = result.sPremiacion_tipo,
                sPosicion = result.sPosicion
            };
        }

        public async Task<List<ReadTblPremiacionDTO>> ObtenerTodos()
        {
            var premiaciones = await _tblPremiacionRepository.ObtenerTodos();
            var PremiacionDTO = new List<ReadTblPremiacionDTO>();

            foreach (var premiacion in premiaciones)
            {
                var premiacionDTO = new ReadTblPremiacionDTO
                {
                    lPremiacion_id = premiacion.lPremiacion_id,
                    lProyecto_id = premiacion.lProyecto_id,
                    lEvento_id = premiacion.lEvento_id,
                    sPremiacion_tipo = premiacion.sPremiacion_tipo,
                    sPosicion = premiacion.sPosicion
                };
                PremiacionDTO.Add(premiacionDTO);
            }

            return PremiacionDTO;
        }
    }
}

