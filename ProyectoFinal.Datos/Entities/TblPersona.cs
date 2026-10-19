using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class TblPersona
    {
        public int lPersona_id { get; set; }
        public string sPersona_nm { get; set; }
        public string sPersona_aps { get; set; }
        public string sPersona_email { get; set; }
        public string sPersona_telf{ get; set; }
        public string sPersona_sexo { get; set; }
        public DateOnly dPersona_fecha_nacimiento { get; set; }
        public string sPersona_ci { get; set; }


    }
}
