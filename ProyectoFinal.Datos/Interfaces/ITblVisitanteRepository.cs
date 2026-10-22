using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblVisitanteRepository
    {
        //CRUD
        public Task Crear(TblVisitante visitante);
        public Task<TblVisitante> ObtenerPorId(int idVisitante);
        public Task<List<TblVisitante>> ObtenerTodos();
        public Task Actualizar(TblVisitante visitante);
        public Task Eliminar(int idVisitante);
        public  Task<TblVisitante?> ObtenerPorCiYEvento(string ci, int idEvento);
    }
}
