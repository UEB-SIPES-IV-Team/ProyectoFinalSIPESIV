using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblEventoRepository
    {
        // CRUD
        public Task<int> Crear(TblEvento evento);
        public Task<TblEvento> ObtenerPorId(int lEvento_id);
        public Task<List<TblEvento>> ObtenerTodos();
        public Task Actualizar(TblEvento evento);
        public Task Eliminar(int lEvento_id);
    }
}
