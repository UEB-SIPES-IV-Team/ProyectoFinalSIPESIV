using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinal.Negocio.DTOs;
using ProyectoFinal.Negocio.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(
            AuthService authService,
            IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request)
        {
            var usuario = await _authService.ValidarCredenciales(
                request.Username,
                request.Password
            );

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    message = "Usuario o contraseña incorrectos."
                });
            }

            string rol = usuario.lRol_id == 1
                ? "Superadministrador"
                : "Administrador";

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.lUsuario_id.ToString()
                ),
                new Claim(ClaimTypes.Name, usuario.sUsername),
                new Claim(ClaimTypes.Role, rol)
            };

            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "Falta configurar Jwt:Key."
                );

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var expiration = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                message = "Inicio de sesión exitoso.",
                token = tokenString,
                expiresAt = expiration,
                usuario = new
                {
                    id = usuario.lUsuario_id,
                    username = usuario.sUsername,
                    rol = rol
                }
            });
        }
    }
}