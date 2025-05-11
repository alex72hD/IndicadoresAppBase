namespace IndicadoresApp.Clases
{
    public class VaribleIndicadorModel
    {
        public int Id { get; set; }
        public string FechaCreacion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string Nombre { get; set; } = string.Empty;
        public string FkEmail { get; set; } = string.Empty;
    }
}
