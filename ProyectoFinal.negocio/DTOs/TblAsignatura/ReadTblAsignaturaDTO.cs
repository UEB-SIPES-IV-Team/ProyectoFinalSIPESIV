using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblAsignatura
{
    public class ReadTblAsignaturaDTO
    {
        public int lAsignatura_id { get; set; }
        public int lArea_id { get; set; }
        public string lAsignatura_slog { get; set; }
        public string lAsignatura_nm { get; set; }
        public bool bEstado { get; set; }
    }
}