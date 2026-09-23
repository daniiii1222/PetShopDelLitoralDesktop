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
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            // Cargamos los datos estáticos al iniciar la pantalla de ventas
            CargarDatosEstaticos();
        }

        private void CargarDatosEstaticos()
        {
            dgvVentas.Columns.Clear();


            DataTable dtVentas = new DataTable();
            dtVentas.Columns.Add("NroVenta");
            dtVentas.Columns.Add("Cliente");
            dtVentas.Columns.Add("Fecha");
            dtVentas.Columns.Add("Total");

            dtVentas.Rows.Add("501", "Ada Lovelace", "2026-09-20", "$ 18.500");
            dtVentas.Rows.Add("502", "Virginia Romero", "2026-09-22", "$ 34.000");


            dgvVentas.DataSource = dtVentas;


            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvVentas.ReadOnly = true;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Cambiamos el azul por un tono dorado/crema que combina con tu diseño
            dgvVentas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dgvVentas.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Simulamos el registro de la venta con un cartel de éxito
            MessageBox.Show("Venta registrada con éxito (Modo Demostración).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void PanelDatosVenta_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint_2(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void PanelDerechoMID_Paint(object sender, PaintEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void panelTituloSec_Paint(object sender, PaintEventArgs e) { }
    }
}
