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
    public partial class FrmReportesAdministrador : Form
    {
        public FrmReportesAdministrador()
        {
            InitializeComponent();
        }

        private void panelTituloSec_Paint(object sender, PaintEventArgs e) { }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { }
        private void botonInicio_Click(object sender, EventArgs e) { }

        private void ConfigurarGrilla(string tipoReporte)
        {
            dataGridReportes.Columns.Clear();
            DataTable dt = new DataTable();

            switch (tipoReporte)
            {
                case "productos":
                    dt.Columns.Add("Codigo");
                    dt.Columns.Add("Descripcion");
                    dt.Columns.Add("Categoria");
                    dt.Columns.Add("Stock");
                    dt.Columns.Add("StockMinimo");
                    dt.Columns.Add("Precio");
                    dt.Columns.Add("Estado");

                    // Filas estáticas de prueba
                    dt.Rows.Add("P001", "Alimento Balanceado 15kg", "Alimentos", "12", "5", "$ 25.000", "Activo");
                    dt.Rows.Add("P002", "Rascador para Gatos", "Accesorios", "4", "3", "$ 14.500", "Activo");
                    dt.Rows.Add("P003", "Antipulgas Pipeta", "Farmacia", "25", "10", "$ 6.200", "Activo");
                    break;

                case "stock":
                    dt.Columns.Add("Codigo");
                    dt.Columns.Add("Descripcion");
                    dt.Columns.Add("Categoria");
                    dt.Columns.Add("Stock");
                    dt.Columns.Add("StockMinimo");

                    // Filas estáticas de prueba
                    dt.Rows.Add("P002", "Rascador para Gatos", "Accesorios", "4", "3");
                    dt.Rows.Add("P008", "Collar Antiparasitario", "Accesorios", "1", "4");
                    break;

                case "ventas":
                    dt.Columns.Add("Codigo");
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("Vendedor");
                    dt.Columns.Add("Cliente");
                    dt.Columns.Add("MetodoPago");
                    dt.Columns.Add("Total");
                    dt.Columns.Add("Detalle");

                    // Filas estáticas de prueba
                    dt.Rows.Add("V-101", "2026-09-21", "Carlos Gomez", "Ada Lovelace", "Efectivo", "$ 25.000", "Ver detalle");
                    dt.Rows.Add("V-102", "2026-09-22", "Carlos Gomez", "Virginia Romero", "Transferencia", "$ 14.500", "Ver detalle");
                    break;

                case "vendedores":
                    dt.Columns.Add("Vendedor");
                    dt.Columns.Add("CantidadVentas");
                    dt.Columns.Add("TotalVendido");

                    // Filas estáticas de prueba
                    dt.Rows.Add("Carlos Gomez", "15", "$ 245.000");
                    dt.Rows.Add("Mariana Perez", "12", "$ 198.000");
                    break;
            }


            // Asignamos la tabla armada a la grilla
            dataGridReportes.DataSource = dt;

            // Asignamos la tabla armada a la grilla
            dataGridReportes.DataSource = dt;
            dataGridReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Bloqueo y colores estéticos para el admin
            dataGridReportes.ReadOnly = true;
            dataGridReportes.AllowUserToAddRows = false;
            dataGridReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridReportes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dataGridReportes.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void botonProductos_Click(object sender, EventArgs e)
        {
            dataGridReportes.Visible = true;
            ConfigurarGrilla("productos");
        }

        private void botonStockMin_Click(object sender, EventArgs e)
        {
            dataGridReportes.Visible = true;
            ConfigurarGrilla("stock");
        }

        private void botonVentas_Click(object sender, EventArgs e)
        {
            dataGridReportes.Visible = true;
            ConfigurarGrilla("ventas");
        }

        private void botonVendedor_Click(object sender, EventArgs e)
        {
            dataGridReportes.Visible = true;
            ConfigurarGrilla("vendedores");
        }
    }
}
