﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class Datos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public Datos()
        {
            if (Environment.UserName.ToLower() == "diego_prieto" || Environment.UserName.ToLower() == "lucho")
                conexion = new SqlConnection("server = localhost\\SQLEXPRESS; database = TP_Cuatrimestal_Equipo4; integrated security = true");
            else
                conexion = new SqlConnection("server =.\\DEVSERVER; database = TP_Cuatrimestal_Equipo4; integrated security = true");


            comando = new SqlCommand();

        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearProcedimiento(string sp)
        {
            comando.CommandType= System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar lectura: " + ex.Message);
            }

        }
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ejecutar acción: " + ex.Message);
            }
        }

        public int ejecturarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
            return int.Parse(comando.ExecuteScalar().ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }
    }
}
