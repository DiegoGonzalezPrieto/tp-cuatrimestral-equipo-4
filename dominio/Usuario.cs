using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{

    public enum TipoUsuario
    {
        Usuario = 0,
        Admin = 1,
    }

    public class Usuario
    {
        public int Id { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public TipoUsuario Tipo { get; set; }

        public List<Curso> CursosCreados { get; set; }

        public List<Curso> CursosAdquiridos { get; set; }

        // fecha de alta nuevo usuario
        public DateTime FechaAlta { get; set; }

        // estado de un usario, puede ser inactivo
        public bool Estado { get; set; }

        // Datos Personales

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Profesion { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string UrlFotoPerfil { get; set; }

        public string Biografia { get; set; }

    }
}