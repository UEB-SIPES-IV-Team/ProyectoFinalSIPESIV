using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.DTOs.TblAsignatura;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoFinal.Negocio.Services
{
    public class TblAsignaturaService : ITblAsignaturaService
    {
        private readonly ITblAsignaturaRepository _tblAsignaturaRepository;

        public TblAsignaturaService(ITblAsignaturaRepository tblAsignaturaRepository)
        {
            _tblAsignaturaRepository = tblAsignaturaRepository;
        }

        public async Task Actualizar(UpdateTblAsignaturaDTO asignatura)
        {
            TblAsignatura asignaturaActualizada = new TblAsignatura
            {
                lAsignatura_id = asignatura.lAsignatura_id,
                lArea_id = asignatura.lArea_id,
                lAsignatura_slog = asignatura.lAsignatura_slog,
                lAsignatura_nm = asignatura.lAsignatura_nm,
                bEstado = asignatura.bEstado 
            };

            int resultado = await _tblAsignaturaRepository.Actualizar(asignaturaActualizada);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al actualizar la asignatura");
            }
        }

        public async Task Crear(CreateTblAsignaturaDTO asignatura)
        {
            TblAsignatura asignaturaCreada = new TblAsignatura
            {
                lArea_id = asignatura.lArea_id,
                lAsignatura_slog = asignatura.lAsignatura_slog,
                lAsignatura_nm = asignatura.lAsignatura_nm,
                bEstado = asignatura.bEstado
            };

            int resultado = await _tblAsignaturaRepository.Crear(asignaturaCreada);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al crear la asignatura");
            }
        }

        public async Task Eliminar(int idAsignatura)
        {
            int resultado = await _tblAsignaturaRepository.Eliminar(idAsignatura);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al eliminar la asignatura");
            }
        }

        public async Task<List<ReadTblAsignaturaDTO>> ObtenerAsignaturas()
        {
            var asignaturas = await _tblAsignaturaRepository.ObtenerAsignaturas();
            List<ReadTblAsignaturaDTO> listaDTOs = new List<ReadTblAsignaturaDTO>();

            foreach (var item in asignaturas)
            {
                listaDTOs.Add(new ReadTblAsignaturaDTO
                {
                    lAsignatura_id = item.lAsignatura_id,
                    lArea_id = item.lArea_id,
                    lAsignatura_slog = item.lAsignatura_slog,
                    lAsignatura_nm = item.lAsignatura_nm,
                    bEstado = item.bEstado
                });
            }
            return listaDTOs;
        }

        public async Task<ReadTblAsignaturaDTO> ObtenerPorId(int idAsignatura)
        {
            var asignatura = await _tblAsignaturaRepository.ObtenerPorId(idAsignatura);

            if (asignatura == null)
            {
                return null;
            }

            return new ReadTblAsignaturaDTO
            {
                lAsignatura_id = asignatura.lAsignatura_id,
                lArea_id = asignatura.lArea_id,
                lAsignatura_slog = asignatura.lAsignatura_slog,
                lAsignatura_nm = asignatura.lAsignatura_nm,
                bEstado = asignatura.bEstado
            };
        }
    }
}