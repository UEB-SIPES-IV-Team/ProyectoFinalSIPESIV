using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.DTOs.TblEvaluacionJurado;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblEvaluacionJuradoService : ITblEvaluacionJuradoService
    {
        private readonly ITblEvaluacionJuradoRepository _tblEvaluacionJuradoRepository;
        public TblEvaluacionJuradoService(ITblEvaluacionJuradoRepository tblEvaluacionJuradoRepository)
        {
            _tblEvaluacionJuradoRepository = tblEvaluacionJuradoRepository;
        }
        public async Task Actualizar(UpdateTblEvaluacionJuradoDTO evaluacionJurado)
        {
            TblEvaluacionJurado objEvaluacionJurado = new TblEvaluacionJurado
            {
                lEvaluacionJurado_id = evaluacionJurado.lEvaluacionJurado_id,
                lProyecto_id = evaluacionJurado.lProyecto_id,
                lJurado_id = evaluacionJurado.lJurado_id,
                sEvaluacionJurado_fecha = evaluacionJurado.sEvaluacionJurado_fecha,
                sEvaluacionJurado_obs = evaluacionJurado.sEvaluacionJurado_obs
            };
            await _tblEvaluacionJuradoRepository.Actualizar(objEvaluacionJurado);
        }

        public async Task Crear(CreateTblEvaluacionJuradoDTO evaluacionJurado)
        {
            TblEvaluacionJurado objEvaluacionJurado = new TblEvaluacionJurado
            {
                lProyecto_id = evaluacionJurado.lProyecto_id,
                lJurado_id = evaluacionJurado.lJurado_id,
                sEvaluacionJurado_fecha = evaluacionJurado.sEvaluacionJurado_fecha,
                sEvaluacionJurado_obs = evaluacionJurado.sEvaluacionJurado_obs
            };
            await _tblEvaluacionJuradoRepository.Crear(objEvaluacionJurado);
        }

        public async Task Eliminar(int idEvaluacionJurado)
        {
            await _tblEvaluacionJuradoRepository.Eliminar(idEvaluacionJurado);
        }

        public async Task<ReadTblEvaluacionJuradoDTO> ObtenerPorId(int idEvaluacionJurado)
        {
            var result = await _tblEvaluacionJuradoRepository.ObtenerPorId(idEvaluacionJurado);
            if (result is null) return null;
            return new ReadTblEvaluacionJuradoDTO
            {
                lEvaluacionJurado_id = result.lEvaluacionJurado_id,
                lProyecto_id = result.lProyecto_id,
                lJurado_id = result.lJurado_id,
                sEvaluacionJurado_fecha = result.sEvaluacionJurado_fecha,
                sEvaluacionJurado_obs = result.sEvaluacionJurado_obs
            };
        }

        public async Task<List<ReadTblEvaluacionJuradoDTO>> ObtenerTodos()
        {
            var evaluacionJurados = await _tblEvaluacionJuradoRepository.ObtenerTodos();
            var EvaluacionJuradosDTO = new List<ReadTblEvaluacionJuradoDTO>();

            foreach (var evaluacionJurado in evaluacionJurados)
            {
                var evaluacionJuradoDTO = new ReadTblEvaluacionJuradoDTO
                {
                    lEvaluacionJurado_id = evaluacionJurado.lEvaluacionJurado_id,
                    lProyecto_id = evaluacionJurado.lProyecto_id,
                    lJurado_id = evaluacionJurado.lJurado_id,
                    sEvaluacionJurado_fecha = evaluacionJurado.sEvaluacionJurado_fecha,
                    sEvaluacionJurado_obs = evaluacionJurado.sEvaluacionJurado_obs
                };
                EvaluacionJuradosDTO.Add(evaluacionJuradoDTO);
            }

            return EvaluacionJuradosDTO;
        }
    }
}
