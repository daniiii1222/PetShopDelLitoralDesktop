using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    // Layout responsivo de las barras de filtros de Reportes (mismo criterio que FrmVentas.Layout.cs y FrmCompras.Layout.cs).
    // Evita que los selectores de fecha se pisen con las etiquetas "Desde" y "Hasta",
    // y mantiene la barra de filtros y el buscador alineados con cualquier tamaño de pantalla.
    public partial class FrmReportesAdministrador
    {
        private void PrepararLayout()
        {
            this.Shown += (s, e) => DistribuirBarras();
            this.Resize += (s, e) => DistribuirBarras();
            guna2CustomGradientPanel2.SizeChanged += (s, e) => DistribuirBarras();
            guna2CustomGradientPanel1.SizeChanged += (s, e) => DistribuirBarras();

            DistribuirBarras();
        }

        private void DistribuirBarras()
        {
            DistribuirBarraSuperior();
            DistribuirBarraInferior();
        }

        // ------------------------------------------------------------------
        // BARRA SUPERIOR (Productos / Stock Mínimo / Ventas + Buscador de Vendedor)
        // ------------------------------------------------------------------
        private void DistribuirBarraSuperior()
        {
            int anchoBarra = guna2CustomGradientPanel2.ClientSize.Width;
            int altoBarra = guna2CustomGradientPanel2.ClientSize.Height;
            if (anchoBarra < 300 || altoBarra < 30) return;

            int yBoton = (altoBarra - 38) / 2;

            // 1) Botones de filtro a la izquierda
            int x = 20;
            guna2CustomGradientPanel3.SetBounds(x, yBoton, 135, 38); // Productos
            botonProductos.SetBounds(0, 0, 135, 38);
            x = guna2CustomGradientPanel3.Right + 12;

            guna2CustomGradientPanel4.SetBounds(x, yBoton, 155, 38); // Stock Minimo
            botonStockMin.SetBounds(0, 0, 155, 38);
            x = guna2CustomGradientPanel4.Right + 12;

            guna2CustomGradientPanel6.SetBounds(x, yBoton, 125, 38); // Ventas
            botonVentas.SetBounds(0, 0, 125, 38);

            // 2) Buscador y lupa pegados a la derecha
            const int MARGEN_DER = 20;
            botonInicio.SetBounds(anchoBarra - MARGEN_DER - 38, yBoton, 38, 38); // Botón lupa

            int espacioDisponible = botonInicio.Left - 10 - (guna2CustomGradientPanel6.Right + 20);
            int anchoBuscador = Math.Max(200, Math.Min(310, espacioDisponible));
            txtBuscarVendedor.SetBounds(botonInicio.Left - 6 - anchoBuscador, yBoton, anchoBuscador, 38);
        }

        // ------------------------------------------------------------------
        // BARRA INFERIOR (Desde [fecha]  Hasta [fecha]  +  Botón Buscar)
        // ------------------------------------------------------------------
        private void DistribuirBarraInferior()
        {
            int anchoBarra = guna2CustomGradientPanel1.ClientSize.Width;
            int altoBarra = guna2CustomGradientPanel1.ClientSize.Height;
            if (anchoBarra < 300 || altoBarra < 30) return;

            int yCampo = (altoBarra - 38) / 2;
            const int ANCHO_FECHA = 175;

            // 1) "Desde" y su selector de fecha (con aire suficiente para que no se corte la palabra)
            int yLabel1 = (altoBarra - label1.Height) / 2;
            label1.Location = new Point(20, yLabel1);
            dtpDesde.SetBounds(label1.Right + 12, yCampo, ANCHO_FECHA, 38);

            // 2) "Hasta" y su selector de fecha
            int yLabel2 = (altoBarra - label2.Height) / 2;
            label2.Location = new Point(dtpDesde.Right + 28, yLabel2);
            dtpHasta.SetBounds(label2.Right + 12, yCampo, ANCHO_FECHA, 38);

            // 3) Botón Buscar pegado a la derecha, alineado con el borde de la lupa de la barra superior
            const int MARGEN_DER = 20;
            const int ANCHO_BUSCAR = 145;
            guna2CustomGradientPanel5.SetBounds(anchoBarra - MARGEN_DER - ANCHO_BUSCAR, yCampo, ANCHO_BUSCAR, 38);
            btnBuscar.SetBounds(0, 0, ANCHO_BUSCAR, 38);
        }
    }
}
