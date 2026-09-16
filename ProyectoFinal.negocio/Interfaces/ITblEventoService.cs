using ProyectoFinal.Negocio.DTOs.TblEvento;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblEventoService
    {
        // CRUD
        public Task Crear(CreateTblEventoDTO evento);
        public Task Actualizar(UpdateTblEventoDTO evento);
        public Task Eliminar(int idEvento);
        public Task<List<ReadTblEventoDTO>> ObtenerTodos();
        public Task<ReadTblEventoDTO> ObtenerPorId(int idEvento);
    }
}
