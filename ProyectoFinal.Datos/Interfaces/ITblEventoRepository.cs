using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblEventoRepository
    {
        Task<int> Crear(TblEvento evento);
        Task<int> Actualizar(TblEvento evento);
        Task<int> Eliminar(int idEvento);
        Task<List<TblEvento>> ObtenerTodos();
        Task<TblEvento> ObtenerPorId(int idEvento);
    }
}
