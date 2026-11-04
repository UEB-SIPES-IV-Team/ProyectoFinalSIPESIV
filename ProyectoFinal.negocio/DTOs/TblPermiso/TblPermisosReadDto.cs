using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Negocio.DTOs.TblPermiso
{
    public class TblPermisosReadDto
    {
        public int lPermisos_id { get; set; }
        public int lRol_id { get; set; }
        public int lProgram_id { get; set; }
        public string sProgram_nm { get; set; }
        public bool insertar { get; set; }
        public bool actualizar { get; set; }
        public bool consultar { get; set; }
        public bool eliminar { get; set; }
    }
}
