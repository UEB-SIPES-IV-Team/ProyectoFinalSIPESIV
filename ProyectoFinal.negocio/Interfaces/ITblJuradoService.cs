using ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion;
using ProyectoFinal.Negocio.DTOs.TblJurado;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblJuradoService
    {
        public Task Crear(CreateTblJuradoDTO jurado);
        public Task Actualizar(UpdateTblJuradoDTO jurado);
        public Task Eliminar(int idJurado);
        public Task<ReadTblJuradoDTO> ObtenerPorId(int idJurado);
        public Task<List<ReadTblJuradoDTO>> ObtenerJurados();
    }
}
