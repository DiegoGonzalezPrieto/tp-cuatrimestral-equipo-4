namespace dominio
{
    public class Categoria
    {
        public Categoria() { }
        public Categoria(int id, string nombre) {
            Id = id;
            Nombre = nombre;
            Activa = true;
        }
        public int Id { get; set; }

        public string Nombre { get; set; }

        public bool Activa { get; set; }
    }
}