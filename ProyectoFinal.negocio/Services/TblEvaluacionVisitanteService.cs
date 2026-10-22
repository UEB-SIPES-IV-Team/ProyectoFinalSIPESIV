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

        public TblEvaluacionVisitanteService(ITblEvaluacionVisitanteRepository tblEvaluacionVisitanteRepository)
        {
            _tblEvaluacionVisitanteRepository = tblEvaluacionVisitanteRepository;
        }

        // =========================================================================
        // 🟢 NUEVO: OBTENER EVALUACIÓN POR VISITANTE Y PROYECTO
        // =========================================================================
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
                var evaluacionVisitanteDTO = new ReadTblEvaluacionVisitanteDTO
                {
                    lEvaluacion_id = evaluacionVisitante.lEvaluacion_id,
                    lProyecto_id = evaluacionVisitante.lProyecto_id,
                    lVisitante_id = evaluacionVisitante.lVisitante_id,
                    sPuntaje = evaluacionVisitante.sPuntaje,
                    sEvaluacion_fecha = evaluacionVisitante.sEvaluacion_fecha,
                    sEvaluacion_desc = evaluacionVisitante.sEvaluacion_desc
                };
                EvaluacionVisitantesDTO.Add(evaluacionVisitanteDTO);
            }

            return EvaluacionVisitantesDTO;
        }

        // =========================================================================
        // 🟢 RANKING: NOMBRE REAL Y VISITANTES ÚNICOS
        // =========================================================================
        public async Task<List<ProyectoRankingDTO>> ObtenerRanking(int idEvento)
        {
            try
            {
                var evaluaciones = await _tblEvaluacionVisitanteRepository.ObtenerEvaluacionesPorEvento(idEvento);

                if (evaluaciones == null || !evaluaciones.Any())
                    return new List<ProyectoRankingDTO>();

                var ranking = evaluaciones
                    .Where(e => e != null && e.lProyecto_id > 0)
                    .GroupBy(e => e.lProyecto_id)
                    .Select(g =>
                    {
                        var ultimoVoto = g.OrderByDescending(x => x.sEvaluacion_fecha).FirstOrDefault();

                        string nombreProyecto = (!string.IsNullOrWhiteSpace(ultimoVoto?.sEvaluacion_desc) && !ultimoVoto.sEvaluacion_desc.StartsWith("PROYECTO #"))
                            ? ultimoVoto.sEvaluacion_desc.ToUpper()
                            : $"PROYECTO #{g.Key}";

                        int cantidadVisitantesUnicos = g.Select(x => x.lVisitante_id).Distinct().Count();

                        decimal puntajeAcumulado = g.Sum(e => e.sPuntaje * 20);
                        decimal promedioPorcentaje = g.Any() ? Math.Round((decimal)g.Average(e => e.sPuntaje * 20), 1) : 0;

                        return new ProyectoRankingDTO
                        {
                            lProyecto_id = g.Key,
                            sProyecto_nm = nombreProyecto,
                            TotalVisitantes = cantidadVisitantesUnicos,
                            PuntajeTotalAcumulado = puntajeAcumulado,
                            PorcentajePromedio = promedioPorcentaje
                        };
                    })
                    .OrderByDescending(r => r.PuntajeTotalAcumulado)
                    .ToList();

                return ranking;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener ranking: {ex.Message}");
                return new List<ProyectoRankingDTO>();
            }
        }

        // =========================================================================
        // 🟢 MOVIMIENTOS RECIENTES
        // =========================================================================
        public async Task<List<MovimientoItemDTO>> ObtenerRecientes(int idEvento)
        {
            try
            {
                var recientes = await _tblEvaluacionVisitanteRepository.ObtenerRecientesPorEvento(idEvento, 100);

                if (recientes == null || !recientes.Any())
                    return new List<MovimientoItemDTO>();

                var listaValida = recientes.Where(e => e != null && e.lProyecto_id > 0).ToList();

                if (!listaValida.Any())
                    return new List<MovimientoItemDTO>();

                var listaMovimientos = listaValida
                    .GroupBy(e => e.lProyecto_id)
                    .Select(g =>
                    {
                        var ultimoVoto = g.OrderByDescending(x => x.sEvaluacion_fecha).FirstOrDefault();

                        string nombreProyecto = (!string.IsNullOrWhiteSpace(ultimoVoto?.sEvaluacion_desc) && !ultimoVoto.sEvaluacion_desc.StartsWith("PROYECTO #"))
                            ? ultimoVoto.sEvaluacion_desc.ToUpper()
                            : $"PROYECTO #{g.Key}";

                        int porcentajePromedio = g.Any() ? (int)Math.Round(g.Average(x => x.sPuntaje * 20)) : 0;

                        string tiempoTexto = "Hace un momento";
                        if (ultimoVoto != null && ultimoVoto.sEvaluacion_fecha.HasValue)
                        {
                            DateTime fechaVoto = ultimoVoto.sEvaluacion_fecha.Value.Kind == DateTimeKind.Utc
                                ? ultimoVoto.sEvaluacion_fecha.Value.ToLocalTime()
                                : ultimoVoto.sEvaluacion_fecha.Value;

                            TimeSpan diferencia = DateTime.Now - fechaVoto;

                            if (diferencia.TotalMinutes < 1)
                            {
                                tiempoTexto = "Hace un momento";
                            }
                            else if (diferencia.TotalMinutes <= 60)
                            {
                                int min = Math.Max(1, (int)diferencia.TotalMinutes);
                                tiempoTexto = $"Hace {min} min";
                            }
                            else if (diferencia.TotalHours < 24)
                            {
                                int horas = (int)diferencia.TotalHours;
                                tiempoTexto = $"Hace {horas} {(horas == 1 ? "hora" : "horas")}";
                            }
                            else
                            {
                                int dias = (int)diferencia.TotalDays;
                                tiempoTexto = $"Hace {dias} {(dias == 1 ? "día" : "días")}";
                            }
                        }

                        return new
                        {
                            Item = new MovimientoItemDTO
                            {
                                Descripcion = nombreProyecto,
                                Tiempo = tiempoTexto,
                                Posicion = $"{porcentajePromedio}%"
                            },
                            PorcentajePromedio = porcentajePromedio
                        };
                    })
                    .OrderByDescending(x => x.PorcentajePromedio)
                    .Select(x => x.Item)
                    .Take(5)
                    .ToList();

                return listaMovimientos;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ObtenerRecientes Service: {ex.Message}");
                return new List<MovimientoItemDTO>();
            }
        }
    }
}