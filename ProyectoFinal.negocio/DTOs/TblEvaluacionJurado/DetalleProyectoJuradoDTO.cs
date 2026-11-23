namespace ProyectoFinal.Negocio.DTOs.TblEvaluacionJurado
{
    public class DetalleProyectoJuradoDTO
    {
        public int lProyecto_id { get; set; }
        public string sProyecto_nm { get; set; } = string.Empty;
        public string lAsignatura_nm { get; set; } = string.Empty;
        public string sProyecto_tipo { get; set; } = string.Empty;
        public int lEvaluacionJurado_id { get; set; }
        public DateTime? sEvaluacionJurado_fecha { get; set; }
        public decimal NotaAsignada { get; set; }
        public decimal PromedioGeneral { get; set; }
        public string Estado { get; set; } = "Pendiente";
    }
}