using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblJuradoRepository
    {
        // CRUD
        public Task<int> Crear(TblJurado jurado); // Cambiar 'Task' por 'Task<int>'
        public Task<TblJurado> ObtenerPorId(int lJurado_id);
        public Task<List<TblJurado>> ObtenerTodos();
        public Task Actualizar(TblJurado jurado);
        public Task Eliminar(int lJurado_id);
    }
}