using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class PerfilUsuarioEntity
    {
        public int lUsuario_id { get; set; }
        public int lPersona_id { get; set; }
        public string sUsername { get; set; } = string.Empty;
        public string sPersona_nm { get; set; } = string.Empty;
        public string sPersona_aps { get; set; } = string.Empty;
        public string sPersona_ci { get; set; } = string.Empty;
        public string sPersona_email { get; set; } = string.Empty;
        public string sPersona_telf { get; set; } = string.Empty;
        public string sRol_nm { get; set; } = string.Empty;
    }
}
