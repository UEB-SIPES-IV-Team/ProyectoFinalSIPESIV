using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblEvento;
using ProyectoFinal.Negocio.DTOs.TblJuradoXUnidadOrganzativa;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblJuradoXUnidadOrganizativaService : ITblJuradoXUnidadOrganizativaService
    {
        private readonly ITblJuradoXUnidadOrganizativaRepository _tblJuradoXUnidadOrganizativaRepository;
        public TblJuradoXUnidadOrganizativaService(ITblJuradoXUnidadOrganizativaRepository tblJuradoXUnidadOrganizativaRepository)
        {
            _tblJuradoXUnidadOrganizativaRepository = tblJuradoXUnidadOrganizativaRepository;
        }
        public async Task Actualizar(UpdateTblJuradoXUnidadOrganzativaDTO juradoXUnidad)
        {
            TblJuradoXUnidadOrganizativa objJuradoXUnidad = new TblJuradoXUnidadOrganizativa
            {
               lJuradoXFacultad_id = juradoXUnidad.lJuradoXFacultad_id,
                lJurado_id = juradoXUnidad.lJurado_id,
                lEvento_id = juradoXUnidad.lEvento_id,
               lUniversidadOrganizativa_id = juradoXUnidad.lUniversidadOrganizativa_id
            };
            await _tblJuradoXUnidadOrganizativaRepository.Actualizar(objJuradoXUnidad);
        }

        public async Task Crear(CreateTblJuradoXUnidadOrganzativaDTO juradoXUnidad)
        {
            TblJuradoXUnidadOrganizativa objJuradoXUnidad = new TblJuradoXUnidadOrganizativa
            {
                lJurado_id = juradoXUnidad.lJurado_id,
                lEvento_id = juradoXUnidad.lEvento_id,
                lUniversidadOrganizativa_id = juradoXUnidad.lUniversidadOrganizativa_id
            };
            await _tblJuradoXUnidadOrganizativaRepository.Crear(objJuradoXUnidad);
        }

        public async Task Eliminar(int idJuradoXUnidad)
        {
            await _tblJuradoXUnidadOrganizativaRepository.Eliminar(idJuradoXUnidad);
        }

        public async Task<ReadTblJuradoXUnidadOrganzativaDTO> ObtenerPorId(int idJuradoXUnidad)
        {
            var result = await _tblJuradoXUnidadOrganizativaRepository.ObtenerPorId(idJuradoXUnidad);
            if (result is null) return null;
            return new ReadTblJuradoXUnidadOrganzativaDTO
            {
                lJuradoXFacultad_id = result.lJuradoXFacultad_id,
                lJurado_id = result.lJurado_id,
                lEvento_id = result.lEvento_id,
                lUniversidadOrganizativa_id = result.lUniversidadOrganizativa_id
            };
        }

        public async Task<List<ReadTblJuradoXUnidadOrganzativaDTO>> ObtenerTodos()
        {
            var juradosXUnidades = await _tblJuradoXUnidadOrganizativaRepository.ObtenerTodos();
            var JuradosXUnidadesDTO = new List<ReadTblJuradoXUnidadOrganzativaDTO>();

            foreach (var juradoXUnidad in juradosXUnidades)
            {
                var juradosXUnidadDTO = new ReadTblJuradoXUnidadOrganzativaDTO
                {
                    lJuradoXFacultad_id = juradoXUnidad.lJuradoXFacultad_id,
                    lJurado_id = juradoXUnidad.lJurado_id,
                    lEvento_id = juradoXUnidad.lEvento_id,
                    lUniversidadOrganizativa_id = juradoXUnidad.lUniversidadOrganizativa_id
                };
                JuradosXUnidadesDTO.Add(juradosXUnidadDTO);
            }

            return JuradosXUnidadesDTO;
        }
    }
}

