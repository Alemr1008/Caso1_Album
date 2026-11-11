namespace Abstracciones.Modelos
{
    public class ArtistaBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }

    public class ArtistaRequest : ArtistaBase
    {
    }

    public class ArtistaResponse : ArtistaBase
    {
        public Guid Id { get; set; }
    }
}
