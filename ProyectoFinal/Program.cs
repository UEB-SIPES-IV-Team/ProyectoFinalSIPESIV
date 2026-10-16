using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;
using Scalar.AspNetCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Falta configurar Jwt:Key.");

if (Encoding.UTF8.GetByteCount(jwtKey) < 32)
    throw new InvalidOperationException(
        "La clave JWT debe tener al menos 32 bytes.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Configuración de Controladores y Swagger/OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;


// Base de datos
builder.Services.AddScoped<ProyectoFinalDatabase>();

// Repositorios
builder.Services.AddScoped<ITblAreaRepository, TblAreaRepository>();
builder.Services.AddScoped<ITblAsignaturaRepository, TblAsignaturaRepository>();
builder.Services.AddScoped<ITblDetalleEvaluacionRepository, TblDetalleEvaluacionRepository>();
builder.Services.AddScoped<ITblEvaluacionJuradoRepository, TblEvaluacionJuradoRepository>();
builder.Services.AddScoped<ITblEvaluacionVisitanteRepository, TblEvaluacionVisitanteRepository>();
builder.Services.AddScoped<ITblEventoRepository, TblEventoRepository>();
builder.Services.AddScoped<ITblInstitucionRepository, TblInstitucionRepository>();
builder.Services.AddScoped<ITblJuradoRepository, TblJuradoRepository>();
builder.Services.AddScoped<ITblJuradoXUnidadOrganizativaRepository, TblJuradoXUnidadOrganizativaRepository>();
builder.Services.AddScoped<ITblParametroRepository, TblParametroRepository>();
builder.Services.AddScoped<ITblPermisosRepository, TblPermisosRepository>();
builder.Services.AddScoped<ITblPersonaRepository, TblPersonaRepository>();
builder.Services.AddScoped<ITblPEvaluacionRepository, TblPEvaluacionRepository>();
builder.Services.AddScoped<ITblPremiacionRepository, TblPremiacionRepository>();
builder.Services.AddScoped<ITblProgramRepository, TblProgramRepository>();
builder.Services.AddScoped<ITblProyectoRepository, TblProyectoRepository>();
builder.Services.AddScoped<ITblProyectoXPersonaRepository, TblProyectoXPersonaRepository>();
builder.Services.AddScoped<ITblRolRepository, TblRolRepository>();
builder.Services.AddScoped<ITblSubParametroRepository, TblSubParametroRepository>();
builder.Services.AddScoped<ITblUnidadOrganizativaRepository, TblUnidadOrganizativaRepository>();
builder.Services.AddScoped<ITblUsuarioRepository, TblUsuarioRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ITblVisitanteRepository, TblVisitanteRepository>();

// Servicios (Corregido TblAreaService)
builder.Services.AddScoped<ITblAreaService, TblAreaService>();
builder.Services.AddScoped<ITblAsignaturaService, TblAsignaturaService>();
builder.Services.AddScoped<ITblDetalleEvaluacionService, TblDetalleEvaluacionService>();
builder.Services.AddScoped<ITblEvaluacionJuradoService, TblEvaluacionJuradoService>();
builder.Services.AddScoped<ITblEvaluacionVisitanteService, TblEvaluacionVisitanteService>();
builder.Services.AddScoped<ITblEventoService, TblEventoService>();
builder.Services.AddScoped<ITblInstitucionService, TblInstitucionService>();
builder.Services.AddScoped<ITblJuradoService, TblJuradoService>();
builder.Services.AddScoped<ITblJuradoXUnidadOrganizativaService, TblJuradoXUnidadOrganizativaService>();
builder.Services.AddScoped<ITblParametroService, TblParametroService>();
builder.Services.AddScoped<ITblPermisosService, TblPermisosService>();
builder.Services.AddScoped<ITblPersonaService, TblPersonaService>();
builder.Services.AddScoped<ITblPEvaluacionService, TblPEvaluacionService>();
builder.Services.AddScoped<ITblPremiacionService, TblPremiacionService>();
builder.Services.AddScoped<ITblProgramService, TblProgramService>();
builder.Services.AddScoped<ITblProyectoService, TblProyectoService>();
builder.Services.AddScoped<ITblProyectoXPersonaService, TblProyectoXPersonaService>();
builder.Services.AddScoped<ITblRolService, TblRolService>();
builder.Services.AddScoped<ITblSubParametroService, TblSubParametroService>();
builder.Services.AddScoped<ITblUnidadOrganizativaService, TblUnidadOrganizativaService>();
builder.Services.AddScoped<ITblUsuarioService, TblUsuarioService>();
builder.Services.AddScoped<ITblVisitanteService, TblVisitanteService>();

var app = builder.Build();

// HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
}
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment() &&
    args.Contains("--seed-superadmin"))
{
    using var scope = app.Services.CreateScope();

    var repository = scope.ServiceProvider
        .GetRequiredService<ITblUsuarioRepository>();

    var existente = await repository
        .ObtenerPorUsername("superadmin");

    if (existente != null)
    {
        Console.WriteLine(
            "El usuario superadmin ya existe."
        );
    }
    else
    {
        var password = Environment.GetEnvironmentVariable(
            "SIPESIV_INITIAL_ADMIN_PASSWORD");

        if (string.IsNullOrWhiteSpace(password) ||
            password.Length < 12)
        {
            throw new InvalidOperationException(
                "Configura SIPESIV_INITIAL_ADMIN_PASSWORD " +
                "con una contraseña de al menos 12 caracteres."
            );
        }

        var usuario = new ProyectoFinal.Datos.Entities.TblUsuario
        {
            lPersona_id = 1,
            lRol_id = 1,
            sUsername = "superadmin",
            bEstado = true
        };

        var hasher = new Microsoft.AspNetCore.Identity
            .PasswordHasher<
                ProyectoFinal.Datos.Entities.TblUsuario>();

        usuario.sPassword = hasher.HashPassword(
            usuario,
            password
        );

        await repository.Crear(usuario);

        Console.WriteLine(
            "Superadministrador creado correctamente."
        );
    }

    return;
}

app.Run();