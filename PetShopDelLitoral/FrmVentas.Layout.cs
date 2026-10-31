using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    // Layout de la pantalla de Ventas que acompaña el tamaño de la ventana.
    // Va en un archivo aparte (partial) para no mezclarlo con la lógica de FrmVentas.cs.
    public partial class FrmVentas
    {
        // ---------- Panel izquierdo (Datos de la venta) ----------
        private const int ALTURA_DISENO_IZQ = 547;   // alto de panelIzquierdoVenta en el Designer
        private const int MARGEN_IZQ = 14;           // de los campos al borde izquierdo
        private const int MARGEN_DER = 18;           // de los campos al borde derecho
        private const int X_ETIQUETAS = 11;          // las etiquetas van un poco más a la izquierda que los campos
        private const int Y_PRIMER_GRUPO = 50;       // debajo del título "Datos de la venta"
        private const int ALTO_ETIQUETA = 22;        // lo que ocupa una etiqueta hasta el campo
        private const int GAP_MIN = 26;              // aire entre un grupo (etiqueta + campo) y el siguiente
        private const int GAP_MAX = 34;
        private const int ALTO_OBS_MIN = 65;         // Observaciones: es el que absorbe la altura que sobra
        private const int ALTO_OBS_MAX = 220;
        private const int AIRE_TOTALES = 18;         // entre Observaciones y el bloque Subtotal/Total
        private const int MARGEN_INFERIOR = 10;      // el bloque de totales queda pegado abajo

        // ---------- Barra "Agregar Productos" ----------
        private const int MARGEN_BARRA = 18;         // a los costados de la barra
        private const int SEPARACION_BARRA = 24;     // entre buscador, Cantidad y Agregar

        // ---------- Botones de abajo (Cancelar y Guardar Venta) ----------
        private const int SEPARACION_BOTONES = 16;
        private const int MARGEN_BOTONES_DER = 20;   // distancia al borde derecho

        private void PrepararLayoutResponsivo()
        {
            // Lado derecho: la grilla se estira y "Vaciar Lista" queda pegado al borde de abajo
            dgvVentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2CustomGradientPanel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // Cada bloque se vuelve a acomodar cuando cambia el tamaño de su panel
            PaneContenidoSec.SizeChanged += (s, e) => AjustarAnchoPanelIzquierdo();
            panelIzquierdoVenta.SizeChanged += (s, e) => DistribuirPanelIzquierdo();
            PanelDerechoSUP.SizeChanged += (s, e) => DistribuirBarraSuperior();
            PanelDerechoBOT.SizeChanged += (s, e) => AcomodarBotonesInferiores();

            AjustarAnchoPanelIzquierdo();
            DistribuirPanelIzquierdo();
            DistribuirBarraSuperior();
            AcomodarBotonesInferiores();
        }

        // El panel de datos ocupa ~20% del ancho (entre 300 y 360 px): en pantalla grande los campos respiran más
        private void AjustarAnchoPanelIzquierdo()
        {
            int deseado = (int)Math.Round(PaneContenidoSec.ClientSize.Width * 0.20);
            panelIzquierdoVenta.Width = Math.Max(300, Math.Min(360, deseado));
        }

        // ------------------------------------------------------------------
        // PANEL IZQUIERDO
        //  - Los campos van uno debajo del otro con una separación pareja (no se reparten por todo el alto).
        //  - "Observaciones" crece para ocupar la altura que sobra.
        //  - El bloque Subtotal / Descuento / Total queda pegado abajo.
        // ------------------------------------------------------------------
        private void DistribuirPanelIzquierdo()
        {
            int alto = panelIzquierdoVenta.ClientSize.Height;
            int ancho = panelIzquierdoVenta.ClientSize.Width - MARGEN_IZQ - MARGEN_DER;   // ancho común de los campos
            if (alto < 200 || ancho < 150) return;

            // 1) Bloque de totales: mismo ancho que los campos y pegado abajo
            guna2CustomGradientPanel1.SetBounds(MARGEN_IZQ,
                alto - guna2CustomGradientPanel1.Height - MARGEN_INFERIOR,
                ancho, guna2CustomGradientPanel1.Height);

            // Los importes quedan alineados a la derecha del bloque, con el mismo margen que en el diseño
            txtSubtotal.Left = ancho - 24 - txtSubtotal.Width;
            txtDescuento.Left = ancho - 24 - txtDescuento.Width;
            guna2TextBox6.Left = ancho - 15 - guna2TextBox6.Width;

            // 2) Separación entre grupos: arranca en GAP_MIN y crece un poco si hay mucha altura
            int gap = Math.Max(GAP_MIN, Math.Min(GAP_MAX, GAP_MIN + (alto - ALTURA_DISENO_IZQ) / 40));

            int y = Y_PRIMER_GRUPO;

            // Cliente: [DNI ......(lupa)] [+]  -> el "+" termina justo en el borde derecho de los demás campos
            label1.Location = new Point(X_ETIQUETAS, y);
            int yCampo = y + ALTO_ETIQUETA;

            iconButton1.Location = new Point(MARGEN_IZQ + ancho - iconButton1.Width, yCampo);
            botonInicio.Location = new Point(iconButton1.Left - 4 - botonInicio.Width, yCampo);
            guna2TextBox1.SetBounds(MARGEN_IZQ, yCampo, botonInicio.Left + 20 - MARGEN_IZQ, guna2TextBox1.Height);   // la lupa se apoya sobre su extremo
            y = guna2TextBox1.Bottom + gap;

            // Fecha, Método de pago y Vendedor: etiqueta + campo del mismo ancho
            y = ColocarGrupo(label2, guna2DateTimePicker1, y, ancho) + gap;
            y = ColocarGrupo(label3, guna2ComboBox1, y, ancho) + gap;
            y = ColocarGrupo(label4, guna2TextBox2, y, ancho) + gap;

            // Observaciones: ocupa lo que quede hasta los totales (entre un mínimo y un máximo)
            label5.Location = new Point(X_ETIQUETAS, y);
            int yObs = y + ALTO_ETIQUETA;
            int disponible = guna2CustomGradientPanel1.Top - AIRE_TOTALES - yObs;
            int altoObs = Math.Max(ALTO_OBS_MIN, Math.Min(ALTO_OBS_MAX, disponible));
            guna2TextBox3.SetBounds(MARGEN_IZQ, yObs, ancho, altoObs);
        }

        // Pone la etiqueta en 'y' y su campo justo debajo; devuelve dónde termina el campo
        private int ColocarGrupo(Label etiqueta, Control campo, int y, int ancho)
        {
            etiqueta.Location = new Point(X_ETIQUETAS, y);
            campo.SetBounds(MARGEN_IZQ, y + ALTO_ETIQUETA, ancho, campo.Height);
            return campo.Bottom;
        }

        // ------------------------------------------------------------------
        // BARRA "AGREGAR PRODUCTOS"
        //  El buscador se estira y se lleva todo el ancho que sobra;
        //  Cantidad y Agregar quedan a la derecha, alineados con el borde de la grilla.
        // ------------------------------------------------------------------
        private void DistribuirBarraSuperior()
        {
            int anchoBarra = PanelDerechoSUP.ClientSize.Width;
            if (anchoBarra < 500) return;

            int derecha = anchoBarra - MARGEN_BARRA;

            guna2CustomGradientPanel5.Left = derecha - guna2CustomGradientPanel5.Width;                              // botón Agregar
            guna2NumericUpDown1.Left = guna2CustomGradientPanel5.Left - SEPARACION_BARRA - guna2NumericUpDown1.Width; // campo Cantidad
            label10.Left = guna2NumericUpDown1.Left - 3;                                                              // su etiqueta

            guna2TextBox7.Width = guna2NumericUpDown1.Left - SEPARACION_BARRA - guna2TextBox7.Left;                   // buscador
            iconButton4.Left = guna2TextBox7.Right - iconButton4.Width;                                               // lupa al final del buscador
        }

        // ------------------------------------------------------------------
        // BOTONES DE ABAJO: pegados a la derecha (Guardar Venta en el borde, Cancelar a su izquierda)
        // ------------------------------------------------------------------
        private void AcomodarBotonesInferiores()
        {
            int derecha = PanelDerechoBOT.ClientSize.Width - MARGEN_BOTONES_DER;

            guna2CustomGradientPanel6.Left = derecha - guna2CustomGradientPanel6.Width;
            guna2CustomGradientPanel4.Left = guna2CustomGradientPanel6.Left - SEPARACION_BOTONES - guna2CustomGradientPanel4.Width;
        }
    }
}