using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoFinal.Negocio.Services
{
    public class AuthService
    {
        private readonly ITblUsuarioRepository _usuarioRepository;
        private readonly ITblRolRepository _rolRepository;
        private readonly PasswordHasher<TblUsuario> _passwordHasher = new();
        private readonly IConfiguration _configuration;
        public AuthService(ITblUsuarioRepository usuarioRepository, IConfiguration configuration, ITblRolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _rolRepository = rolRepository;
        }

        public async Task<ReadLoginResponseDTO?> ValidarCredenciales(
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
            //if (usuario.lRol_id != 1 && usuario.lRol_id != 2)
            //    return null;
            // Generar JWT
            var rol = await _rolRepository.ObtenerPorId(usuario.lRol_id);
            var usuarioConInfo = new ReadBasicUserInformationDTO
            {
                lUsuario_id = usuario.lUsuario_id,
                sRol_nombre = rol.sRol_nm,
                bEstado = usuario.bEstado,
                sUsername = username,
            };
            var jwt = GenerarJwt(usuarioConInfo);
            var response = new ReadLoginResponseDTO
            {
                JwtToken = jwt,
                UserInfo = new ReadBasicUserInformationDTO
                {
                    sUsername = usuario.sUsername,
                    lUsuario_id = usuario.lUsuario_id,
                    bEstado = usuario.bEstado
                }
            };
            return response;
        }
        public string GenerarJwt(ReadBasicUserInformationDTO usuario)
        {
            // 1. Payload
            // 1.1.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, usuario.lUsuario_id.ToString()),
                new Claim(ClaimTypes.Name, usuario.sUsername),
                new Claim(ClaimTypes.Role, usuario.sRol_nombre),
                new Claim("Estado", usuario.bEstado.ToString())
            };

            // 2. Header y firma
            // 2.1. Obtener la clave para firmar el jwt
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // 2.2. Definir el tipo de algoritmo
            var algorithm = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // 2.3. Configurar el token (issuer, audience, expire time, etc)

            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:Duration"])),
                signingCredentials: algorithm
                );

            // 3. Generamos y lo devolvemos
            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }
    }
}