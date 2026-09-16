using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblProyectoXPersona;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblProyectoXPersonaService : ITblProyectoXPersonaService
    {
        private readonly ITblProyectoXPersonaRepository _tblProyectoXPersonaRepository;
        public TblProyectoXPersonaService(ITblProyectoXPersonaRepository tblProyectoXPersonaRepository)
        {
            _tblProyectoXPersonaRepository = tblProyectoXPersonaRepository;
        }
        public async Task Actualizar(UpdateTblProyectoXPersonaDTO proyectoXPersona)
        {
            TblProyectoXPersona objProyectoXPersona = new TblProyectoXPersona
            {
                lProyectoXPersona_id = proyectoXPersona.lProyectoXPersona_id,
                lProyecto_id = proyectoXPersona.lProyecto_id,
                lPersona_id = proyectoXPersona.lPersona_id,
            };
            await _tblProyectoXPersonaRepository.Actualizar(objProyectoXPersona);
        }

        public async Task Crear(CreateTblProyectoXPersonaDTO proyectoXPersona)
        {
            TblProyectoXPersona objProyectoXPersona = new TblProyectoXPersona
            {
                lProyecto_id = proyectoXPersona.lProyecto_id,
                lPersona_id = proyectoXPersona.lPersona_id
            };
            await _tblProyectoXPersonaRepository.Crear(objProyectoXPersona);
        }

        public async Task Eliminar(int idProyectoXPersona)
        {
            await _tblProyectoXPersonaRepository.Eliminar(idProyectoXPersona);
        }

        public async Task<ReadTblProyectoXPersonaDTO> ObtenerPorId(int idProyectoXPersona)
        {
            var result = await _tblProyectoXPersonaRepository.ObtenerPorId(idProyectoXPersona);
            if (result is null) return null;
            return new ReadTblProyectoXPersonaDTO
            {
                lProyectoXPersona_id = result.lProyectoXPersona_id,
                lProyecto_id = result.lProyecto_id,
                lPersona_id = result.lPersona_id
            };
        }

        public async Task<List<ReadTblProyectoXPersonaDTO>> ObtenerTodos()
        {
            var proyectoXPersonas = await _tblProyectoXPersonaRepository.ObtenerTodos();
            var ProyectoXPersonasDTO = new List<ReadTblProyectoXPersonaDTO>();

            foreach (var proyectoXPersona in proyectoXPersonas)
            {
                var proyectoXPersonaDTO = new ReadTblProyectoXPersonaDTO
                {
                    lProyectoXPersona_id = proyectoXPersona.lProyectoXPersona_id,
                    lProyecto_id = proyectoXPersona.lProyecto_id,
                    lPersona_id = proyectoXPersona.lPersona_id,
                };
                ProyectoXPersonasDTO.Add(proyectoXPersonaDTO);
            }

            return ProyectoXPersonasDTO;
        }
    }
}
