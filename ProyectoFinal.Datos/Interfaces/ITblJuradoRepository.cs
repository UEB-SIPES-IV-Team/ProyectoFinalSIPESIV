using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblJuradoRepository
    {
        // CRUD
        public Task Crear(TblJurado jurado);
        public Task<TblJurado> ObtenerPorId(int idJurado);
        public Task<List<TblJurado>> ObtenerTodos();
        public Task Actualizar(TblJurado jurado);
        public Task Eliminar(int idJurado);
    }
}
