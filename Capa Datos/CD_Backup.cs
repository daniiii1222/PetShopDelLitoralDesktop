using Capa_Datos;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace Capa_Datos
{
    // Acceso a datos de las copias de seguridad: lee la base (tablas, datos y procedimientos)
    // y escribe el archivo .sql. No sabe nada de pantallas ni de mensajes al usuario.
    public class CD_Backup
    {
        // Genera el script SQL completo de la base y lo guarda en rutaDestino.
        // Si algo falla, la excepción sube a la capa de negocio.
        public void GenerarRespaldo(string rutaDestino)
        {
            using (MySqlConnection conn = ClsDataBase.CrearConexion())
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"-- Backup {DateTime.Now}");

                DataTable dtTables = new DataTable();
                using (MySqlDataAdapter da = new MySqlDataAdapter("SHOW TABLES", conn))
                {
                    da.Fill(dtTables);
                }

                foreach (DataRow row in dtTables.Rows)
                {
                    string tableName = row[0].ToString();

                    DataTable dtCreate = new DataTable();
                    using (MySqlDataAdapter da = new MySqlDataAdapter($"SHOW CREATE TABLE `{tableName}`", conn))
                    {
                        da.Fill(dtCreate);
                    }

                    sb.AppendLine($"DROP TABLE IF EXISTS `{tableName}`;");
                    sb.AppendLine(dtCreate.Rows[0]["Create Table"].ToString() + ";\n");

                    DataTable dtData = new DataTable();
                    using (MySqlDataAdapter da = new MySqlDataAdapter($"SELECT * FROM `{tableName}`", conn))
                    {
                        da.Fill(dtData);
                    }

                    foreach (DataRow dataRow in dtData.Rows)
                    {
                        sb.Append($"INSERT INTO `{tableName}` VALUES (");
                        for (int i = 0; i < dtData.Columns.Count; i++)
                        {
                            sb.Append(FormatearValorSql(dataRow[i]));
                            if (i < dtData.Columns.Count - 1) sb.Append(", ");
                        }
                        sb.AppendLine(");");
                    }
                    sb.AppendLine();
                }

                // --- Backup de Stored Procedures ---
                sb.AppendLine();
                sb.AppendLine("-- ================================");
                sb.AppendLine("-- STORED PROCEDURES");
                sb.AppendLine("-- ================================");
                sb.AppendLine("DELIMITER $$");
                sb.AppendLine();

                DataTable dtProcedures = new DataTable();
                using (MySqlCommand cmdProcs = new MySqlCommand("SHOW PROCEDURE STATUS WHERE Db = @db", conn))
                {
                    cmdProcs.Parameters.AddWithValue("@db", conn.Database);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmdProcs))
                    {
                        da.Fill(dtProcedures);
                    }
                }

                foreach (DataRow procRow in dtProcedures.Rows)
                {
                    string nombreProc = procRow["Name"].ToString();

                    DataTable dtCreateProc = new DataTable();
                    using (MySqlDataAdapter da = new MySqlDataAdapter($"SHOW CREATE PROCEDURE `{nombreProc}`", conn))
                    {
                        da.Fill(dtCreateProc);
                    }

                    if (dtCreateProc.Rows.Count > 0)
                    {
                        string createStatement = dtCreateProc.Rows[0]["Create Procedure"].ToString();

                        sb.AppendLine($"DROP PROCEDURE IF EXISTS `{nombreProc}`$$");
                        sb.AppendLine(createStatement + "$$");
                        sb.AppendLine();
                    }
                }

                sb.AppendLine("DELIMITER ;");

                File.WriteAllText(rutaDestino, sb.ToString(), Encoding.UTF8);
            }

        }

        // Ejecuta en la base el script de una copia de seguridad (reemplaza los datos actuales).
        // Si algo falla, la excepción sube a la capa de negocio.
        public void RestaurarRespaldo(string scriptSql)
        {
            using (MySqlConnection conn = ClsDataBase.CrearConexion())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0;", conn)) cmd.ExecuteNonQuery();
                try
                {
                    MySqlScript script = new MySqlScript(conn, scriptSql);   // entiende el DELIMITER de los procedimientos
                    script.Execute();
                }
                finally
                {
                    using (MySqlCommand cmd = new MySqlCommand("SET FOREIGN_KEY_CHECKS=1;", conn)) cmd.ExecuteNonQuery();
                }
            }
        }

        // Convierte un valor a SQL sin depender de la configuración regional de la PC
        // (antes un decimal salía como '3200,00' y una fecha como '25/10/2026', que MySQL no entiende al restaurar).
        private static string FormatearValorSql(object val)
        {
            if (val == null || val == DBNull.Value) return "NULL";
            if (val is bool) return ((bool)val) ? "1" : "0";

            // Las columnas BIT(1) (los "estado_...") llegan como ulong. Si se escribieran entre comillas ('1'),
            // MySQL las toma como TEXTO de 8 bits y al restaurar falla con "Data too long for column".
            // Por eso van como número, sin comillas.
            if (val is ulong) return ((ulong)val).ToString(CultureInfo.InvariantCulture);
            if (val is DateTime) return "'" + ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "'";
            if (val is byte[])
            {
                byte[] bytes = (byte[])val;
                return bytes.Length == 0 ? "''" : "0x" + BitConverter.ToString(bytes).Replace("-", "");
            }
            if (val is IFormattable) return "'" + ((IFormattable)val).ToString(null, CultureInfo.InvariantCulture) + "'";

            string texto = val.ToString().Replace("\\", "\\\\").Replace("'", "\\'");
            return "'" + texto + "'";
        }
    }
}