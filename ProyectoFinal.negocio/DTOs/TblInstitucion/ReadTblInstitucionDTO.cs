using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblInstitucion
{
    public class ReadTblInstitucionDTO
    {
        public int lInstitucion_id { get; set; }
        public int lSubParametro_id { get; set; }
        public string sInstitucion_nm { get; set; }
        public string sInstitucion_slug { get; set; }
        public bool bInstitucion_estado { get; set; }
    }
}
