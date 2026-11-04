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

            bool esPasswordValida = false;

            // 1. Intentar validar usando el verificador de Hash de ASP.NET Identity
            try
            {
                var resultado = _passwordHasher.VerifyHashedPassword(
                    usuario,
                    usuario.sPassword,
                    password
                );

                esPasswordValida = (resultado == PasswordVerificationResult.Success ||
                                    resultado == PasswordVerificationResult.SuccessRehashNeeded);
            }
            catch
            {
                esPasswordValida = false;
            }

            // 2. Si falló el Hash, verificar si la contraseña en la BD está en texto plano
            if (!esPasswordValida && usuario.sPassword == password)
            {
                esPasswordValida = true;
            }

            if (!esPasswordValida)
                return null;

            // PERMITIR ACCESO: Se removió la restricción rígida de (lRol_id != 1 && lRol_id != 2)
            // para que usuarios con Rol 3, 5, etc. (como ruti_08 o jurado.matias) puedan autenticarse.

            return usuario;
        }
    }
}