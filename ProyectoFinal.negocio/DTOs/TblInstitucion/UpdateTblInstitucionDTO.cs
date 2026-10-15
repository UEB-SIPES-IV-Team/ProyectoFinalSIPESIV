using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblInstitucion
{
    public class UpdateTblInstitucionDTO
    {
        public int lInstitucion_id { get; set; }
        public string sInstitucion_tipo { get; set; }
        public string sInstitucion_nm { get; set; }
        public string sInstitucion_slug { get; set; }
        public bool bInstitucion_estado { get; set; }
    }
}
