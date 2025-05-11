namespace IndicadoresApp.Clases
{
    public class ResponsableModel
    {
        public int IdResponsable { get; set; }
        public int IdIndicador { get; set; }
        public DateTime FechaAsignacion { get; set; } = DateTime.Today;
    }
}
