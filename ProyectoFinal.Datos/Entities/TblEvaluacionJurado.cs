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

      
    }
}