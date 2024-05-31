using System;

namespace dominio
{
    public class Categoria
    {
        public Categoria() { }
        
        public int Id { get; set; }

        public string Nombre { get; set; }

        public byte[] Imagen { get; set; }

        public string ImagenDataUrl
        {
            get
            {
                if (Imagen != null)
                {
                    string tipoImagen = "image/jpeg"; 
                    if (Imagen.Length >= 4 && Imagen[0] == 0x3C)
                    {
                        tipoImagen = "image/svg+xml"; 
                    }
                    string base64String = Convert.ToBase64String(Imagen);
                    string dataUrl = $"data:{tipoImagen};base64,{base64String}";
                    return dataUrl;
                }
                return null;
            }
        }
        public bool Activa { get; set; }
    }
}