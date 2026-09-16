using ProyectoFinal.Datos.DataAccess;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Datos.Repository;
using ProyectoFinal.Negocio.Interfaces;
using ProyectoFinal.Negocio.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Controladores y Swagger/OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


// Base de datos
builder.Services.AddScoped<ProyectoFinalDatabase>();

// Repositorios
builder.Services.AddScoped<ITblAreaRepository, TblAreaRepository>();
builder.Services.AddScoped<ITblAsignaturaRepository, TblAsignaturaRepository>();
//builder.Services.AddScoped<ITblDetalleEvaluacionRepository, TblDetalleEvaluacionRepository>();
builder.Services.AddScoped<ITblEvaluacionJuradoRepository, TblEvaluacionJuradoRepository>();
builder.Services.AddScoped<ITblEvaluacionVisitanteRepository, TblEvaluacionVisitanteRepository>();
builder.Services.AddScoped<ITblEventoRepository, TblEventoRepository>();
builder.Services.AddScoped<ITblInstitucionRepository, TblInstitucionRepository>();
//builder.Services.AddScoped<ITblJuradoRepository, TblJuradoRepository>();
builder.Services.AddScoped<ITblJuradoXUnidadOrganizativaRepository, TblJuradoXUnidadOrganizativaRepository>();
builder.Services.AddScoped<ITblParametroRepository, TblParametroRepository>();
builder.Services.AddScoped<ITblPermisosRepository, TblPermisosRepository>();
//builder.Services.AddScoped<ITblPersonaRepository, TblPersonaRepository>();
//builder.Services.AddScoped<ITblPEvaluacionRepository, TblPEvaluacionRepository>();
builder.Services.AddScoped<ITblPremiacionRepository, TblPremiacionRepository>();
builder.Services.AddScoped<ITblProgramRepository, TblProgramRepository>();
builder.Services.AddScoped<ITblProyectoRepository, TblProyectoRepository>();
builder.Services.AddScoped<ITblProyectoXPersonaRepository, TblProyectoXPersonaRepository>();
builder.Services.AddScoped<ITblRolRepository, TblRolRepository>();
builder.Services.AddScoped<ITblSubParametroRepository, TblSubParametroRepository>();
builder.Services.AddScoped<ITblUnidadOrganizativaRepository, TblUnidadOrganizativaRepository>();
builder.Services.AddScoped<ITblUsuarioRepository, TblUsuarioRepository>();
builder.Services.AddScoped<ITblVisitanteRepository, TblVisitanteRepository>();

// Servicios (Corregido TblAreaService)
builder.Services.AddScoped<ITblAreaService, TblAreaService>();
builder.Services.AddScoped<ITblAsignaturaService, TblAsignaturaService>();
//builder.Services.AddScoped<ITblDetalleEvaluacionService, TblDetalleEvaluacionService>();
builder.Services.AddScoped<ITblEvaluacionJuradoService, TblEvaluacionJuradoService>();
builder.Services.AddScoped<ITblEvaluacionVisitanteService, TblEvaluacionVisitanteService>();
builder.Services.AddScoped<ITblEventoService, TblEventoService>();
builder.Services.AddScoped<ITblInstitucionService, TblInstitucionService>();
//builder.Services.AddScoped<ITblJuradoService, TblJuradoService>();
builder.Services.AddScoped<ITblJuradoXUnidadOrganizativaService, TblJuradoXUnidadOrganizativaService>();
builder.Services.AddScoped<ITblParametroService, TblParametroService>();
builder.Services.AddScoped<ITblPermisosService, TblPermisosService>();
//builder.Services.AddScoped<ITblPersonaService, TblPersonaService>();
//builder.Services.AddScoped<ITblPEvaluacionService, TblPEvaluacionService>();
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();