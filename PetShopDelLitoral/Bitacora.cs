using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PetShopDelLitoral
{
    // Registro de errores de la aplicación: en lugar de tragarse una excepción (catch vacío),
    // las pantallas la anotan acá. Queda en la ventana de salida de Visual Studio (Debug)
    // y en el archivo Logs\errores.log junto al ejecutable.
    internal static class Bitacora
    {
        public static void Registrar(string contexto, Exception ex)
        {
            string linea = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " + contexto + " | " + ex;
            Debug.WriteLine(linea);

            try
            {
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                Directory.CreateDirectory(carpeta);
                File.AppendAllText(Path.Combine(carpeta, "errores.log"), linea + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception errorAlEscribir)
            {
                // Si ni siquiera se puede escribir el archivo, queda al menos en la ventana de salida
                Debug.WriteLine("No se pudo escribir Logs\\errores.log: " + errorAlEscribir.Message);
            }
        }
    }
}
