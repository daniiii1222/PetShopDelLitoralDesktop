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
    public partial class FrmReportesSupervisor : Form
    {
        public FrmReportesSupervisor()
        {
            InitializeComponent();
        }

        private void panelTituloSec_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void botonInicio_Click(object sender, EventArgs e)
        {

        }

        private void ConfigurarGrilla(string tipoReporte)
        {
            dataGridReportes.Columns.Clear();

            switch (tipoReporte)
            {
                case "productos":

                    dataGridReportes.Columns.Add("Codigo", "Código");
                    dataGridReportes.Columns.Add("Descripcion", "Descripción");
                    dataGridReportes.Columns.Add("Categoria", "Categoría");
                    dataGridReportes.Columns.Add("Stock", "Stock Actual");
                    dataGridReportes.Columns.Add("StockMinimo", "Stock Mínimo");
                    dataGridReportes.Columns.Add("Precio", "Precio Venta");
                    dataGridReportes.Columns.Add("Estado", "Estado");

                    break;

                case "stock":

                    dataGridReportes.Columns.Add("Codigo", "Código");
                    dataGridReportes.Columns.Add("Descripcion", "Descripción");
                    dataGridReportes.Columns.Add("Categoria", "Categoría");
                    dataGridReportes.Columns.Add("Stock", "Stock Actual");
                    dataGridReportes.Columns.Add("StockMinimo", "Stock Mínimo");

                    break;

                case "ventas":

                    dataGridReportes.Columns.Add("Codigo", "Código");
                    dataGridReportes.Columns.Add("Fecha", "Fecha");
                    dataGridReportes.Columns.Add("Vendedor", "Vendedor");
                    dataGridReportes.Columns.Add("Cliente", "Cliente");
                    dataGridReportes.Columns.Add("MetodoPago", "Método de pago");
                    dataGridReportes.Columns.Add("Total", "Total");
                    dataGridReportes.Columns.Add("Detalle", "Detalle");

                    break;

                case "vendedores":

                    dataGridReportes.Columns.Add("Vendedor", "Vendedor");
                    dataGridReportes.Columns.Add("CantidadVentas", "Cantidad de ventas");
                    dataGridReportes.Columns.Add("TotalVendido", "Total vendido");

                    break;
            }
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
