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
using CapaNegocio;

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
            // Carga de la grilla de productos / compras
            CargarDatosEstaticos();
        }

        private void CargarDatosEstaticos()
        {
            // Borramos columnas viejas si las hubiera
            dgvCompras.Columns.Clear();

            // Creamos la estructura de la tabla de detalle
            DataTable dtCompras = new DataTable();
            dtCompras.Columns.Add("Producto");
            dtCompras.Columns.Add("Precio Compra");
            dtCompras.Columns.Add("Cantidad");
            dtCompras.Columns.Add("Subtotal");

            // Datos de prueba para simular la grilla
            dtCompras.Rows.Add("Alimento perro 3kg", "$ 6.200,00", "10", "$ 62.000,00");
            dtCompras.Rows.Add("Correa nylon", "$ 1.800,00", "5", "$ 9.000,00");
            dtCompras.Rows.Add("Alimento gato 1kg", "$ 2.100,00", "20", "$ 42.000,00");

            // Asignamos datos a la grilla y la configuramos
            dgvCompras.DataSource = dtCompras;
            dgvCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCompras.ReadOnly = true;
            dgvCompras.AllowUserToAddRows = false;
            dgvCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Personalización visual
            dgvCompras.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dgvCompras.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Simulamos la registración exitosa de la compra
            MessageBox.Show("Compra registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Métodos de eventos del diseñador
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
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void PanelDerechoSUP_Paint(object sender, PaintEventArgs e) { }
    }
}