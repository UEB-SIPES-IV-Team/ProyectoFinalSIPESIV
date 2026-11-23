using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class TblPremiacion
    {
        public int lPremiacion_id { get; set; }
        public int lProyecto_id { get; set; }
        public int lEvento_id { get; set; }
        public string sPremiacion_tipo { get; set; }
        public string sPosicion {  get; set; }
    }
    public class PremiacionDetalleEvento
    {
        public int lPremiacion_id { get; set; }
        public int lProyecto_id { get; set; }
        public string sProyecto_nm { get; set; } = string.Empty;
        public int lEvento_id { get; set; }
        public string sEvento_nm { get; set; } = string.Empty;
        public string sPremiacion_tipo { get; set; } = string.Empty;
        public int sPosicion { get; set; }
        public decimal NotaPromedio { get; set; }
        public bool EventoCerrado { get; set; }
    }
}
