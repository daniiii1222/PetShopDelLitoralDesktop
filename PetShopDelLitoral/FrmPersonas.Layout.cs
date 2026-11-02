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
            guna2CustomGradientPanel8.Location = new Point(xCampo, y);                                        // Guardar
            guna2CustomGradientPanel6.Location = new Point(xCampo + ancho - guna2CustomGradientPanel6.Width, y); // Cancelar
            int xLimpiar = (guna2CustomGradientPanel8.Right + guna2CustomGradientPanel6.Left - guna2CustomGradientPanel4.Width) / 2;
            guna2CustomGradientPanel4.Location = new Point(xLimpiar, y);                                       // Limpiar
        }
    }
}
