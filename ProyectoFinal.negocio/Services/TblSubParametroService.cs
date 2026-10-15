using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.DTOs.TblSubParametro;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblSubParametroService : ITblSubParametroService
    {
        private readonly ITblSubParametroRepository _tblSubParametroRepository;

        public TblSubParametroService(ITblSubParametroRepository tblSubParametroRepository)
        {
            _tblSubParametroRepository = tblSubParametroRepository;
        }

        public async Task Actualizar(UpdateTblSubParametroDTO subParametro)
        {
            TblSubParametro subParametroActualizado = new TblSubParametro
            {
                lSubParametro_id = subParametro.lSubParametro_id,
                lParametro_id = subParametro.lParametro_id,
                sSubParametro_desc = subParametro.sSubParametro_desc,
                sSubParametro_nm = subParametro.sSubParametro_nm
            };
            int resultado = await _tblSubParametroRepository.Actualizar(subParametroActualizado);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al actualizar el subparametro");
            }
        }

        public async Task Crear(CreateTblSubParametroDTO subParametro)
        {
            TblSubParametro subParametroCreado = new TblSubParametro
            {
                lParametro_id = subParametro.lParametro_id,
                sSubParametro_nm = subParametro.sSubParametro_nm,
                sSubParametro_desc = subParametro.sSubParametro_desc
            };
            int resultado = await _tblSubParametroRepository.Crear(subParametroCreado);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al crear el subparametro");
            }
        }

        public async Task Eliminar(int idSubParametro)
        {
            int resultado = await _tblSubParametroRepository.Eliminar(idSubParametro);
            if (resultado == 0)
            {
                throw new Exception("Ocurrió un error al eliminar el subparametro");
            }
        }

        public async Task<ReadTblSubParametroDTO> ObtenerPorId(int idSubParametro)
        {
            var subParametro = await _tblSubParametroRepository.ObtenerPorId(idSubParametro);
            if (subParametro == null)
            {
                return null;
            }
            return new ReadTblSubParametroDTO
            {
                lSubParametro_id = subParametro.lSubParametro_id,
                lParametro_id = subParametro.lParametro_id,
                sSubParametro_desc = subParametro.sSubParametro_desc,
                sSubParametro_nm = subParametro.sSubParametro_nm
            };
        }

        public async Task<List<ReadTblSubParametroDTO>> ObtenerSubParametros()
        {
            var subParametros = await _tblSubParametroRepository.ObtenerSubParametros();
            return subParametros.Select(subparam => new ReadTblSubParametroDTO
            {
                lSubParametro_id = subparam.lSubParametro_id,
                lParametro_id = subparam.lParametro_id,
                sSubParametro_nm = subparam.sSubParametro_nm,
                sSubParametro_desc = subparam.sSubParametro_desc
            }).ToList();
        }

        public async Task<List<ReadTblSubParametroDTO>> ObtenerSubparametrosTipoArea()
        {
            var lista = await _tblSubParametroRepository.ObtenerSubparametrosTipoArea();

            return lista.Select(sp => new ReadTblSubParametroDTO
            {
                lSubParametro_id = sp.lSubParametro_id,
                lParametro_id = sp.lParametro_id,
                sSubParametro_nm = sp.sSubParametro_nm,
                sSubParametro_desc = sp.sSubParametro_desc
            }).ToList();
        }
    }
}
