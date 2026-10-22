using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class TblProyectoXPersona
    {
        public int lProyectoXPersona_id { get; set; }
        public int lProyecto_id { get; set; }
        public int lPersona_id { get; set; }

        public string sPersona_nm { get; set; } 
        public string sPersona_aps { get; set; } 
    }
}
