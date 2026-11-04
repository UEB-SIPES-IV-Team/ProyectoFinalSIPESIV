using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.Auth
{
    public class ReadLoginResponseDTO
    {
        public string JwtToken { get; set; }
        public ReadBasicUserInformationDTO UserInfo { get; set; }
    }
    public class ReadBasicUserInformationDTO
    {
        public long lUsuario_id { get; set; }
        public string sUsername { get; set; }
        public string sRol_nombre { get; set; } 
        public bool bEstado { get; set; }
    }
}
