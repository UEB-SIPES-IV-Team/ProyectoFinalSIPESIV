using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblJurado;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace ProyectoFinal.Negocio.Services
{
    public class TblJuradoService : ITblJuradoService
    {
        private readonly ITblJuradoRepository _tblJuradoRepository;

        public TblJuradoService(ITblJuradoRepository tblJuradoRepository)
        {
            _tblJuradoRepository = tblJuradoRepository;
        }

        public async Task Actualizar(UpdateTblJuradoDTO jurado)
        {
            TblJurado juradoActualizado = new TblJurado
            {
                lJurado_id = jurado.lJurado_id,
                lPersona_id = jurado.lPersona_id,
                sJurado_institucion = jurado.sJurado_institucion,
                sJurado_gral = jurado.sJurado_gral
            };

            await _tblJuradoRepository.Actualizar(juradoActualizado);
        }

        public async Task Crear(CreateTblJuradoDTO jurado)
        {
            TblJurado juradoCreado = new TblJurado
            {
                lPersona_id = jurado.lPersona_id,
                sJurado_institucion = jurado.sJurado_institucion,
                sJurado_gral = jurado.sJurado_gral
            };

            await _tblJuradoRepository.Crear(juradoCreado);
        }

        public async Task Eliminar(int idJurado)
        {
            await _tblJuradoRepository.Eliminar(idJurado);
        }

        public async Task<ReadTblJuradoDTO> ObtenerPorId(int idJurado)
        {
            TblJurado jurado = await _tblJuradoRepository.ObtenerPorId(idJurado);
            if (jurado == null)
            {
                throw new Exception("No se encontró el jurado con el ID proporcionado");
            }

            return new ReadTblJuradoDTO
            {
                lJurado_id = jurado.lJurado_id,
                lPersona_id = jurado.lPersona_id,
                sJurado_institucion = jurado.sJurado_institucion,
                sJurado_gral = jurado.sJurado_gral
            };
        }

        public async Task<List<ReadTblJuradoDTO>> ObtenerJurados()
        {
            List<TblJurado> jurados = await _tblJuradoRepository.ObtenerTodos();

            return jurados.Select(j => new ReadTblJuradoDTO
            {
                lJurado_id = j.lJurado_id,
                lPersona_id = j.lPersona_id,
                sJurado_institucion = j.sJurado_institucion,
                sJurado_gral = j.sJurado_gral
            }).ToList();
        }
    }
}