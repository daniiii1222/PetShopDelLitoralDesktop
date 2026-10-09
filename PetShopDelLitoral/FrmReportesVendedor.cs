using Capa_Datos;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Entidad;

namespace PetShopDelLitoral
{
    public partial class FrmReportesVendedor : Form
    {
        public FrmReportesVendedor()
        {
            InitializeComponent();
            
            this.Load += new System.EventHandler(this.FrmReportesVendedor_Load);
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
        }

        private void FrmReportesVendedor_Load(object sender, EventArgs e)
        {
            CargarDatosEstaticos();
        }

        private void CargarDatosEstaticos()
        {
            dgvReportes.Columns.Clear();

            DataTable dtRepoVendedor = new DataTable();
            dtRepoVendedor.Columns.Add("Concepto");
            dtRepoVendedor.Columns.Add("Cantidad");

            dtRepoVendedor.Rows.Add("Ventas realizadas hoy", "8 tickets");
            dtRepoVendedor.Rows.Add("Comisión estimada", "$ 12.000");
            dtRepoVendedor.Rows.Add("Clientes atendidos", "5");

            dgvReportes.DataSource = dtRepoVendedor;
            dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Bloqueo y colores estéticos
            dgvReportes.ReadOnly = true;
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dgvReportes.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void panelTituloSec_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
            try
            {
                DateTime inicio = dtpDesde.Value.Date;
                DateTime fin = dtpHasta.Value.Date;

                // Mandamos el ID del vendedor logueado directamente
                int miId = Sesion.UsuarioActual.idUsuario;

                CN_Reporte cnReporte = new CN_Reporte();
                DataTable dt = cnReporte.ObtenerReporteVentas(inicio, fin, miId);

                dgvReportes.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("No tenés ventas en este rango de fechas.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void dgvReportes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si hace clic en los encabezados de las columnas, no hacemos nada
            if (e.RowIndex < 0) return;

            // Verificamos si hizo clic en la columna de Detalle
            if (dgvReportes.Columns[e.ColumnIndex].HeaderText == "Detalle" || dgvReportes.Columns[e.ColumnIndex].Name == "columnaDetalle")
            {
                try
                {
                    // Obtenemos el ID de la venta de esa fila (asegurate de que la columna se llame "Nº Venta" o "Codigo")
                    int idVenta = Convert.ToInt32(dgvReportes.Rows[e.RowIndex].Cells["Nº Venta"].Value);

                    CN_Reporte cnReporte = new CN_Reporte();
                    DataTable dtDetalle = cnReporte.ObtenerDetalleVenta(idVenta);

                    // Creamos la ventana flotante con la grilla del detalle
                    using (Form frmDetalle = new Form())
                    {
                        frmDetalle.Text = "Detalle de Venta N° " + idVenta;
                        frmDetalle.Size = new Size(600, 350);
                        frmDetalle.StartPosition = FormStartPosition.CenterParent;
                        frmDetalle.ShowIcon = false;

                        DataGridView dgvDetalle = new DataGridView
                        {
                            DataSource = dtDetalle,
                            Dock = DockStyle.Fill,
                            ReadOnly = true,
                            AllowUserToAddRows = false,
                            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                            BackgroundColor = Color.White
                        };

                        frmDetalle.Controls.Add(dgvDetalle);
                        frmDetalle.ShowDialog(this);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir el detalle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
