using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;

namespace ProyectoFinal.Negocio.Services
{
    public class AuthService
    {
        private readonly ITblUsuarioRepository _usuarioRepository;
        private readonly PasswordHasher<TblUsuario> _passwordHasher = new();

        public AuthService(ITblUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<TblUsuario?> ValidarCredenciales(
            string username,
            string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password))
                return null;

            var usuario = await _usuarioRepository
                .ObtenerPorUsername(username.Trim());

            if (usuario == null || !usuario.bEstado)
                return null;

            var resultado = _passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.sPassword,
                password
            );

            if (resultado == PasswordVerificationResult.Failed)
                return null;

            // Roles administrativos de SIPES IV.
            if (usuario.lRol_id != 1 && usuario.lRol_id != 2)
                return null;

            return usuario;
        }
    }
}