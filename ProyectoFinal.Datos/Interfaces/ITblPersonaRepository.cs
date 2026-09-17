using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblPersonaRepository
    {
        // CRUD
        public Task Crear(TblPersona persona);
        public Task<TblPersona> ObtenerPorId(int idPersona);
        public Task<List<TblPersona>> ObtenerTodos();
        public Task Actualizar(TblPersona persona);
        public Task Eliminar(int idPersona);
    }
}
