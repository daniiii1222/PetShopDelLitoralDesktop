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
using Capa_Negocio;

namespace PetShopDelLitoral
{
    public partial class FrmProductos : Form
    {
        private List<Producto> _listaProductos = new List<Producto>();

        public FrmProductos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            CargarCategoriasFiltro();
            CargarProductos();

            TxtBuscar.PlaceholderText = "Buscar por código o nombre...";
            iBSearch.Click += (s, ev) => AplicarFiltros();
            TxtBuscar.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    AplicarFiltros();
                }
            };

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
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("Cargar categor\u00edas del filtro de Productos: " + ex.Message); MessageBox.Show("No se pudieron cargar las categor\u00edas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CargarProductos()
        {
            try
            {
                _listaProductos = new CN_Producto().Listar();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Cargar productos: " + ex.Message);
                MessageBox.Show("No se pudieron cargar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            DataGridViewProductos.Rows.Clear();

            string textoBusqueda = TxtBuscar.Text.Trim().ToLower();
            string textoLimpio = textoBusqueda.TrimStart('#');
            bool esNumero = int.TryParse(textoLimpio, out int codigoBuscado);
            int idCategoriaFiltro = 0;

            if (CBFiltroCateg.SelectedValue != null && int.TryParse(CBFiltroCateg.SelectedValue.ToString(), out int idCat))
            {
                idCategoriaFiltro = idCat;
            }

            var listaFiltrada = _listaProductos.Where(p =>
            {
                bool coincideEstado = p.Estado_producto;
                bool coincideTexto = string.IsNullOrEmpty(textoBusqueda) ||
                                     (p.Nombre_producto != null && p.Nombre_producto.ToLower().Contains(textoBusqueda)) ||
                                     (!string.IsNullOrEmpty(p.Codigo_producto) && p.Codigo_producto.ToLower().Contains(textoBusqueda)) ||
                                     p.IdProducto.ToString().Contains(textoLimpio) ||
                                     (esNumero && p.IdProducto == codigoBuscado);

                bool coincideCategoria = idCategoriaFiltro == 0 ||
                                         (p.IdCategoria != null && p.IdCategoria.IdCategoria == idCategoriaFiltro);

                return coincideEstado && coincideTexto && coincideCategoria;
            });

            foreach (Producto item in listaFiltrada)
            {
                int index = DataGridViewProductos.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Codigo_producto,
                    item.Nombre_producto,
                    item.IdCategoria != null ? item.IdCategoria.Nombre_categoria : "Sin categoría",
                    item.Precio_producto,
                    item.Stock_producto
                });
                DataGridViewProductos.Rows[index].Tag = item;
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            try
            {
                string nombreColumna = DataGridViewProductos.Columns[e.ColumnIndex].Name;

                // Solo responder si se hizo clic en las columnas de acción Editar o Eliminar
                if (nombreColumna != "ColumnEditar" && nombreColumna != "ColumnEliminar")
                    return;

                // Obtener el producto de la fila (a través del Tag o por Id)
                Producto productoSeleccionado = DataGridViewProductos.Rows[e.RowIndex].Tag as Producto;

                if (productoSeleccionado == null)
                {
                    int idProducto = 0;
                    if (DataGridViewProductos.Columns.Contains("ColumnId") &&
                        DataGridViewProductos.Rows[e.RowIndex].Cells["ColumnId"].Value != null)
                    {
                        int.TryParse(DataGridViewProductos.Rows[e.RowIndex].Cells["ColumnId"].Value.ToString(), out idProducto);
                    }
                    else if (DataGridViewProductos.Rows[e.RowIndex].Cells.Count > 0 &&
                             DataGridViewProductos.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        int.TryParse(DataGridViewProductos.Rows[e.RowIndex].Cells[0].Value.ToString(), out idProducto);
                    }

                    if (idProducto > 0)
                    {
                        productoSeleccionado = _listaProductos?.FirstOrDefault(p => p.IdProducto == idProducto)
                            ?? new CN_Producto().Listar().FirstOrDefault(p => p.IdProducto == idProducto);
                    }
                }

                if (productoSeleccionado == null)
                {
                    MessageBox.Show("No se pudo obtener la información del producto seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nombreColumna == "ColumnEditar")
                {
                    CargaProductos frm = new CargaProductos(productoSeleccionado);
                    frm.ShowDialog();
                    CargarProductos();
                }
                else if (nombreColumna == "ColumnEliminar")
                {
                    string infoProd = string.IsNullOrWhiteSpace(productoSeleccionado.Codigo_producto)
                        ? productoSeleccionado.Nombre_producto
                        : $"{productoSeleccionado.Nombre_producto} (Código: {productoSeleccionado.Codigo_producto})";

                    DialogResult resultado = MessageBox.Show(
                        $"¿Estás seguro de que deseas eliminar el producto '{infoProd}'?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    {
                        string mensaje = string.Empty;
                        bool respuesta = new CN_Producto().Eliminar(productoSeleccionado.IdProducto, out mensaje);

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
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al procesar la acción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void guna2Button1_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel5_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void PanelContenedor_Paint(object sender, PaintEventArgs e) { }
    }
}
