
using ProyectoFinal.Negocio.DTOs.TblPremiacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblPremiacionService
    {
        // CRUD
        public Task Crear(CreateTblPremiacionDTO premiacion);
        public Task Actualizar(UpdateTblPremiacionDTO premiacion);
        public Task Eliminar(int idPremiacion);
        public Task<List<ReadTblPremiacionDTO>> ObtenerTodos();
        public Task<ReadTblPremiacionDTO> ObtenerPorId(int idPremiacion);
    }

}
