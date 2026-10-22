namespace ProyectoFinal.Datos.Entities
{
    public class TblProyecto
    {
        public int lProyecto_id { get; set; }
        public int lAsignatura_id { get; set; }
        public int lEvento_id { get; set; }
        public string sProyecto_nm { get; set; } = string.Empty;
        public string sProyecto_desc { get; set; } = string.Empty;
        public string sProyecto_tipo { get; set; } = string.Empty;
        public bool sProyecto_estado { get; set; }
        public string sProyecto_video { get; set; } = string.Empty;

        public string lAsignatura_nm { get; set; } = string.Empty;
    }
}