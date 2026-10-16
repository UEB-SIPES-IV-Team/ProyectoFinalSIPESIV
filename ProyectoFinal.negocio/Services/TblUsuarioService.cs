using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblUsuario;
using ProyectoFinal.Negocio.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class TblUsuarioService : ITblUsuarioService
    {
        private readonly ITblUsuarioRepository _tblUsuarioRepository;
        private readonly PasswordHasher<TblUsuario> _passwordHasher = new();
        public TblUsuarioService(ITblUsuarioRepository tblUsuarioRepository)
        {
            _tblUsuarioRepository = tblUsuarioRepository;
        }

        public async Task Actualizar(TblUsuarioUpdateDto usuario)
        {
            TblUsuario objUsuario = new TblUsuario
            {
                lUsuario_id = usuario.lUsuario_id,
                lPersona_id = usuario.lPersona_id,
                lRol_id = usuario.lRol_id,
                sUsername = usuario.sUsername,
                sPassword = usuario.sPassword,
                bEstado = usuario.bEstado
            };
            await _tblUsuarioRepository.Actualizar(objUsuario);
        }

        public async Task Crear(TblUsuarioCreateDto usuario)
        {
            TblUsuario objUsuario = new TblUsuario
            {
                lPersona_id = usuario.lPersona_id,
                lRol_id = usuario.lRol_id,
                sUsername = usuario.sUsername,
                sPassword = usuario.sPassword,
                bEstado = usuario.bEstado
            };
            await _tblUsuarioRepository.Crear(objUsuario);
        }

        public async Task Eliminar(int idUsuario)
        {
            await _tblUsuarioRepository.Eliminar(idUsuario);
        }

        public async Task<TblUsuarioReadDto> ObtenerPorId(int idUsuario)
        {
            var result = await _tblUsuarioRepository.ObtenerPorId(idUsuario);
            if (result is null) return null;
            return new TblUsuarioReadDto
            {
                lUsuario_id = result.lUsuario_id,
                lPersona_id = result.lPersona_id,
                lRol_id = result.lRol_id,
                sUsername = result.sUsername,
                sPassword = result.sPassword,
                bEstado = result.bEstado
            };
        }

        public async Task<List<TblUsuarioReadDto>> ObtenerTodos()
        {
            var usuarios = await _tblUsuarioRepository.ObtenerTodos();
            var usuariosDTO = new List<TblUsuarioReadDto>();

            foreach (var usuario in usuarios)
            {
                var usuarioDTO = new TblUsuarioReadDto
                {
                    lUsuario_id = usuario.lUsuario_id,
                    lPersona_id = usuario.lPersona_id,
                    lRol_id = usuario.lRol_id,
                    sUsername = usuario.sUsername,
                    sPassword = usuario.sPassword,
                    bEstado = usuario.bEstado
                };
                usuariosDTO.Add(usuarioDTO);
            }

            return usuariosDTO;
        }
    }
}
