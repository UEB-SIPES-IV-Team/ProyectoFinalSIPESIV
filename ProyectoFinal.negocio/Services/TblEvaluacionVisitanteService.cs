using ProyectoFinal.Datos.Entities;
using ProyectoFinal.Datos.Interfaces;
using ProyectoFinal.Negocio.DTOs.TblEvaluacionVisitante;
using ProyectoFinal.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Negocio.Services
{
    public class TblEvaluacionVisitanteService : ITblEvaluacionVisitanteService
    {
        private readonly ITblEvaluacionVisitanteRepository _tblEvaluacionVisitanteRepository;
        private readonly ITblProyectoRepository _tblProyectoRepository;

        public TblEvaluacionVisitanteService(
            ITblEvaluacionVisitanteRepository tblEvaluacionVisitanteRepository,
            ITblProyectoRepository tblProyectoRepository)
        {
            _tblEvaluacionVisitanteRepository = tblEvaluacionVisitanteRepository;
            _tblProyectoRepository = tblProyectoRepository;
        }

        public async Task<ReadTblEvaluacionVisitanteDTO?> ObtenerPorVisitanteYProyecto(int idVisitante, int idProyecto)
        {
            var result = await _tblEvaluacionVisitanteRepository.ObtenerPorVisitanteYProyecto(idVisitante, idProyecto);
            if (result is null) return null;

            return new ReadTblEvaluacionVisitanteDTO
            {
                lEvaluacion_id = result.lEvaluacion_id,
                lProyecto_id = result.lProyecto_id,
                lVisitante_id = result.lVisitante_id,
                sPuntaje = result.sPuntaje,
                sEvaluacion_fecha = result.sEvaluacion_fecha,
                sEvaluacion_desc = result.sEvaluacion_desc
            };
        }

        public async Task Actualizar(UpdateTblEvaluacionVisitanteDTO evaluacionVisitante)
        {
            TblEvaluacionVisitante objEvaluacionVisitante = new TblEvaluacionVisitante
            {
                lEvaluacion_id = evaluacionVisitante.lEvaluacion_id,
                lProyecto_id = evaluacionVisitante.lProyecto_id,
                lVisitante_id = evaluacionVisitante.lVisitante_id,
                sPuntaje = evaluacionVisitante.sPuntaje,
                sEvaluacion_fecha = evaluacionVisitante.sEvaluacion_fecha,
                sEvaluacion_desc = evaluacionVisitante.sEvaluacion_desc
            };
            await _tblEvaluacionVisitanteRepository.Actualizar(objEvaluacionVisitante);
        }

        public async Task Crear(CreateTblEvaluacionVisitanteDTO evaluacionVisitante)
        {
            bool esEventoActivo = await _tblEvaluacionVisitanteRepository.ValidarEventoActivoPorProyecto(evaluacionVisitante.lProyecto_id);

            if (!esEventoActivo)
            {
                throw new InvalidOperationException("El evento se encuentra cerrado o finalizado. No se permiten más votaciones.");
            }

            TblEvaluacionVisitante objEvaluacionVisitante = new TblEvaluacionVisitante
            {
                lProyecto_id = evaluacionVisitante.lProyecto_id,
                lVisitante_id = evaluacionVisitante.lVisitante_id,
                sPuntaje = evaluacionVisitante.sPuntaje,
                sEvaluacion_fecha = DateTime.Now,
                sEvaluacion_desc = evaluacionVisitante.sEvaluacion_desc
            };

            await _tblEvaluacionVisitanteRepository.Crear(objEvaluacionVisitante);
        }

        public async Task Eliminar(int idEvaluacionVisitante)
        {
            await _tblEvaluacionVisitanteRepository.Eliminar(idEvaluacionVisitante);
        }

        public async Task<ReadTblEvaluacionVisitanteDTO> ObtenerPorId(int idEvaluacionVisitante)
        {
            var result = await _tblEvaluacionVisitanteRepository.ObtenerPorId(idEvaluacionVisitante);
            if (result is null) return null!;
            return new ReadTblEvaluacionVisitanteDTO
            {
                lEvaluacion_id = result.lEvaluacion_id,
                lProyecto_id = result.lProyecto_id,
                lVisitante_id = result.lVisitante_id,
                sPuntaje = result.sPuntaje,
                sEvaluacion_fecha = result.sEvaluacion_fecha,
                sEvaluacion_desc = result.sEvaluacion_desc
            };
        }

        public async Task<List<ReadTblEvaluacionVisitanteDTO>> ObtenerTodos()
        {
            var evaluacionVisitantes = await _tblEvaluacionVisitanteRepository.ObtenerTodos();
            var EvaluacionVisitantesDTO = new List<ReadTblEvaluacionVisitanteDTO>();

            foreach (var evaluacionVisitante in evaluacionVisitantes)
            {
                EvaluacionVisitantesDTO.Add(new ReadTblEvaluacionVisitanteDTO
                {
                    lEvaluacion_id = evaluacionVisitante.lEvaluacion_id,
                    lProyecto_id = evaluacionVisitante.lProyecto_id,
                    lVisitante_id = evaluacionVisitante.lVisitante_id,
                    sPuntaje = evaluacionVisitante.sPuntaje,
                    sEvaluacion_fecha = evaluacionVisitante.sEvaluacion_fecha,
                    sEvaluacion_desc = evaluacionVisitante.sEvaluacion_desc
                });
            }

            return EvaluacionVisitantesDTO;
        }

        public async Task<List<ProyectoRankingDTO>> ObtenerRanking(int idEvento)
        {
            try
            {
                var evaluaciones = await _tblEvaluacionVisitanteRepository.ObtenerEvaluacionesPorEvento(idEvento);
                if (evaluaciones == null || !evaluaciones.Any())
                    return new List<ProyectoRankingDTO>();

                var proyectos = await _tblProyectoRepository.ObtenerTodos();
                var dicProyectos = proyectos != null
                    ? proyectos.GroupBy(p => p.lProyecto_id).ToDictionary(g => g.Key, g => g.First().sProyecto_nm)
                    : new Dictionary<int, string>();

                return evaluaciones
                    .Where(e => e != null && e.lProyecto_id > 0)
                    .GroupBy(e => e.lProyecto_id)
                    .Select(g =>
                    {
                        string nombreProyecto = dicProyectos.TryGetValue(g.Key, out string? nombreReal) && !string.IsNullOrWhiteSpace(nombreReal)
                            ? nombreReal.ToUpper()
                            : $"PROYECTO #{g.Key}";

                        int cantidadVisitantesUnicos = g.Select(x => x.lVisitante_id).Distinct().Count();
                        decimal promedioEstrellas = (decimal)g.Average(e => (double)e.sPuntaje);
                        decimal promedioPorcentaje = Math.Min(100m, Math.Round(promedioEstrellas * 20m, 1));

                        return new ProyectoRankingDTO
                        {
                            lProyecto_id = g.Key,
                            sProyecto_nm = nombreProyecto,
                            TotalVisitantes = cantidadVisitantesUnicos,
                            PuntajeTotalAcumulado = g.Sum(e => e.sPuntaje),
                            PorcentajePromedio = promedioPorcentaje
                        };
                    })
                    .OrderByDescending(r => r.PorcentajePromedio)
                    .ThenByDescending(r => r.TotalVisitantes)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener ranking: {ex.Message}");
                return new List<ProyectoRankingDTO>();
            }
        }

        public async Task<List<MovimientoItemDTO>> ObtenerRecientes(int idEvento)
        {
            try
            {
                var evaluaciones = await _tblEvaluacionVisitanteRepository.ObtenerEvaluacionesPorEvento(idEvento);
                if (evaluaciones == null || !evaluaciones.Any())
                    return new List<MovimientoItemDTO>();

                var proyectos = await _tblProyectoRepository.ObtenerTodos();
                var dicProyectos = proyectos != null
                    ? proyectos.GroupBy(p => p.lProyecto_id).ToDictionary(g => g.Key, g => g.First().sProyecto_nm)
                    : new Dictionary<int, string>();

                return evaluaciones
                    .Where(e => e != null && e.lProyecto_id > 0)
                    .GroupBy(e => e.lProyecto_id)
                    .Select(g =>
                    {
                        var ultimoVoto = g.OrderByDescending(x => x.sEvaluacion_fecha).FirstOrDefault();

                        string nombreProyecto = dicProyectos.TryGetValue(g.Key, out string? nombreReal) && !string.IsNullOrWhiteSpace(nombreReal)
                            ? nombreReal.ToUpper()
                            : $"PROYECTO #{g.Key}";

                        double promedioEstrellas = g.Average(x => (double)x.sPuntaje);
                        double porcentajeExacto = Math.Min(100.0, Math.Round(promedioEstrellas * 20.0, 1));
                        int porcentajeRedondeado = (int)Math.Round(porcentajeExacto);

                        // Forzar hora local para evitar el desfase de 15 horas
                        DateTime fechaUltimoVoto = ultimoVoto?.sEvaluacion_fecha ?? DateTime.Now;
                        fechaUltimoVoto = DateTime.SpecifyKind(fechaUltimoVoto, DateTimeKind.Local);

                        TimeSpan diferencia = DateTime.Now - fechaUltimoVoto;
                        if (diferencia.TotalSeconds < 0)
                            diferencia = TimeSpan.Zero;

                        string tiempoTexto;
                        if (diferencia.TotalMinutes < 1)
                        {
                            tiempoTexto = $"Hace un momento ({fechaUltimoVoto:HH:mm})";
                        }
                        else if (diferencia.TotalHours < 1)
                        {
                            int min = (int)diferencia.TotalMinutes;
                            tiempoTexto = $"Hace {min} min ({fechaUltimoVoto:HH:mm})";
                        }
                        else if (fechaUltimoVoto.Date == DateTime.Today)
                        {
                            tiempoTexto = $"Hoy a las {fechaUltimoVoto:HH:mm}";
                        }
                        else
                        {
                            int dias = Math.Max(1, (int)diferencia.TotalDays);
                            tiempoTexto = $"Hace {dias} {(dias == 1 ? "día" : "días")} ({fechaUltimoVoto:dd/MM HH:mm})";
                        }

                        return new
                        {
                            Item = new MovimientoItemDTO
                            {
                                Descripcion = nombreProyecto,
                                Tiempo = tiempoTexto,
                                Posicion = $"{porcentajeRedondeado}%"
                            },
                            PorcentajeOrden = porcentajeExacto,
                            TotalVotos = g.Count()
                        };
                    })
                    .OrderByDescending(x => x.PorcentajeOrden)
                    .ThenByDescending(x => x.TotalVotos)
                    .Select(x => x.Item)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ObtenerRecientes Service: {ex.Message}");
                return new List<MovimientoItemDTO>();
            }
        }
    }
}