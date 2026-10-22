using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblProyectoXPersona;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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

        // 🟢 MÉTODO CORREGIDO: Llama directamente al repositorio de base de datos
        public async Task<List<ReadTblProyectoXPersonaDTO>> ObtenerIntegrantesPorProyectoAsync(long idProyecto)
        {
            try
            {
                // Consulta al repositorio (BD / PostgreSQL Function)
                var listaEntidad = await _tblProyectoXPersonaRepository.ObtenerIntegrantesPorProyectoAsync(idProyecto);
                var listaDTO = new List<ReadTblProyectoXPersonaDTO>();

                foreach (var item in listaEntidad)
                {
                    listaDTO.Add(new ReadTblProyectoXPersonaDTO
                    {
                        lProyecto_id = item.lProyecto_id,
                        lPersona_id = item.lPersona_id,
                        sPersona_nm = item.sPersona_nm,
                        sPersona_aps = item.sPersona_aps
                    });
                }

                return listaDTO;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener integrantes en el servicio Backend: {ex.Message}");
                return new List<ReadTblProyectoXPersonaDTO>();
            }
        }
    }
}