using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblVisitante
{
    public class UpdateTblVisitanteDTO
    {
        public int lVisitante_id { get; set; }
        public int lEvento_id { get; set; }
        public string sVisitante_nm { get; set; }
        public string sVisitante_email { get; set; }
        public string sVisitante_telf { get; set; }
        public string sVisitante_ci { get; set; }
        public string sVisitante_inst { get; set; }
        public DateOnly sVisitante_fecha_nacimiento { get; set; }
        public string sVisitante_sexo { get; set; }
        public string sVisitante_carrera { get; set; }
    }
}
