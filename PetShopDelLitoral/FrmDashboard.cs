using Capa_Entidad;
using Capa_Negocio;
using CapaDatos;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    public partial class FrmDashboard : Form
    {
        private static readonly CultureInfo culturaAR = new CultureInfo("es-AR");

        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            CargarDatosDashboard();   // tarjetas y actividad reciente con datos reales
            panel1_Resize(null, EventArgs.Empty);   // re-centra con los números ya cargados
            panel2_Resize(null, EventArgs.Empty);
        }

        // ==========================================
        // DATOS REALES DEL DASHBOARD
        // ==========================================

        private void CargarDatosDashboard()
        {
            try
            {
                CN_Venta cnVenta = new CN_Venta();

                // Tarjetas: ventas de hoy y clientes
                DataTable resumen = cnVenta.ObtenerResumenDashboard();
                if (resumen.Rows.Count > 0)
                {
                    lblVentasHoy.Text = Convert.ToInt32(resumen.Rows[0]["ventas_hoy"]).ToString();
                    label5.Text = Convert.ToInt32(resumen.Rows[0]["clientes"]).ToString();
                }

                // Actividad reciente: una fila de la grilla por venta
                DataTable recientes = cnVenta.ObtenerVentasRecientes();

                dgvVentasRecientes.Rows.Clear();
                foreach (DataRow fila in recientes.Rows)
                {
                    dgvVentasRecientes.Rows.Add(
                        "#" + fila["idVenta"],
                        Convert.ToDateTime(fila["fecha_venta"]).ToString("dd/MM/yyyy"),
                        Convert.ToString(fila["cliente"]),
                        "$ " + Convert.ToDecimal(fila["total_venta"]).ToString("N2", culturaAR));
                }

                // Sin fila seleccionada al abrir (si no, la primera queda pintada de dorado)
                dgvVentasRecientes.ClearSelection();
                dgvVentasRecientes.CurrentCell = null;

                // El mensaje "No hay ventas" solo aparece cuando la lista viene vacía
                label8.Visible = recientes.Rows.Count == 0;
                label8.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el dashboard: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // EVENTOS DE LOS BOTONES DE ACCIONES RÁPIDAS (tarjeta oculta)
        // ==========================================

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                inicio.NavegarAFormulario(new FrmVentas());
            }
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                inicio.NavegarAFormulario(new FrmProductos());
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                string rol = (Sesion.UsuarioActual != null && Sesion.UsuarioActual.idRol != null)
                    ? Sesion.UsuarioActual.idRol.descripcion_rol.Trim()
                    : "";

                inicio.NavegarAFormulario(new FrmPersonas(rol));
            }
        }

        private void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                inicio.NavegarAFormulario(new FrmCompras());
            }
        }

        // ==========================================
        // Las tarjetas y la actividad se acomodan solas con el TableLayoutPanel del Designer.
        // Acá solo se centra el contenido (icono, título y número) de cada tarjeta de resumen.
        // ==========================================

        private void panel1_Resize(object sender, EventArgs e)
        {
            CentrarTarjeta(panel1, iconPictureBox2, label1, lblVentasHoy);
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            CentrarTarjeta(panel2, iconPictureBox1, label2, label5);
        }

        private void CentrarTarjeta(Control tarjeta, Control icono, Label titulo, Label valor)
        {
            icono.Location = new Point(40, (tarjeta.Height - icono.Height) / 2);

            int zonaIzq = 40 + icono.Width + 10;
            int zonaAncho = tarjeta.Width - zonaIzq - 30;

            // Título + número como un solo bloque, centrado en vertical según el alto real de la tarjeta
            int separacion = 4;
            int yTop = Math.Max(4, (tarjeta.Height - (titulo.Height + separacion + valor.Height)) / 2);

            titulo.Location = new Point(zonaIzq + (zonaAncho - titulo.Width) / 2, yTop);
            valor.Location = new Point(zonaIzq + (zonaAncho - valor.Width) / 2, yTop + titulo.Height + separacion);
        }
    }
}