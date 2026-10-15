using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ProyectoFinal.Datos.Entities
{
    public class TblInstitucion
    {
        [Column("linstitucion_id")]
        public int lInstitucion_id { get; set; }

        [Column("sinstitucion_tipo")]
        public string sInstitucion_tipo { get; set; }

        [Column("sinstitucion_nm")]
        public string sInstitucion_nm { get; set; }

        [Column("sinstitucion_slug")]
        public string sInstitucion_slug { get; set; }

        [Column("binstitucion_estado")]
        public bool bInstitucion_estado { get; set; }
    }
}