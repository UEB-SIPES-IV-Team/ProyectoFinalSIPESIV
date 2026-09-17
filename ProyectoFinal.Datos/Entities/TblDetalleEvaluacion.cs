using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class TblDetalleEvaluacion
    {
        public int lDetalleEvaluacion_id { get; set; }
        public int lPEvaluacion_id { get; set; }
        public int lEvaluacionJurado_id { get; set; }
        public int sPuntaje { get; set; }
     
    }
}
