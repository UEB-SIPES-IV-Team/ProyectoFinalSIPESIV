using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblProyecto
{
    public class ProyectoPorUnidadDTO
    {
        public int lProyecto_id { get; set; }
        public int lAsignatura_id { get; set; }
        public string lAsignatura_nm { get; set; } = string.Empty;
        public int lEvento_id { get; set; }
        public string sEvento_nm { get; set; } = string.Empty;
        public string sProyecto_nm { get; set; } = string.Empty;
        public string? sProyecto_desc { get; set; }
        public string? sProyecto_tipo { get; set; }
        public bool sProyecto_estado { get; set; }
        public string? sProyecto_video { get; set; }
    }

    public class FiltroProyectosPorUnidadesDTO
    {
        public int lEvento_id { get; set; }
        public List<int> UnidadesIds { get; set; } = new();
    }
}
