namespace Abstracciones.Modelos
{
    public class ArtistaBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;
        public int AnioFormacion { get; set; }
    }

    public class ArtistaRequest : ArtistaBase { }

    public class ArtistaResponse : ArtistaBase
    {
        public Guid Id { get; set; }
    }
}
