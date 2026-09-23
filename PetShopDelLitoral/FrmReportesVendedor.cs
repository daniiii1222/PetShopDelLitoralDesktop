using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    public partial class FrmReportesVendedor : Form
    {
        public FrmReportesVendedor()
        {
            InitializeComponent();
            
            this.Load += new System.EventHandler(this.FrmReportesVendedor_Load);
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
    }
}
