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
    public partial class FrmCompras : Form
    {
        public FrmCompras()
        {
            InitializeComponent();
        }

        private void FrmCompras_Load(object sender, EventArgs e)
        {
            // Llamamos al método para cargar datos estáticos apenas abre la pantalla
            CargarDatosEstaticos();
        }

        private void CargarDatosEstaticos()
        {
            // 1. Borramos columnas viejas
            dgvCompras.Columns.Clear();

            // 2. Creamos la tabla
            DataTable dtCompras = new DataTable();
            dtCompras.Columns.Add("NroCompra");
            dtCompras.Columns.Add("Proveedor");
            dtCompras.Columns.Add("Fecha");
            dtCompras.Columns.Add("Total");

            dtCompras.Rows.Add("101", "Distribuidora PetFood S.A.", "2026-09-10", "$ 125.000");
            dtCompras.Rows.Add("102", "Accesorios del Litoral", "2026-09-18", "$ 48.500");

            // 3. Asignamos datos y estiramos columnas
            dgvCompras.DataSource = dtCompras;
            dgvCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 4. Bloqueamos modificaciones y personalizamos el color de selección
            dgvCompras.ReadOnly = true;
            dgvCompras.AllowUserToAddRows = false;
            dgvCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Mismo tono estético que combina con el sistema
            dgvCompras.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dgvCompras.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Simulamos el botón de guardar/registrar compra para que muestre un cartel exitoso
            MessageBox.Show("Compra registrada con éxito (Modo Demostración).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void PanelDatosCompra_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint_2(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void PanelDerechoMID_Paint(object sender, PaintEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
    }
}
