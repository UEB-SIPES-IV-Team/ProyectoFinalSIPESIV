
using System;

namespace ProyectoFinal.Negocio.DTOs.TblPersona
{
    public class CreateTblPersonaDTO
    {
        public string sPersona_nm { get; set; } = "";
        public string sPersona_aps { get; set; } = "";
        public string sPersona_email { get; set; } = "";
        public string sPersona_telf { get; set; } = "";
        public string sPersona_tipo_persona { get; set; } = "";
        public string sPersona_sexo { get; set; } = "";
        public string sPersona_ci { get; set; } = "";
        public DateOnly dPersona_fecha_nacimiento { get; set; }
    }
}
