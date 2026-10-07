using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Datos.Entities
{
    public class TblJuradoXUnidadOrganizativa
    {
        public int lJuradoXFacultad_id {  get; set; }
        public int lJurado_id { get; set; }
        public int lEvento_id { get; set; }
        public int lUniversidadOrganizativa_id {  get; set; }
    }
}
