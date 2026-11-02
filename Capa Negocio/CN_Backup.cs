using Capa_Datos;
using System;
using System.IO;

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
