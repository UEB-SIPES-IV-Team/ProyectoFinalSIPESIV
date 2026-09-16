using Microsoft.Extensions.Logging;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblEvento;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblEventoService : ITblEventoService
    {
        private readonly ITblEventoRepository _tblEventoRepository;
        public TblEventoService(ITblEventoRepository tblEventoRepository)
        {
            _tblEventoRepository = tblEventoRepository;
        }
        public async Task Actualizar(UpdateTblEventoDTO evento)
        {
            TblEvento objEvento = new TblEvento
            {
                lEvento_id = evento.lEvento_id,
                lInstitucion_id = evento.lInstitucion_id,
                sEvento_nm = evento.sEvento_nm,
                sAnio= evento.sAnio,
                sGestion = evento.sGestion,
                sFecha_ini = evento.sFecha_ini,
                sFecha_fin = evento.sFecha_fin,
                sEvento_estado = evento.sEvento_estado
            };
            await _tblEventoRepository.Actualizar(objEvento);
        }

        public async Task Crear(CreateTblEventoDTO evento)
        {
            TblEvento objEvento = new TblEvento
            {
                lInstitucion_id = evento.lInstitucion_id,
                sEvento_nm = evento.sEvento_nm,
                sAnio = evento.sAnio,
                sGestion = evento.sGestion,
                sFecha_ini = evento.sFecha_ini,
                sFecha_fin = evento.sFecha_fin,
                sEvento_estado = evento.sEvento_estado
            };
            await _tblEventoRepository.Crear(objEvento);
        }

        public async Task Eliminar(int idEvento)
        {
            await _tblEventoRepository.Eliminar(idEvento);
        }

        public async Task<ReadTblEventoDTO> ObtenerPorId(int idEvento)
        {
            var result = await _tblEventoRepository.ObtenerPorId(idEvento);
            if (result is null) return null;
            return new ReadTblEventoDTO
            {
                lEvento_id = result.lEvento_id,
                lInstitucion_id = result.lInstitucion_id,
                sEvento_nm = result.sEvento_nm,
                sAnio = result.sAnio,
                sGestion = result.sGestion,
                sFecha_ini = result.sFecha_ini,
                sFecha_fin = result.sFecha_fin,
                sEvento_estado = result.sEvento_estado
            };
        }

        public async Task<List<ReadTblEventoDTO>> ObtenerTodos()
        {
            var eventos = await _tblEventoRepository.ObtenerTodos();
            var EventosDTO = new List<ReadTblEventoDTO>();

            foreach (var evento in eventos)
            {
                var eventoDTO = new ReadTblEventoDTO
                {
                    lEvento_id = evento.lEvento_id,
                    lInstitucion_id = evento.lInstitucion_id,
                    sEvento_nm = evento.sEvento_nm,
                    sAnio = evento.sAnio,
                    sGestion = evento.sGestion,
                    sFecha_ini = evento.sFecha_ini,
                    sFecha_fin = evento.sFecha_fin,
                    sEvento_estado = evento.sEvento_estado
                };
                EventosDTO.Add(eventoDTO);
            }

            return EventosDTO;
        }
    }
}
