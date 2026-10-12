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
using CapaNegocio;

namespace PetShopDelLitoral
{
    public partial class CargaProductos : Form
    {
        private Producto _productoEditar = null;

        public CargaProductos(Producto objProducto = null)
        {
            InitializeComponent();
            _productoEditar = objProducto;
        }

        private void ABMProductos_Load(object sender, EventArgs e)
        {
            CargarCategorias();

            if (_productoEditar != null)
            {
                LBABMProductos.Text = "Modificar Producto";
                txtNombre.Text = _productoEditar.Nombre_producto;
                txtStock.Text = _productoEditar.PrecioUnitario_producto.ToString();
                guna2TextBox1.Text = _productoEditar.Precio_producto.ToString();
                txtPrecio.Text = _productoEditar.Stock_producto.ToString();

                if (_productoEditar.IdCategoria != null)
                {
                    cmbCategoria.SelectedValue = _productoEditar.IdCategoria.IdCategoria;
                }
            }
        }

        private void CargarCategorias()
        {
            try
            {
                CN_Categoria objNegocioCategoria = new CN_Categoria();
                List<Categoria> lista = objNegocioCategoria.Listar();

                if (lista == null || lista.Count == 0)
                {
                    string[] categoriasDefecto = new string[] { "Alimentos", "Accesorios", "Juguetes", "Higiene y Estética", "Farmacia" };
                    string msg = string.Empty;
                    foreach (string nombre in categoriasDefecto)
                    {
                        objNegocioCategoria.Registrar(new Categoria()
                        {
                            Nombre_categoria = nombre,
                            Estado_categoria = true
                        }, out msg);
                    }

                    // Volvemos a listar las categorías recién creadas
                    lista = objNegocioCategoria.Listar();
                }

                cmbCategoria.DisplayMember = "Nombre_categoria";
                cmbCategoria.ValueMember = "IdCategoria";
                cmbCategoria.DataSource = lista;
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos y seleccione una categoría.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal precioUnitario = Convert.ToDecimal(txtStock.Text.Trim().Replace('.', ','));
                decimal precioVenta = Convert.ToDecimal(guna2TextBox1.Text.Trim().Replace('.', ','));
                int stock = Convert.ToInt32(txtPrecio.Text.Trim());

                Producto objProducto = new Producto()
                {
                    Nombre_producto = txtNombre.Text.Trim(),
                    Descripcion_producto = txtNombre.Text.Trim(),
                    PrecioUnitario_producto = precioUnitario,
                    Precio_producto = precioVenta,
                    Stock_producto = stock,
                    Stock_minimo = 0,
                    IdCategoria = new Categoria()
                    {
                        IdCategoria = Convert.ToInt32(cmbCategoria.SelectedValue)
                    },
                    Estado_producto = true
                };

                string mensaje = string.Empty;
                CN_Producto objNegocio = new CN_Producto();
                bool resultado = false;

                if (_productoEditar == null)
                {
                    int idProducto = objNegocio.Registrar(objProducto, out mensaje);
                    resultado = idProducto > 0;
                }
                else
                {
                    objProducto.IdProducto = _productoEditar.IdProducto;
                    resultado = objNegocio.Editar(objProducto, out mensaje);
                }

                if (resultado)
                {
                    MessageBox.Show(_productoEditar == null ? "¡Producto registrado exitosamente!" : "¡Producto modificado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el producto: " + mensaje, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en los formatos ingresados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
        }

        private void LBNombreProducto_Click(object sender, EventArgs e)
        {
        }


        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}