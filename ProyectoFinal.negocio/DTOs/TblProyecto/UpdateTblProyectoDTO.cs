using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblProyecto
{
    public class UpdateTblProyectoDTO
    {
        public int lProyecto_id { get; set; }
        public int lAsignatura_id { get; set; }
        public int lEvento_id { get; set; }
        public string sProyecto_nm { get; set; }
        public string sProyecto_desc { get; set; }
        public string sProyecto_tipo { get; set; }
        public bool sProyecto_estado { get; set; }
        public string sProyecto_video { get; set; }
    }
}
