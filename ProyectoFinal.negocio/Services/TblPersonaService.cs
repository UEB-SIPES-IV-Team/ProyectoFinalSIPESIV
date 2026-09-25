using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblPersona;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static ProyectoFinal.Negocio.Services.TblPersonaService;

namespace ProyectoFinal.Negocio.Services
{
         public class TblPersonaService : ITblPersonaService
    {
            private readonly ITblPersonaRepository _tblPersonaRepository;
            public TblPersonaService(ITblPersonaRepository tblPersonaRepository)
            {
                _tblPersonaRepository = tblPersonaRepository;
            }
            public async Task Actualizar(UpdateTblPersonaDTO persona)
            {
                TblPersona objPersona = new TblPersona
                {
                    lPersona_id = persona.lPersona_id,
                    sPersona_aps = persona.sPersona_aps,
                    sPersona_email = persona.sPersona_email,
                    sPersona_nm = persona.sPersona_nm,
                    sPersona_telf = persona.sPersona_telf,
                    sPersona_tipo_persona = persona.sPersona_tipo_persona
                };
                await _tblPersonaRepository.Actualizar(objPersona);
            }

            public async Task Crear(CreateTblPersonaDTO persona)
            {
                TblPersona objPersona = new TblPersona
                {
                    sPersona_aps = persona.sPersona_aps,
                    sPersona_email = persona.sPersona_email,
                    sPersona_nm = persona.sPersona_nm,
                    sPersona_telf = persona.sPersona_telf,
                    sPersona_tipo_persona = persona.sPersona_tipo_persona
                };
                await _tblPersonaRepository.Crear(objPersona);
            }

            public async Task Eliminar(int idPersona)
            {
                await _tblPersonaRepository.Eliminar(idPersona);
            }

        public async Task<ReadTblPersonaDTO> ObtenerPorId(int idPersona)
        {
            var result = await _tblPersonaRepository.ObtenerPorId(idPersona);

      
            if (result == null)
            {
                return null;
            }

            
            return new ReadTblPersonaDTO
            {
                lPersona_id = result.lPersona_id,
                sPersona_nm = result.sPersona_nm,
                sPersona_aps = result.sPersona_aps,
                sPersona_email = result.sPersona_email,
                sPersona_telf = result.sPersona_telf,
                sPersona_tipo_persona = result.sPersona_tipo_persona,
                sPersona_sexo = result.sPersona_sexo
            };
        }

        public async Task<List<ReadTblPersonaDTO>> ObtenerTodos()
            {
                var personas = await _tblPersonaRepository.ObtenerTodos();
                var PersonasDTO = new List<ReadTblPersonaDTO>();

                foreach (var persona in personas)
                {
                    var personaDTO = new ReadTblPersonaDTO
                    {
                        lPersona_id = persona.lPersona_id,
                        sPersona_email = persona.sPersona_email,
                        sPersona_aps = persona.sPersona_aps,
                        sPersona_nm = persona.sPersona_nm,
                        sPersona_telf = persona.sPersona_telf,
                        sPersona_tipo_persona = persona.sPersona_tipo_persona
                    };
                    PersonasDTO.Add(personaDTO);
                }

                return PersonasDTO;
            }
        }
}
