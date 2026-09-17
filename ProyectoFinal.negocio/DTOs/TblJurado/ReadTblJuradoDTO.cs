using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblJurado
{
    public class ReadTblJuradoDTO
    {
        public int lJurado_id { get; set; }
        public int lPersona_id { get; set; }
        public string sJurado_institucion { get; set; }
        public bool sJurado_gral { get; set; }
    }
}
