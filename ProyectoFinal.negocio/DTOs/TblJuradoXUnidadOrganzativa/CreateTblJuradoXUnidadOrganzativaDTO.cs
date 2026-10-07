using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblJuradoXUnidadOrganzativa
{
    public class CreateTblJuradoXUnidadOrganzativaDTO
    {
        public int lJurado_id { get; set; }
        public int lEvento_id { get; set; }
        public int lUniversidadOrganizativa_id { get; set; }
    }
}
