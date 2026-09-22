using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class TblEvaluacionVisitante
    {
        public int lEvaluacion_id { get; set; }
        public int lProyecto_id { get; set; }
        public int lVisitante_id { get; set; }
        public int sPuntaje { get; set; }
        public DateTime sEvaluacion_fecha { get; set; }
        public string sEvaluacion_desc { get; set; }
    }
}
