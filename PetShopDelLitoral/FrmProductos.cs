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
    public partial class FrmProductos : Form
    {
        private List<Producto> _listaProductos = new List<Producto>();

        public FrmProductos ()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            CargarCategoriasFiltro();
            CargarProductos();

            // Tus configuraciones de diseño de la grilla que ya tenías:
            DataGridViewProductos.ReadOnly = true;
            DataGridViewProductos.AllowUserToAddRows = false;
            DataGridViewProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            DataGridViewProductos.DefaultCellStyle.SelectionForeColor = Color.Black;
            DataGridViewProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(61, 44, 34);
            DataGridViewProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            DataGridViewProductos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(61, 44, 34);
            DataGridViewProductos.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            DataGridViewProductos.EnableHeadersVisualStyles = false;
        }

        private void CargarCategoriasFiltro()
        {
            try
            {
                List<Categoria> lista = new CN_Categoria().Listar();
                List<Categoria> comboLista = new List<Categoria>();
                comboLista.Add(new Categoria() { IdCategoria = 0, Nombre_categoria = "Todas las categorías" });
                comboLista.AddRange(lista);

                CBFiltroCateg.DisplayMember = "Nombre_categoria";
                CBFiltroCateg.ValueMember = "IdCategoria";
                CBFiltroCateg.DataSource = comboLista;
                CBFiltroCateg.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
        }

        private void CargarProductos()
        {
            _listaProductos = new CN_Producto().Listar();
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            DataGridViewProductos.Rows.Clear();

            string textoBusqueda = TxtBuscar.Text.Trim().ToLower();
            int idCategoriaFiltro = 0;

            if (CBFiltroCateg.SelectedValue != null && int.TryParse(CBFiltroCateg.SelectedValue.ToString(), out int idCat))
            {
                idCategoriaFiltro = idCat;
            }

            var listaFiltrada = _listaProductos.Where(p =>
            {
                bool coincideEstado = p.Estado_producto;
                bool coincideTexto = string.IsNullOrEmpty(textoBusqueda) ||
                                     (p.Nombre_producto != null && p.Nombre_producto.ToLower().Contains(textoBusqueda));

                bool coincideCategoria = idCategoriaFiltro == 0 ||
                                         (p.IdCategoria != null && p.IdCategoria.IdCategoria == idCategoriaFiltro);

                return coincideEstado && coincideTexto && coincideCategoria;
            });

            foreach (Producto item in listaFiltrada)
            {
                DataGridViewProductos.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Nombre_producto,
                    item.IdCategoria != null ? item.IdCategoria.Nombre_categoria : "Sin categoría",
                    item.Precio_producto,
                    item.Stock_producto
                });
            }
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
            if (e.RowIndex >= 0)
            {
                string nombreColumna = DataGridViewProductos.Columns[e.ColumnIndex].Name;

                if (nombreColumna == "ColumnEditar")
                {
                    int idProducto = Convert.ToInt32(DataGridViewProductos.Rows[e.RowIndex].Cells["ColumnCodigo"].Value);
                    List<Producto> lista = new CN_Producto().Listar();
                    Producto productoSeleccionado = lista.FirstOrDefault(p => p.IdProducto == idProducto);

                    if (productoSeleccionado != null)
                    {
                        CargaProductos frm = new CargaProductos(productoSeleccionado);
                        frm.ShowDialog();
                        CargarProductos();
                    }
                }
                else if (nombreColumna == "ColumnEliminar")
                {
                    int idProducto = Convert.ToInt32(DataGridViewProductos.Rows[e.RowIndex].Cells["ColumnCodigo"].Value);
                    string nombreProducto = DataGridViewProductos.Rows[e.RowIndex].Cells["ColumnNombre"].Value.ToString();

                    DialogResult resultado = MessageBox.Show($"¿Estás seguro de eliminar el producto '{nombreProducto}'?",
                                                            "Confirmar Eliminación",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    {
                        string mensaje = string.Empty;
                        bool respuesta = new CN_Producto().Eliminar(idProducto, out mensaje);

                        if (respuesta)
                        {
                            MessageBox.Show("¡Producto eliminado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProductos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el producto: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
            frm.ShowDialog();
            CargarProductos();
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void CBFiltroCateg_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

