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
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            var existente = await _tblUsuarioRepository
                .ObtenerPorId(usuario.lUsuario_id);

            if (existente == null)
                throw new Exception("El usuario no existe.");

            existente.lPersona_id = usuario.lPersona_id;
            existente.lRol_id = usuario.lRol_id;
            existente.sUsername = usuario.sUsername.Trim();
            existente.bEstado = usuario.bEstado;

            if (!string.IsNullOrWhiteSpace(usuario.sPassword))
            {
                existente.sPassword = _passwordHasher.HashPassword(
                    existente,
                    usuario.sPassword
                );
            }

            await _tblUsuarioRepository.Actualizar(existente);
        }




        public async Task Crear(TblUsuarioCreateDto usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.sPassword))
                throw new ArgumentException(
                    "La contraseña es obligatoria.");

            var nuevoUsuario = new TblUsuario
            {
                lPersona_id = usuario.lPersona_id,
                lRol_id = usuario.lRol_id,
                sUsername = usuario.sUsername.Trim(),
                bEstado = usuario.bEstado
            };

            nuevoUsuario.sPassword = _passwordHasher.HashPassword(
                nuevoUsuario,
                usuario.sPassword
            );

            await _tblUsuarioRepository.Crear(nuevoUsuario);
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
        public async Task<PerfilUsuarioDto?> ObtenerPerfil(int idUsuario)
        {
            var perfilEntity = await _tblUsuarioRepository.ObtenerPerfil(idUsuario);

            if (perfilEntity == null)
                return null;

            return new PerfilUsuarioDto
            {
                lUsuario_id = perfilEntity.lUsuario_id,
                lPersona_id = perfilEntity.lPersona_id,
                sUsername = perfilEntity.sUsername,
                sPersona_nm = perfilEntity.sPersona_nm,
                sPersona_aps = perfilEntity.sPersona_aps,
                sPersona_ci = perfilEntity.sPersona_ci,
                sPersona_email = perfilEntity.sPersona_email,
                sPersona_telf = perfilEntity.sPersona_telf,
                sRol_nm = perfilEntity.sRol_nm
            };
        }

        public async Task<bool> CambiarPassword(int idUsuario, string passwordActual, string nuevaPassword)
        {
            if (string.IsNullOrWhiteSpace(passwordActual) || string.IsNullOrWhiteSpace(nuevaPassword))
                throw new ArgumentException("Las contraseñas no pueden estar vacías.");

            var usuario = await _tblUsuarioRepository.ObtenerPorId(idUsuario);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            // Verificar si la contraseña actual coincide con el hash guardado en la BD
            var resultadoVerificacion = _passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.sPassword,
                passwordActual
            );

            if (resultadoVerificacion == PasswordVerificationResult.Failed)
                return false;

            // Generar el hash seguro para la nueva contraseña y guardarla
            string nuevoPasswordHash = _passwordHasher.HashPassword(usuario, nuevaPassword);
            int filasAfectadas = await _tblUsuarioRepository.CambiarPassword(idUsuario, nuevoPasswordHash);

            return filasAfectadas > 0;
        }
    }
}
