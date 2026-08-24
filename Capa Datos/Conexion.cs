using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Capa_Datos
{
    internal class Conexion
    {
        private static string cadena = "Server=localhost;Database=petshopdellitoral;Uid=root;Pwd=;";

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadena);
        }

    }
}
