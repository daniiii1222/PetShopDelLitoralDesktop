using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    // Layout del panel izquierdo de Compras (mismo criterio que FrmVentas.Layout.cs).
    // Va en un archivo aparte (partial) para no mezclarlo con la lógica de FrmCompras.cs.
    public partial class FrmCompras
    {
        private const int MARGEN_IZQ = LayoutPantalla.MARGEN_IZQ;
        private const int MARGEN_DER = LayoutPantalla.MARGEN_DER;
        private const int X_ETIQUETAS = LayoutPantalla.X_ETIQUETAS;
        private const int Y_PRIMER_GRUPO = 50;        // debajo del título "Datos de la Compra"
        private const int ALTO_ETIQUETA = LayoutPantalla.ALTO_ETIQUETA;
        private const int GAP_MIN = 12;               // aire entre un grupo (etiqueta + campo) y el siguiente
        private const int GAP_MAX = 24;
        private const int ALTO_OBS_MIN = 65;          // Observaciones: absorbe toda la altura que sobra
        private const int AIRE_TOTALES = 14;          // entre Observaciones y el bloque Subtotal/Total
        private const int MARGEN_INFERIOR = 10;       // el bloque de totales queda pegado abajo

        private void PrepararLayoutPanelIzquierdo()
        {
            PaneContenidoSec.SizeChanged += (s, e) => AjustarAnchoPanelIzquierdo();
            panelIzquierdoVenta.SizeChanged += (s, e) => DistribuirPanelIzquierdo();

            AjustarAnchoPanelIzquierdo();
            DistribuirPanelIzquierdo();
        }

        // El ancho del panel de datos lo define LayoutPantalla (igual que en Ventas)
        private void AjustarAnchoPanelIzquierdo()
        {
            panelIzquierdoVenta.Width = LayoutPantalla.AnchoPanelIzquierdo(PaneContenidoSec.ClientSize.Width);
        }

        // Todos los campos ocupan el ancho del panel; "Observaciones" crece hasta los totales,
        // que quedan pegados abajo. Así el panel se aprovecha completo.
        private void DistribuirPanelIzquierdo()
        {
            int alto = panelIzquierdoVenta.ClientSize.Height;
            int ancho = panelIzquierdoVenta.ClientSize.Width - MARGEN_IZQ - MARGEN_DER;
            if (alto < 200 || ancho < 150) return;

            // 1) Bloque de totales: mismo ancho que los campos y pegado abajo
            guna2CustomGradientPanel1.SetBounds(MARGEN_IZQ,
                alto - guna2CustomGradientPanel1.Height - MARGEN_INFERIOR,
                ancho, guna2CustomGradientPanel1.Height);

            // Importes alineados a la derecha del bloque
            guna2TextBox5.Left = ancho - 13 - guna2TextBox5.Width;   // Subtotal
            guna2TextBox4.Left = ancho - 13 - guna2TextBox4.Width;   // Descuento
            guna2TextBox6.Left = ancho - 11 - guna2TextBox6.Width;   // Total

            // 2) Reparto vertical: los 5 grupos fijos + Observaciones (que absorbe el resto)
            int altoCampo = guna2ComboBox1.Height;
            int altoGrupo = ALTO_ETIQUETA + altoCampo;
            int altoObsLabel = ALTO_ETIQUETA;
            int libre = guna2CustomGradientPanel1.Top - AIRE_TOTALES - Y_PRIMER_GRUPO
                        - 5 * altoGrupo - altoObsLabel - ALTO_OBS_MIN;

            // Si sobra altura, los grupos se separan un poco más (hasta GAP_MAX); el resto se lo lleva Observaciones
            int gap = Math.Max(GAP_MIN, Math.Min(GAP_MAX, libre / 5));

            int y = Y_PRIMER_GRUPO;
            y = ColocarGrupo(label1, guna2ComboBox1, y, ancho) + gap;          // Proveedor
            y = ColocarGrupo(label2, guna2DateTimePicker1, y, ancho) + gap;    // Fecha
            y = ColocarGrupo(label12, comboBox1, y, ancho) + gap;              // Tipo de comprobante
            y = ColocarGrupo(label13, textBox1, y, ancho) + gap;               // N° de comprobante
            y = ColocarGrupo(label4, guna2TextBox2, y, ancho) + gap;           // Encargado

            // Observaciones: ocupa todo lo que queda hasta los totales
            label5.Location = new Point(X_ETIQUETAS, y);
            int yObs = y + ALTO_ETIQUETA;
            int altoObs = Math.Max(ALTO_OBS_MIN, guna2CustomGradientPanel1.Top - AIRE_TOTALES - yObs);
            guna2TextBox3.SetBounds(MARGEN_IZQ, yObs, ancho, altoObs);
        }

        private int ColocarGrupo(Label etiqueta, Control campo, int y, int ancho)
        {
            return LayoutPantalla.ColocarGrupo(etiqueta, campo, y, ancho);
        }

        // ------------------------------------------------------------------
        // BARRA "AGREGAR PRODUCTOS" Y BOTONES DE ABAJO (lado derecho)
        // ------------------------------------------------------------------
        private void AlinearControlesCompras()
        {
            try
            {
                if (panelTituloSec != null) panelTituloSec.Visible = false;
                if (PaneContenidoSec != null)
                {
                    PaneContenidoSec.AutoSize = false;
                    PaneContenidoSec.Dock = DockStyle.Fill;
                }

                // 1. Alinear y expandir controles de "Agregar Productos" para que alcancen ambos bordes
                if (PanelDerechoSUP != null)
                {
                    int supWidth = PanelDerechoSUP.ClientSize.Width;

                    // Botón + Agregar alineado a la derecha (exacto margen de 14px como la grilla)
                    if (guna2CustomGradientPanel5 != null)
                    {
                        int btnAgregarX = supWidth - 14 - guna2CustomGradientPanel5.Width;
                        guna2CustomGradientPanel5.Location = new Point(btnAgregarX, 34);

                        // Cantidad (16px a la izquierda de Agregar)
                        if (guna2NumericUpDown1 != null)
                        {
                            int cantX = btnAgregarX - 16 - guna2NumericUpDown1.Width;
                            guna2NumericUpDown1.Location = new Point(cantX, 34);
                            if (label10 != null) label10.Location = new Point(cantX, 12);

                            // Precio Compra (16px a la izquierda de Cantidad)
                            if (textBox2 != null)
                            {
                                int precioX = cantX - 16 - textBox2.Width;
                                textBox2.Location = new Point(precioX, 34);
                                if (label14 != null) label14.Location = new Point(precioX, 12);

                                // Botón Lupa (16px a la izquierda de Precio)
                                if (iconButton4 != null)
                                {
                                    int lupaX = precioX - 16 - iconButton4.Width;
                                    iconButton4.Location = new Point(lupaX, 34);

                                    // Buscador de productos: se expande desde X=14 hasta la Lupa
                                    if (guna2TextBox7 != null)
                                    {
                                        int boxX = 14;
                                        int boxW = Math.Max(180, (lupaX - 4) - boxX);
                                        guna2TextBox7.Location = new Point(boxX, 34);
                                        guna2TextBox7.Size = new Size(boxW, 36);
                                    }
                                }
                            }
                        }
                    }
                }

                // 2. Alinear barra de botones inferiores en PanelDerechoBOT
                if (PanelDerechoBOT != null)
                {
                    int botWidth = PanelDerechoBOT.ClientSize.Width;
                    int botHeight = PanelDerechoBOT.ClientSize.Height;
                    int yPos = Math.Max(0, (botHeight - 40) / 2);

                    // Vaciar Lista alineado a 14px a la izquierda (exacto margen de la grilla)
                    if (guna2CustomGradientPanel3 != null)
                    {
                        guna2CustomGradientPanel3.Location = new Point(14, yPos);
                    }

                    // Guardar Compra alineado a 14px a la derecha (exacto margen de la grilla)
                    if (guna2CustomGradientPanel6 != null)
                    {
                        int btnGuardarX = botWidth - 14 - guna2CustomGradientPanel6.Width;
                        guna2CustomGradientPanel6.Location = new Point(btnGuardarX, yPos);

                        // Cancelar separado 12px a la izquierda de Guardar Compra
                        if (guna2CustomGradientPanel4 != null)
                        {
                            int btnCancelarX = btnGuardarX - 12 - guna2CustomGradientPanel4.Width;
                            guna2CustomGradientPanel4.Location = new Point(btnCancelarX, yPos);
                        }
                    }
                }
            }
            catch (Exception ex) { Bitacora.Registrar("Acomodar el layout de Compras", ex); }
        }
    }
}
