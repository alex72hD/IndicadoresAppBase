namespace IndicadoresApp.Clases
{
    public class ResultadoModel
    {
        public string Id { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public DateTime FechaCalculo { get; set; } = DateTime.Now;
        public string IndicadorId { get; set; } = string.Empty;
    }
}
