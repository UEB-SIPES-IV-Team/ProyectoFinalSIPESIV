using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblProyectoRepository
    {
        // CRUD
        public Task Crear(TblProyecto proyecto);
        public Task<TblProyecto> ObtenerPorId(int idProyecto);
        public Task<List<TblProyecto>> ObtenerTodos();
        public Task Actualizar(TblProyecto proyecto);
        public Task Eliminar(int idProyecto);
        public Task<IEnumerable<TblProyecto>> ObtenerPorEventoAsync(int eventoId);

    }
}

