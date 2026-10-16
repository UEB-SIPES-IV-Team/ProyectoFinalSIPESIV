using ProyectoFinal.Datos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Interfaces
{
    public interface ITblUsuarioRepository
    {
        public Task Crear(TblUsuario usuario);
        Task<TblUsuario?> ObtenerPorUsername(string username);
        public Task<TblUsuario> ObtenerPorId(int idUsuario);
        public Task<List<TblUsuario>> ObtenerTodos();
        public Task Actualizar(TblUsuario usuario);
        public Task Eliminar(int idUsuario);
    }
}
