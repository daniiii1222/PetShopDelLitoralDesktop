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
            CargarReporte();
        }


        private void CargarReporte()
        {
            try
            {
                DateTime inicio = dtpDesde.Value.Date;
                DateTime fin = dtpHasta.Value.Date;

                int miId = Sesion.UsuarioActual.idUsuario;

                CN_Reporte cnReporte = new CN_Reporte();

                DataTable dt = cnReporte.ObtenerReporteVentas(
                    inicio,
                    fin,
                    miId
                );

                // Eliminamos las columnas creadas desde el Designer.
                dgvReportes.Columns.Clear();

                // Permitimos que las columnas se generen desde el DataTable.
                dgvReportes.AutoGenerateColumns = true;

                dgvReportes.DataSource = dt;

                dgvReportes.ReadOnly = true;
                dgvReportes.AllowUserToAddRows = false;
                dgvReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Mejoramos la legibilidad.
                dgvReportes.DefaultCellStyle.ForeColor = Color.Black;
                dgvReportes.DefaultCellStyle.SelectionBackColor =
                    Color.FromArgb(225, 185, 110);
                dgvReportes.DefaultCellStyle.SelectionForeColor = Color.Black;

                dgvReportes.RowsDefaultCellStyle.ForeColor = Color.Black;
                dgvReportes.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

                // Agregamos el botón Ver Detalle.
                DataGridViewButtonColumn columnaDetalle = new DataGridViewButtonColumn
                {
                    Name = "columnaDetalle",
                    HeaderText = "Detalle",
                    Text = "Ver Detalle",
                    UseColumnTextForButtonValue = true,
                    ReadOnly = true
                };

                dgvReportes.Columns.Add(columnaDetalle);

                // Agregamos la columna de comprobante.
                ComprobanteVenta.AsegurarColumna(dgvReportes);

                //correccion visual
                foreach (DataGridViewColumn columna in dgvReportes.Columns)
                {
                    if (columna is DataGridViewButtonColumn boton)
                    {
                        boton.FlatStyle = FlatStyle.Flat;

                        boton.DefaultCellStyle.SelectionBackColor =
                            Color.FromArgb(225, 185, 110);

                        boton.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el reporte: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarReporte();
        }
        
        

        private void dgvReportes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si hace clic en los encabezados de las columnas, no hacemos nada
            if (e.RowIndex < 0) return;

            if (ComprobanteVenta.EsColumnaComprobante(dgvReportes, e.ColumnIndex))
            {
                ComprobanteVenta.Generar(this, dgvReportes, e.RowIndex);
                return;
            }
            // Verificamos si hizo clic en la columna de Detalle
            if (dgvReportes.Columns[e.ColumnIndex].HeaderText == "Detalle" || dgvReportes.Columns[e.ColumnIndex].Name == "columnaDetalle")
            {
                try
                {
                    // Obtenemos el ID de la venta de esa fila (asegurate de que la columna se llame "Nº Venta" o "Codigo")
                    int idVenta = Convert.ToInt32(dgvReportes.Rows[e.RowIndex].Cells["N° Venta"].Value);

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
