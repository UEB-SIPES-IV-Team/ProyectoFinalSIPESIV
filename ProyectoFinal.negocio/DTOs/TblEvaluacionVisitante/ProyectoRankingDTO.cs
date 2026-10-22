using System;

namespace ProyectoFinal.Negocio.DTOs.TblEvaluacionVisitante
{
    public class ProyectoRankingDTO
    {
        public int lProyecto_id { get; set; }
        public string sProyecto_nm { get; set; } = string.Empty;
        public int TotalVisitantes { get; set; }
        public decimal PuntajeTotalAcumulado { get; set; }
        public decimal PorcentajePromedio { get; set; }
    }
}