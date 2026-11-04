using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
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

                // Asignación de Rol
                string rol = usuario.lRol_id == 1
                    ? "Superadministrador"
                    : "Administrador";

                // Creación de Claims para el JWT
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.lUsuario_id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.sUsername),
                    new Claim(ClaimTypes.Role, rol)
                };

                var jwtKey = _configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("Falta configurar Jwt:Key en appsettings.json.");

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

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // CORRECCIÓN 1: Estructura anidada con el objeto "usuario" para la app WPF
                return Ok(new
                {
                    message = "Inicio de sesión exitoso.",
                    token = tokenString,
                    expiresAt = expiration,
                    usuario = new
                    {
                        id = usuario.lUsuario_id,
                        username = usuario.sUsername,
                        rol = rol,
                        lRol_id = usuario.lRol_id,
                        rolId = usuario.lRol_id
                    }
                });
            }
            catch (Exception ex)
            {
                // CORRECCIÓN 2: Captura del error interno para revelar la causa real del 500
                string detalleError = ex.InnerException != null
                    ? $"{ex.Message} -> {ex.InnerException.Message}"
                    : ex.Message;

                return StatusCode(500, new
                {
                    message = $"Error en backend: {detalleError}"
                });
            }
        }
    }
}