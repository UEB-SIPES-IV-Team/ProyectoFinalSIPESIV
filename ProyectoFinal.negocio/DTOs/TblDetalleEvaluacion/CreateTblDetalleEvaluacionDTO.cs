using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblDetalleEvaluacion
{
    public class CreateTblDetalleEvaluacionDTO
    {
        public int lPEvaluacion_id { get; set; }
        public int lEvaluacionJurado_id { get; set; }
        public int sPuntaje { get; set; }
    }
}
