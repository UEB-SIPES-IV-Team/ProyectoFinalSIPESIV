using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblProyectoXPersona
{
    public class ReadTblProyectoXPersonaDTO
    {
        public int lProyectoXPersona_id { get; set; }
        public int lProyecto_id { get; set; }
        public int lPersona_id { get; set; }
        public string sPersona_nm { get; set; } = string.Empty;
        public string sPersona_aps { get; set; } = string.Empty;
    }
}
