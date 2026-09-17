using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblPEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace ProyectoFinal.Negocio.Services
{
    public class TblPEvaluacionService : ITblPEvaluacionService
    {
        private readonly ITblPEvaluacionRepository _tblPEvaluacionRepository;

        public TblPEvaluacionService(ITblPEvaluacionRepository tblPEvaluacionRepository)
        {
            _tblPEvaluacionRepository = tblPEvaluacionRepository;
        }

        public async Task Actualizar(UpdateTblPEvaluacionDTO pEvaluacion)
        {
            TblPEvaluacion pEvaluacionActualizada = new TblPEvaluacion
            {
                lPEvaluacion_id = pEvaluacion.lPEvaluacion_id,
                sPEvaluacion_nm = pEvaluacion.sPEvaluacion_nm,
                sPEvaluacion_desc = pEvaluacion.sPEvaluacion_desc,
                sPEvaluacion_peso = pEvaluacion.sPEvaluacion_peso
            };

            
            await _tblPEvaluacionRepository.Actualizar(pEvaluacionActualizada);
        }

        public async Task Crear(CreateTblPEvaluacionDTO pEvaluacion)
        {
            TblPEvaluacion pEvaluacionCreada = new TblPEvaluacion
            {
                sPEvaluacion_nm = pEvaluacion.sPEvaluacion_nm,
                sPEvaluacion_desc = pEvaluacion.sPEvaluacion_desc,
                sPEvaluacion_peso = pEvaluacion.sPEvaluacion_peso
            };

            await _tblPEvaluacionRepository.Crear(pEvaluacionCreada);
        }

        public async Task Eliminar(int idPEvaluacion)
        {
            await _tblPEvaluacionRepository.Eliminar(idPEvaluacion);
        }

        public async Task<ReadTblPEvaluacionDTO> ObtenerPorId(int idPEvaluacion)
        {
            var pEvaluacion = await _tblPEvaluacionRepository.ObtenerPorId(idPEvaluacion);
            if (pEvaluacion == null)
            {
                return null;
            }

            return new ReadTblPEvaluacionDTO
            {
                lPEvaluacion_id = pEvaluacion.lPEvaluacion_id,
                sPEvaluacion_nm = pEvaluacion.sPEvaluacion_nm,
                sPEvaluacion_desc = pEvaluacion.sPEvaluacion_desc,
                sPEvaluacion_peso = pEvaluacion.sPEvaluacion_peso
            };
        }


        public async Task<List<ReadTblPEvaluacionDTO>> ObtenerEvaluaciones()
        {
         
            var evaluaciones = await _tblPEvaluacionRepository.ObtenerTodos();

            return evaluaciones.Select(e => new ReadTblPEvaluacionDTO
            {
                lPEvaluacion_id = e.lPEvaluacion_id,
                sPEvaluacion_nm = e.sPEvaluacion_nm,
                sPEvaluacion_desc = e.sPEvaluacion_desc,
                sPEvaluacion_peso = e.sPEvaluacion_peso
            }).ToList();
        }
    }
}