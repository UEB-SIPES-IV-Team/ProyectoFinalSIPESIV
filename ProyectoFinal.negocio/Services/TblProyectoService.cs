using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblProyecto;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblProyectoService : ITblProyectoService
    {
        private readonly ITblProyectoRepository _tblProyectoRepository;
        public TblProyectoService(ITblProyectoRepository tblProyectoRepository)
        {
            _tblProyectoRepository = tblProyectoRepository;
        }
        public async Task Actualizar(UpdateTblProyectoDTO proyecto)
        {
            TblProyecto objProyecto= new TblProyecto
            {
                lProyecto_id = proyecto.lProyecto_id,
                lAsignatura_id = proyecto.lAsignatura_id,
                sProyecto_nm = proyecto.sProyecto_nm,
                sProyecto_desc = proyecto.sProyecto_desc,
                sProyecto_tipo = proyecto.sProyecto_tipo,
                sProyecto_estado = proyecto.sProyecto_estado,
                sProyecto_video = proyecto.sProyecto_video
            };
            await _tblProyectoRepository.Actualizar(objProyecto);
        }

        public async Task Crear(CreateTblProyectoDTO proyecto)
        {
            TblProyecto objProyecto = new TblProyecto
            {
                lAsignatura_id = proyecto.lAsignatura_id,
                sProyecto_nm = proyecto.sProyecto_nm,
                sProyecto_desc = proyecto.sProyecto_desc,
                sProyecto_tipo = proyecto.sProyecto_tipo,
                sProyecto_estado = proyecto.sProyecto_estado,
                sProyecto_video = proyecto.sProyecto_video
            };
            await _tblProyectoRepository.Crear(objProyecto);
        }

        public async Task Eliminar(int idProyecto)
        {
            await _tblProyectoRepository.Eliminar(idProyecto);
        }

        public async Task<ReadTblProyectoDTO> ObtenerPorId(int idProyecto)
        {
            var result = await _tblProyectoRepository.ObtenerPorId(idProyecto);
            if (result is null) return null;
            return new ReadTblProyectoDTO
            {
                lProyecto_id = result.lProyecto_id,
                lAsignatura_id = result.lAsignatura_id,
                sProyecto_nm = result.sProyecto_nm,
                sProyecto_desc = result.sProyecto_desc,
                sProyecto_tipo = result.sProyecto_tipo,
                sProyecto_estado = result.sProyecto_estado,
                sProyecto_video = result.sProyecto_video
            };
        }

        public async Task<List<ReadTblProyectoDTO>> ObtenerTodos()
        {
            var proyectos = await _tblProyectoRepository.ObtenerTodos();
            var ProyectoDTO = new List<ReadTblProyectoDTO>();

            foreach (var proyecto in proyectos)
            {
                var proyectoDTO = new ReadTblProyectoDTO
                {
                    lProyecto_id = proyecto.lProyecto_id,
                    lAsignatura_id = proyecto.lAsignatura_id,
                    sProyecto_nm = proyecto.sProyecto_nm,
                    sProyecto_desc = proyecto.sProyecto_desc,
                    sProyecto_tipo = proyecto.sProyecto_tipo,
                    sProyecto_estado = proyecto.sProyecto_estado,
                    sProyecto_video = proyecto.sProyecto_video
                };
                ProyectoDTO.Add(proyectoDTO);
            }

            return ProyectoDTO;
        }
    }
}

