using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblEvento
{
    public class ReadTblEventoDTO
    {
        public int lEvento_id { get; set; }
        public int lInstitucion_id { get; set; }
        public string sEvento_nm { get; set; }
        public string sAnio { get; set; }
        public string sGestion { get; set; }
        public DateTime sFecha_ini { get; set; }
        public DateTime sFecha_fin { get; set; }
        public bool sEvento_estado { get; set; }
    }

}