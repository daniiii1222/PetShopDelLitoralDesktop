using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    // Reglas de layout que comparten las pantallas con "panel de datos a la izquierda + detalle a la derecha"
    // (Ventas y Compras). Así el ancho del panel, los márgenes y la forma de apilar "etiqueta + campo"
    // se definen en un solo lugar y las pantallas se ven iguales.
    internal static class LayoutPantalla
    {
        // Ancho del panel izquierdo: un porcentaje del ancho disponible, entre un mínimo y un máximo
        public const double PORCENTAJE_ANCHO = 0.20;
        public const int ANCHO_PANEL_MIN = 300;
        public const int ANCHO_PANEL_MAX = 360;

        // Márgenes y medidas comunes dentro del panel izquierdo
        public const int MARGEN_IZQ = 14;      // de los campos al borde izquierdo
        public const int MARGEN_DER = 18;      // de los campos al borde derecho
        public const int X_ETIQUETAS = 11;     // las etiquetas van un poco más a la izquierda que los campos
        public const int ALTO_ETIQUETA = 22;   // lo que ocupa una etiqueta hasta su campo

        // Calcula el ancho del panel izquierdo según el ancho del contenedor
        public static int AnchoPanelIzquierdo(int anchoContenedor)
        {
            int deseado = (int)Math.Round(anchoContenedor * PORCENTAJE_ANCHO);
            return Math.Max(ANCHO_PANEL_MIN, Math.Min(ANCHO_PANEL_MAX, deseado));
        }

        // Pone la etiqueta en 'y' y su campo justo debajo (todo el ancho); devuelve dónde termina el campo
        public static int ColocarGrupo(Label etiqueta, Control campo, int y, int ancho)
        {
            etiqueta.Location = new Point(X_ETIQUETAS, y);
            campo.SetBounds(MARGEN_IZQ, y + ALTO_ETIQUETA, ancho, campo.Height);
            return campo.Bottom;
        }
    }
}
