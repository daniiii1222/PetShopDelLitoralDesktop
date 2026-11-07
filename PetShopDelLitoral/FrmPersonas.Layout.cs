using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    // Layout del formulario de ingreso de datos (panel izquierdo) de Personas.
    // Va en un archivo aparte (partial), igual que FrmVentas.Layout.cs y FrmCompras.Layout.cs.
    public partial class FrmPersonas
    {
        private const int GAP_MIN_FORM = 10;           // aire mínimo entre un bloque y el siguiente
        private const int GAP_MAX_FORM = 32;           // si sobra altura, se separan hasta este valor
        private const int MARGEN_INFERIOR_FORM = 14;   // aire que queda debajo de los botones

        // Filtros de la derecha (Todos / Clientes / Proveedores / Usuarios):
        // misma altura, mismo ancho, en la misma fila y con la misma separación.
        // Filtros de la derecha (Clientes / Proveedores / Usuarios):
        // misma altura, mismo ancho, en la misma fila y con la misma separación.
        private void AcomodarFiltros()
        {
            Control[] paneles = { guna2CustomGradientPanel12, guna2CustomGradientPanel11, guna2CustomGradientPanel10 };
            Control[] botones = { btnTClientes, btnTProveedores, btnTUsuarios };

            int x = guna2CustomGradientPanel12.Left;
            int y = guna2CustomGradientPanel12.Top;
            const int ANCHO_FILTRO = 145;
            const int SEPARACION_FILTRO = 14;

            for (int i = 0; i < paneles.Length; i++)
            {
                paneles[i].Visible = botones[i].Visible;
                if (!paneles[i].Visible) continue;

                paneles[i].SetBounds(x, y, ANCHO_FILTRO, 36);
                botones[i].SetBounds(0, 0, ANCHO_FILTRO, 36);
                x += ANCHO_FILTRO + SEPARACION_FILTRO;
            }
        }

        // Reparte los botones visibles en una fila de ancho 'anchoTotal', todos iguales.
        // Cada botón ocupa todo su panel redondeado; la fuente y el ícono se achican para que entren.
        private void RepartirBotones(Control[] paneles, Control[] botones, int x, int y, int anchoTotal, int alto, float fuente, int icono)
        {
            const int SEPARACION = 8;

            int cantidad = 0;
            for (int i = 0; i < botones.Length; i++)
                if (botones[i].Visible) cantidad++;
            if (cantidad == 0) return;

            // Si queda un solo botón visible (por ejemplo, Cliente para el rol Vendedor),
            // no debe estirarse a todo el ancho del formulario: se centra y conserva un ancho compacto.
            int ancho = cantidad == 1
                ? Math.Min(150, anchoTotal)
                : (anchoTotal - (cantidad - 1) * SEPARACION) / cantidad;

            if (cantidad == 1)
                x += (anchoTotal - ancho) / 2;

            for (int i = 0; i < paneles.Length; i++)
            {
                paneles[i].Visible = botones[i].Visible;
                if (!paneles[i].Visible) continue;

                paneles[i].SetBounds(x, y, ancho, alto);
                botones[i].SetBounds(0, 0, ancho, alto);
                botones[i].Font = new Font(botones[i].Font.FontFamily, fuente, botones[i].Font.Style);
                FontAwesome.Sharp.IconButton ib = botones[i] as FontAwesome.Sharp.IconButton;
                if (ib != null)
                {
                    ib.IconSize = icono;

                    // Centra el texto respecto del botón.
                    ib.TextAlign = ContentAlignment.MiddleCenter;

                    // Ubica el ícono a la izquierda.
                    ib.ImageAlign = ContentAlignment.MiddleLeft;

                    // Superpone el texto para que no se desplace por el espacio del ícono.
                    ib.TextImageRelation = TextImageRelation.Overlay;

                    ib.Text = ib.Text.Trim();
                }

                x += ancho + SEPARACION;
            }
        }

        // Se llama una sola vez desde el constructor
        private void PrepararLayoutFormulario()
        {
            // El formulario se reacomoda al mostrarse, al cambiar el alto del panel
            // y cuando aparece/desaparece el bloque de Rol y Contraseña
            panelUsuarios.VisibleChanged += (s, e) => DistribuirFormulario();
            PanelUsuario.SizeChanged += (s, e) => DistribuirFormulario();
            this.Shown += (s, e) => DistribuirFormulario();
        }

        // ------------------------------------------------------------------
        // Un campo debajo del otro, a todo el ancho del panel:
        //   Dni, Nombre, Apellido, Correo, Teléfono, Dirección, Estado,
        //   (bloque Rol + Contraseña cuando se muestra) y los botones Guardar / Limpiar / Cancelar.
        // La separación entre bloques crece un poco si hay altura de sobra.
        // Todas las medidas salen de los controles del Designer, así valen con cualquier escala de pantalla.
        // ------------------------------------------------------------------
        private void DistribuirFormulario()
        {
            AcomodarFiltros();   // botones de filtro de la derecha: misma fila, mismo tamaño

            int anchoPanel = PanelUsuario.ClientSize.Width;
            int altoPanel = PanelUsuario.ClientSize.Height;
            if (anchoPanel < 200 || altoPanel < 200) return;

            int xCampo = TBDni.Left;                 // los campos arrancan donde arrancaba el primero
            int xEtiqueta = label11.Left;
            int ancho = anchoPanel - 2 * xCampo;
            int alto = TBDni.Height;
            int offsetEtiqueta = label11.Height + 3;  // de la etiqueta a su campo
            int altoGrupo = offsetEtiqueta + alto;
            int y0 = label11.Top;                     // debajo de "Tipo de persona"

            Label[] etiquetas = { label11, label3, label4, label6, label5, label7 };
            Control[] campos = { TBDni, TBNombre, TBApellido, TBCorreo, TBTelefono, TBDireccion };

            // Medidas del bloque Rol + Contraseña (si se está mostrando)
            bool conRol = panelUsuarios.Visible;
            int pad = xCampo;
            int altoRol = 0;
            if (conRol)
            {
                altoRol = pad / 2 + lbPanelUsuarios.Height + pad / 2
                          + offsetEtiqueta + CBRol.Height + pad
                          + offsetEtiqueta + TBContraseniaUsuario.Height + pad;
            }

            bool conEstado = chkActivo.Visible;
            int altoEstado = conEstado ? chkActivo.Height : 0;
            int altoBoton = guna2CustomGradientPanel8.Height;

            // Reparto de la altura: bloques fijos + separaciones iguales entre ellos
            int bloques = 6 + (conEstado ? 1 : 0) + (conRol ? 1 : 0) + 1;   // campos, estado, rol (opcional), botones
            int huecos = bloques - 1;
            int fijo = 6 * altoGrupo + altoEstado + altoRol + altoBoton;
            int libre = altoPanel - y0 - MARGEN_INFERIOR_FORM - fijo;
            int gap = Math.Max(GAP_MIN_FORM, Math.Min(GAP_MAX_FORM, libre / huecos));

            int y = y0;

            // 1) Los seis campos, uno debajo del otro
            for (int i = 0; i < etiquetas.Length; i++)
            {
                etiquetas[i].Location = new Point(xEtiqueta, y);
                campos[i].SetBounds(xCampo, y + offsetEtiqueta, ancho, alto);
                y += altoGrupo + gap;
            }

            // 2) Estado: etiqueta y casilla en la misma fila
            // Después
            if (conEstado)
            {
                label12.Location = new Point(xEtiqueta, y + (altoEstado - label12.Height) / 2);
                chkActivo.Location = new Point(label12.Right + 8, y);
                y += altoEstado + gap;
            }

            // 3) Rol y Contraseña (solo si el bloque está visible), también uno debajo del otro
            if (conRol)
            {
                panelUsuarios.SetBounds(xEtiqueta, y, anchoPanel - 2 * xEtiqueta, altoRol);
                int anchoInterno = panelUsuarios.Width - 2 * pad;

                int yi = pad / 2;
                lbPanelUsuarios.Location = new Point(pad, yi);
                yi += lbPanelUsuarios.Height + pad / 2;

                label8.Location = new Point(pad, yi);
                CBRol.SetBounds(pad, yi + offsetEtiqueta, anchoInterno, CBRol.Height);
                yi = CBRol.Bottom + pad;

                label9.Location = new Point(pad, yi);
                TBContraseniaUsuario.SetBounds(pad, yi + offsetEtiqueta, anchoInterno, TBContraseniaUsuario.Height);

                y += altoRol + gap;
            }

            // 4) Botones: Guardar a la izquierda, Cancelar a la derecha y Limpiar en el medio
            label2.Left = xEtiqueta;   // el título "Tipo de persona" alineado con las demás etiquetas

            // "Tipo de persona" y Guardar / Limpiar / Cancelar: botones del mismo ancho y alto,
            // repartidos de borde a borde de los campos (así ningún texto queda cortado)
            Control[] panelesTipo = { guna2CustomGradientPanel5, guna2CustomGradientPanel2, guna2CustomGradientPanel3 };
            Control[] botonesTipo = { btnCliente, btntipoProveedor, btnUsuario };
            RepartirBotones(panelesTipo, botonesTipo, xCampo, guna2CustomGradientPanel5.Top, ancho, 36, 8.5F, 18);

            Control[] panelesAccion = { guna2CustomGradientPanel8, guna2CustomGradientPanel4, guna2CustomGradientPanel6 };
            Control[] botonesAccion = { botonGuardarRegistro, btnLimpiarCampos, btnCancelar };
            RepartirBotones(panelesAccion, botonesAccion, xCampo, y, ancho, 36, 9F, 20);                                        // Guardar
            // (la posición de Cancelar la resuelve RepartirBotones) // Cancelar
            
                                                   // Limpiar
        }
    }
}
