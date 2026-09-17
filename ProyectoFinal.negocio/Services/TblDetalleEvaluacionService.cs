using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq; // AGREGADO: Necesario para .Select() y .ToList()
using System.Threading.Tasks;

namespace ProyectoFinal.Negocio.Services
{
    public class TblDetalleEvaluacionService : ITblDetalleEvaluacionService
    {
        private readonly ITblDetalleEvaluacionRepository _tblDetalleEvaluacionRepository;

        public TblDetalleEvaluacionService(ITblDetalleEvaluacionRepository tblDetalleEvaluacionRepository)
        {
            _tblDetalleEvaluacionRepository = tblDetalleEvaluacionRepository;
        }

        public async Task Actualizar(UpdateDetalleEvaluacionDTO detalleEvaluacion)
        {
            TblDetalleEvaluacion detalleEvaluacionActualizado = new TblDetalleEvaluacion
            {
                lDetalleEvaluacion_id = detalleEvaluacion.lDetalleEvaluacion_id,
                lPEvaluacion_id = detalleEvaluacion.lPEvaluacion_id,
                lEvaluacionJurado_id = detalleEvaluacion.lEvaluacionJurado_id,
                sPuntaje = detalleEvaluacion.sPuntaje
            };

            await _tblDetalleEvaluacionRepository.Actualizar(detalleEvaluacionActualizado);
        }

        public async Task Crear(CreateTblDetalleEvaluacionDTO detalleEvaluacion)
        {
            TblDetalleEvaluacion detalleEvaluacionCreado = new TblDetalleEvaluacion
            {
                lPEvaluacion_id = detalleEvaluacion.lPEvaluacion_id,
                lEvaluacionJurado_id = detalleEvaluacion.lEvaluacionJurado_id,
                sPuntaje = detalleEvaluacion.sPuntaje
            };

            await _tblDetalleEvaluacionRepository.Crear(detalleEvaluacionCreado);
        }

        public async Task Eliminar(int idDetalleEvaluacion)
        {
            await _tblDetalleEvaluacionRepository.Eliminar(idDetalleEvaluacion);
        }

        public async Task<ReadTblDetalleEvaluacionDTO> ObtenerPorId(int idDetalleEvaluacion)
        {
            var detalleEvaluacion = await _tblDetalleEvaluacionRepository.ObtenerPorId(idDetalleEvaluacion);
            if (detalleEvaluacion == null)
            {
                return null;
            }

            return new ReadTblDetalleEvaluacionDTO
            {
                lDetalleEvaluacion_id = detalleEvaluacion.lDetalleEvaluacion_id,
                lPEvaluacion_id = detalleEvaluacion.lPEvaluacion_id,
                lEvaluacionJurado_id = detalleEvaluacion.lEvaluacionJurado_id,
                sPuntaje = detalleEvaluacion.sPuntaje
            };
        }

        public async Task<List<ReadTblDetalleEvaluacionDTO>> ObtenerDetallesEvaluacion()
        {
            var detallesEvaluacion = await _tblDetalleEvaluacionRepository.ObtenerTodos(); // Asegúrate si en el repo es ObtenerTodos() u ObtenerDetallesEvaluacion()

            return detallesEvaluacion.Select(de => new ReadTblDetalleEvaluacionDTO
            {
                lDetalleEvaluacion_id = de.lDetalleEvaluacion_id,
                lPEvaluacion_id = de.lPEvaluacion_id,
                lEvaluacionJurado_id = de.lEvaluacionJurado_id,
                sPuntaje = de.sPuntaje
            }).ToList();
        }
    }
}