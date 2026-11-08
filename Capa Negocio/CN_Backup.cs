using Capa_Datos;
using System;
using System.IO;
using System.Text;

namespace Capa_Negocio
{
    // Reglas de las copias de seguridad: dónde se guardan y cómo se nombran.
    // El acceso a la base y a los archivos lo hace CD_Backup.
    public class CN_Backup
    {
        private CD_Backup objBackupDatos = new CD_Backup();

        // Carpeta donde se guardan las copias (junto al ejecutable). Se crea si no existe.
        public string ObtenerCarpetaBackups()
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            return ruta;
        }

        // Lee el archivo de una copia y controla que sea una copia válida de esta aplicación.
        // Devuelve true si se puede restaurar; si no, deja el motivo en "mensaje".
        public bool LeerRespaldo(string rutaArchivo, out string scriptSql, out string mensaje)
        {
            scriptSql = null;
            mensaje = string.Empty;

            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    mensaje = "No se encontró el archivo de la copia.";
                    return false;
                }

                string sql = File.ReadAllText(rutaArchivo, Encoding.UTF8);
                if (!sql.StartsWith("-- Backup") || sql.IndexOf("CREATE TABLE", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    mensaje = "El archivo no parece una copia de seguridad válida de esta aplicación.";
                    return false;
                }

                scriptSql = sql;
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "No se pudo leer la copia: " + ex.Message;
                return false;
            }
        }

        // Restaura la base con el script indicado. Devuelve true si salió bien; si no, el motivo queda en "mensaje".
        public bool RestaurarRespaldo(string scriptSql, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(scriptSql))
            {
                mensaje = "La copia está vacía.";
                return false;
            }

            try
            {
                objBackupDatos.RestaurarRespaldo(scriptSql);
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        // Deja constancia (restauraciones.log) de quién restauró y quién autorizó.
        // Si no se puede escribir el log, no se frena la restauración: el error se informa por Debug.
        public void RegistrarRestauracion(string archivo, string administrador, string supervisor, string copiaPrevia)
        {
            try
            {
                string linea = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                               " | archivo=" + archivo +
                               " | administrador=" + administrador +
                               " | supervisor=" + supervisor +
                               " | copia previa=" + Path.GetFileName(copiaPrevia);
                File.AppendAllText(Path.Combine(ObtenerCarpetaBackups(), "restauraciones.log"),
                                   linea + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("No se pudo escribir restauraciones.log: " + ex.Message);
            }
        }

        // Crea una copia de seguridad con el nombre petshop_db_AAAAMMDD_HHMMSS.sql.
        // Devuelve true si salió bien; si no, deja el motivo en "mensaje".
        public bool GenerarRespaldo(out string rutaArchivo, out string mensaje)
        {
            rutaArchivo = null;
            mensaje = string.Empty;

            try
            {
                string nombreArchivo = $"petshop_db_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                string ruta = Path.Combine(ObtenerCarpetaBackups(), nombreArchivo);

                objBackupDatos.GenerarRespaldo(ruta);

                rutaArchivo = ruta;
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
    }
}
