namespace dominio
{
    public class Contenido
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Orden { get; set; }

        public TipoContenido Tipo { get; set; }

        public string Texto { get; set; }

        public string UrlVideo { get; set; }

        // TODO : ver cómo guardar y obtener pdf de la base de datos
        public byte[] Archivo { get; set; }

        public bool Liberado { get; set; }
    }
}