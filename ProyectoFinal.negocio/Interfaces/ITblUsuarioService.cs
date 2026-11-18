using ProyectoFinal.Negocio.DTOs.TblUsuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Interfaces
{
    public interface ITblUsuarioService
    {
        public Task Crear(TblUsuarioCreateDto usuario);
        public Task<TblUsuarioReadDto> ObtenerPorId(int idUsuario);
        public Task<List<TblUsuarioReadDto>> ObtenerTodos();
        public Task Actualizar(TblUsuarioUpdateDto usuario);
        public Task Eliminar(int idUsuario);

        
        public Task<PerfilUsuarioDto?> ObtenerPerfil(int idUsuario);
        public Task<bool> CambiarPassword(int idUsuario, string passwordActual, string nuevaPassword);
    }
}