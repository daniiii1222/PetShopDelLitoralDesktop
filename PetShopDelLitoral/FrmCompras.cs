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
using CapaDatos;

namespace PetShopDelLitoral
{
    public partial class FrmCompras : Form
    {
        private List<Producto> _listaProductos = new List<Producto>();
        private Producto _productoSeleccionado = null;

        public FrmCompras()
        {
            InitializeComponent();

            // Enlazar eventos de botones del diseñador
            this.iconButton6.Click += new EventHandler(this.btnAgregar_Click);
            this.iconButton5.Click += new EventHandler(this.btnVaciarLista_Click);
            this.botonGuardarCompra.Click += new EventHandler(this.btnGuardarCompra_Click);
            this.iconButton2.Click += new EventHandler(this.btnCancelar_Click);
            this.iconButton4.Click += new EventHandler(this.btnBuscarProducto_Click);
            this.dgvCompras.CellContentClick += new DataGridViewCellEventHandler(this.dgvCompras_CellContentClick);
        }

        private void FrmCompras_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            CargarProveedores();
            CargarTiposComprobante();
            CargarProductosAutocompletado();
            InicializarCampos();
        }

        private void ConfigurarGrilla()
        {
            dgvCompras.Rows.Clear();
            dgvCompras.ReadOnly = true;
            dgvCompras.AllowUserToAddRows = false;
            dgvCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCompras.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dgvCompras.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void InicializarCampos()
        {
            guna2DateTimePicker1.Value = DateTime.Today;

            // Encargado de la compra (Usuario actual)
            if (Sesion.UsuarioActual != null && Sesion.UsuarioActual.idPersona != null)
            {
                guna2TextBox2.Text = $"{Sesion.UsuarioActual.idPersona.nombre_persona} {Sesion.UsuarioActual.idPersona.apellido_persona}";
            }
            else
            {
                guna2TextBox2.Text = "Administrador";
            }
            guna2TextBox2.ReadOnly = true;

            // Totales iniciales
            guna2TextBox4.Text = "$ 0,00"; // Descuento / IVA
            guna2TextBox4.ReadOnly = true;

            guna2TextBox5.Text = "$ 0,00"; // Subtotal
            guna2TextBox5.ReadOnly = true;

            guna2TextBox6.Text = "$ 0,00"; // Total
            guna2TextBox6.ReadOnly = true;

            guna2NumericUpDown1.Value = 1;
            guna2NumericUpDown1.Minimum = 1;
            guna2NumericUpDown1.Maximum = 10000;
        }

        private void CargarTiposComprobante()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] { "Factura A", "Factura B", "Factura C", "Remito", "Ticket" });
            comboBox1.SelectedIndex = 0;
        }

        private void CargarProveedores()
        {
            try
            {
                CN_Proveedor objNegocioProv = new CN_Proveedor();
                List<Proveedor> lista = objNegocioProv.Listar();

                // Si no hay proveedores en la base de datos, agregamos proveedores iniciales de ejemplo
                if (lista == null || lista.Count == 0)
                {
                    CrearProveedoresIniciales();
                    lista = objNegocioProv.Listar();
                }

                var itemsCombo = lista.Select(p => new
                {
                    Id = p.Id_proveedor,
                    Nombre = p.Id_persona != null 
                        ? $"{p.Id_persona.nombre_persona} {p.Id_persona.apellido_persona} ({p.Id_persona.dni_persona})" 
                        : $"Proveedor #{p.Id_proveedor}"
                }).ToList();

                guna2ComboBox1.DisplayMember = "Nombre";
                guna2ComboBox1.ValueMember = "Id";
                guna2ComboBox1.DataSource = itemsCombo;

                if (itemsCombo.Count > 0)
                    guna2ComboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearProveedoresIniciales()
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("Server=localhost; Database=petshopdellitoral; Uid=root; Pwd=;"))
                {
                    conn.Open();

                    // Insertar personas para los proveedores
                    string sql = @"
                        INSERT INTO persona(nombre_persona, apellido_persona, dni_persona, correo_persona, telefono_persona, direccion_persona, estado_persona, fechaCreacion_persona)
                        VALUES('Distribuidora PetFood', 'Litoral', '30-11223344-5', 'ventas@petfood.com', '3794112233', 'Av. Libertad 1234', 1, NOW()),
                              ('Laboratorio VetSalud', 'Argentina', '30-55667788-9', 'contacto@vetsalud.com', '3794556677', 'Ruta 12 Km 5', 1, NOW());
                        
                        INSERT INTO proveedor(id_persona, estado_proveedor, fechaCreacion_proveedor)
                        SELECT idPersona, 1, NOW() FROM persona WHERE dni_persona IN ('30-11223344-5', '30-55667788-9')
                        AND idPersona NOT IN (SELECT id_persona FROM proveedor);
                    ";

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
            }
        }

        private void CargarProductosAutocompletado()
        {
            try
            {
                _listaProductos = new CN_Producto().Listar();

                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                foreach (Producto prod in _listaProductos)
                {
                    if (!string.IsNullOrEmpty(prod.Nombre_producto))
                    {
                        coleccion.Add(prod.Nombre_producto);
                    }
                }

                guna2TextBox7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                guna2TextBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
                guna2TextBox7.AutoCompleteCustomSource = coleccion;
            }
            catch (Exception)
            {
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            SeleccionarProductoPorNombre();
        }

        private void SeleccionarProductoPorNombre()
        {
            string texto = guna2TextBox7.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Ingrese el nombre de un producto para buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _productoSeleccionado = _listaProductos.FirstOrDefault(p =>
                p.Nombre_producto.Equals(texto, StringComparison.OrdinalIgnoreCase) ||
                p.Nombre_producto.ToLower().Contains(texto.ToLower()));

            if (_productoSeleccionado != null)
            {
                guna2TextBox7.Text = _productoSeleccionado.Nombre_producto;

                decimal costoSugerido = _productoSeleccionado.PrecioUnitario_producto > 0 
                    ? _productoSeleccionado.PrecioUnitario_producto 
                    : _productoSeleccionado.Precio_producto;

                textBox2.Text = costoSugerido.ToString("F2");
                guna2NumericUpDown1.Value = 1;
                guna2NumericUpDown1.Focus();
            }
            else
            {
                MessageBox.Show("No se encontró ningún producto con ese nombre.", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Si no se buscó previamente, intentamos encontrarlo por el texto actual
            if (_productoSeleccionado == null)
            {
                string texto = guna2TextBox7.Text.Trim();
                _productoSeleccionado = _listaProductos.FirstOrDefault(p =>
                    p.Nombre_producto.Equals(texto, StringComparison.OrdinalIgnoreCase));
            }

            if (_productoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un producto válido antes de agregar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox7.Focus();
                return;
            }

            if (!decimal.TryParse(textBox2.Text.Trim().Replace('.', ','), out decimal precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("Ingrese un precio de compra válido mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            int cantidad = (int)guna2NumericUpDown1.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2NumericUpDown1.Focus();
                return;
            }

            decimal subtotal = precioCompra * cantidad;

            // Verificar si el producto ya existe en la grilla para sumar cantidad
            bool existe = false;
            foreach (DataGridViewRow fila in dgvCompras.Rows)
            {
                if (fila.Tag is Producto p && p.IdProducto == _productoSeleccionado.IdProducto)
                {
                    int cantidadActual = Convert.ToInt32(fila.Cells["columnaCantidad"].Value);
                    int nuevaCantidad = cantidadActual + cantidad;
                    decimal nuevoSubtotal = nuevaCantidad * precioCompra;

                    fila.Cells["coolumnaPrecio"].Value = precioCompra.ToString("F2");
                    fila.Cells["columnaCantidad"].Value = nuevaCantidad;
                    fila.Cells["columnaSubtotal"].Value = nuevoSubtotal.ToString("F2");

                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                int indexFila = dgvCompras.Rows.Add(new object[] {
                    _productoSeleccionado.Nombre_producto,
                    precioCompra.ToString("F2"),
                    cantidad,
                    subtotal.ToString("F2"),
                    "Eliminar"
                });

                dgvCompras.Rows[indexFila].Tag = _productoSeleccionado;
            }

            CalcularTotales();

            // Limpiar selección para el próximo producto
            guna2TextBox7.Text = "";
            textBox2.Text = "";
            guna2NumericUpDown1.Value = 1;
            _productoSeleccionado = null;
            guna2TextBox7.Focus();
        }

        private void CalcularTotales()
        {
            decimal total = 0;

            foreach (DataGridViewRow fila in dgvCompras.Rows)
            {
                if (fila.Cells["columnaSubtotal"].Value != null)
                {
                    total += Convert.ToDecimal(fila.Cells["columnaSubtotal"].Value);
                }
            }

            guna2TextBox5.Text = $"$ {total:N2}";
            guna2TextBox6.Text = $"$ {total:N2}";
        }

        private void dgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Si hace clic en la columna Eliminar (columnaEliminar o índice 4)
                if (dgvCompras.Columns[e.ColumnIndex].Name == "columnaEliminar" || e.ColumnIndex == 4)
                {
                    dgvCompras.Rows.RemoveAt(e.RowIndex);
                    CalcularTotales();
                }
            }
        }

        private void btnVaciarLista_Click(object sender, EventArgs e)
        {
            if (dgvCompras.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("¿Está seguro de vaciar la lista de compra actual?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    dgvCompras.Rows.Clear();
                    CalcularTotales();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dgvCompras.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("¿Desea cancelar la compra actual? Se perderán los productos agregados a la lista.", "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    dgvCompras.Rows.Clear();
                    CalcularTotales();
                    guna2TextBox7.Text = "";
                    textBox2.Text = "";
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnGuardarCompra_Click(object sender, EventArgs e)
        {
            if (dgvCompras.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la lista de compra.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (guna2ComboBox1.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor para la compra.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = 0;
            List<DetalleCompracs> listaDetalle = new List<DetalleCompracs>();

            foreach (DataGridViewRow fila in dgvCompras.Rows)
            {
                if (fila.Tag is Producto prod)
                {
                    int cant = Convert.ToInt32(fila.Cells["columnaCantidad"].Value);
                    decimal precio = Convert.ToDecimal(fila.Cells["coolumnaPrecio"].Value);
                    decimal sub = Convert.ToDecimal(fila.Cells["columnaSubtotal"].Value);
                    total += sub;

                    listaDetalle.Add(new DetalleCompracs()
                    {
                        IdProducto = prod,
                        Cantidad = cant,
                        Precio_unitario = precio,
                        Subtotal_compra = sub
                    });
                }
            }

            DialogResult confirmacion = MessageBox.Show(
                $"¿Confirmar el registro de la compra por un total de $ {total:N2}?\nEl stock de los productos se actualizará automáticamente.",
                "Confirmar Compra",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            CompraProveedor objCompra = new CompraProveedor()
            {
                Monto_compra = total,
                Fecha_compra = guna2DateTimePicker1.Value,
                IdUsuario = Sesion.UsuarioActual,
                IdProveedor = new Proveedor()
                {
                    Id_proveedor = Convert.ToInt32(guna2ComboBox1.SelectedValue)
                },
                Tipo_comprobante = comboBox1.Text,
                Numero_comprobante = string.IsNullOrWhiteSpace(textBox1.Text) ? "0001-00000001" : textBox1.Text.Trim(),
                Observaciones = guna2TextBox3.Text.Trim()
            };

            string mensajeError;
            CN_Compra objNegocioCompra = new CN_Compra();
            bool guardado = objNegocioCompra.Registrar(objCompra, listaDetalle, out mensajeError);

            if (guardado)
            {
                MessageBox.Show(
                    "¡Compra registrada exitosamente!\nLos productos han sido actualizados con el nuevo stock y precio de costo.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Limpiar grilla y formulario
                dgvCompras.Rows.Clear();
                CalcularTotales();
                textBox1.Text = "";
                guna2TextBox3.Text = "";
                guna2TextBox7.Text = "";
                textBox2.Text = "";
                guna2NumericUpDown1.Value = 1;
                _productoSeleccionado = null;

                // Actualizar lista interna de productos para reflejar nuevos stocks
                _listaProductos = new CN_Producto().Listar();
                CargarProductosAutocompletado();
            }
            else
            {
                MessageBox.Show("No se pudo registrar la compra: " + mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Métodos de eventos visuales del diseñador (se conservan)
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