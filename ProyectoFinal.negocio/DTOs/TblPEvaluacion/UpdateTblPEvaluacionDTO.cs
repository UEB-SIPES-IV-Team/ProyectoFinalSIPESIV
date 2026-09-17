using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblPEvaluacion
{
    public class UpdateTblPEvaluacionDTO
    {
        public int lPEvaluacion_id { get; set; }
        public string sPEvaluacion_nm { get; set; }
        public string sPEvaluacion_desc { get; set; }
        public int sPEvaluacion_peso { get; set; }
    }
}

