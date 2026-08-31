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
    public partial class FrmProductos : Form
    {
        public FrmProductos ()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            DataGridViewProductos.Rows.Add("001", "Producto Test", "Categoria A", "100", "10");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            // Verificamos que el usuario haya hecho clic en una fila válida (y no en el encabezado)
            if (e.RowIndex >= 0)
            {
                // Obtenemos el nombre de la columna en la que hicieron clic
                string nombreColumna = DataGridViewProductos.Columns[e.ColumnIndex].Name;

                // Si hicieron clic en la columna de Editar
                if (nombreColumna == "ColumnaEditar")
                {
                    // Aquí pones la lógica para editar
                    MessageBox.Show("Editar producto de la fila: " + e.RowIndex);

                    // Ejemplo de lo que harás después:
                    // var idProducto = guna2DataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                    // FormEditar frm = new FormEditar(Convert.ToInt32(idProducto));
                    // frm.ShowDialog();
                }

                // Si hicieron clic en la columna de Eliminar
                else if (nombreColumna == "ColumnaEliminar")
                {
                    DialogResult resultado = MessageBox.Show("¿Estás seguro de eliminar este producto?",
                                                            "Confirmar",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    {
                        // Aquí ejecutas tu borrado de la base de datos y luego remueves la fila visualmente:
                        DataGridViewProductos.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BTCargarproducto_Click(object sender, EventArgs e)
        { 
            CargaProductos frm = new CargaProductos();

            frm.Show();
        }
    }
}

