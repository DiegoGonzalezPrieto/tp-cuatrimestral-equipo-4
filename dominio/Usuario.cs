using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Correo { get; set; }

        public string Nombre { get; set; }

        public TipoUsuario Tipo { get; set; }

        public List<Curso> CursosCreados { get; set; }
        
        public List<Curso> CursosAdquiridos { get; set; }


    }

    public enum TipoUsuario
    {
        Usuario = 0,
        Admin = 1,
    }
}