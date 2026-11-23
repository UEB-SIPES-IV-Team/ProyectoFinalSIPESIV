using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class TblEvaluacionJurado
    {
        public int lEvaluacionJurado_id { get; set; }
        public int lProyecto_id { get; set; }
        public int lJurado_id { get; set; }
        public DateTime? sEvaluacionJurado_fecha { get; set; }
        public string? sEvaluacionJurado_obs { get; set; }

        // Propiedades adicionales que devuelve la función de consulta por jurado
        public string sProyecto_nm { get; set; } = string.Empty;
        public string lAsignatura_nm { get; set; } = string.Empty;
        public string sProyecto_tipo { get; set; } = string.Empty;
        public decimal NotaAsignada { get; set; }
        public decimal PromedioGeneral { get; set; }
        public string Estado { get; set; } = "Pendiente";
    }
}