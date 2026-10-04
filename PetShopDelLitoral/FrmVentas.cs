using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    public partial class FrmVentas : Form
    {

        private readonly CN_Venta cnVenta = new CN_Venta();
        private readonly CultureInfo cultura = new CultureInfo("es-AR");

        // Fuente de verdad del detalle: la grilla solo se "dibuja" a partir de esta lista
        private readonly List<DetalleVenta> detalles = new List<DetalleVenta>();

        private int idClienteSeleccionado = 0;
        private DataRow productoSeleccionado = null;
        private bool cargandoCliente = false; // evita que el TextChanged borre el cliente recién encontrado

        public FrmVentas()
        {
            InitializeComponent();

            botonInicio.Click += BuscarCliente_Click;       // lupa junto al cliente
            iconButton4.Click += BuscarProducto_Click;      // lupa junto al buscador de productos
            iconButton6.Click += AgregarProducto_Click;     // "Agregar"
            iconButton5.Click += VaciarLista_Click;         // "Vaciar Lista"
            iconButton2.Click += Cancelar_Click;            // "Cancelar"
            iconButton3.Click += GuardarVenta_Click;        // "Guardar Venta"
            dgvVentas.CellClick += DgvVentas_CellClick;     // columna "Eliminar"

            // Si se edita el DNI después de buscar, el cliente elegido deja de ser válido
            guna2TextBox4.TextChanged += (s, ev) => ActualizarTotales();   // % de descuento de la compra
            dgvVentas.CellEndEdit += DgvVentas_CellEndEdit;                 // % de descuento por producto

            guna2TextBox1.TextChanged += (s, ev) => { if (!cargandoCliente) idClienteSeleccionado = 0; };

            // Enter dispara la búsqueda (DNI y producto)
            guna2TextBox1.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; BuscarCliente_Click(s, ev); }
            };
            guna2TextBox7.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; BuscarProducto_Click(s, ev); }
            };
        }


        private void FrmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarMetodosPago();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Vendedor: sale de la sesión, no se edita
            if (Sesion.UsuarioActual != null && Sesion.UsuarioActual.idPersona != null)
            {
                guna2TextBox2.Text = Sesion.UsuarioActual.idPersona.nombre_persona + " " +
                                     Sesion.UsuarioActual.idPersona.apellido_persona;
            }
            guna2TextBox2.ReadOnly = true;

            guna2TextBox1.PlaceholderText = "DNI del cliente...";
            CargarSugerenciasDni();
            guna2DateTimePicker1.Value = DateTime.Today;

            // Los importes se calculan solos
            guna2TextBox5.ReadOnly = true; // Subtotal
            guna2TextBox4.ReadOnly = false; // Descuento % sobre la compra completa
            guna2TextBox4.PlaceholderText = "0";
            guna2TextBox4.Text = "0";
            label7.Text = "Descuento (%)";
            guna2TextBox6.ReadOnly = true; // Total
            guna2TextBox6.ForeColor = Color.White; // sobre la barra oscura del Total el texto no se leía
            guna2NumericUpDown1.Minimum = 1;
            guna2NumericUpDown1.Maximum = 9999;
            guna2NumericUpDown1.Value = 1;

            dgvVentas.AllowUserToAddRows = false;
            ConfigurarColumnaDescuento();
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dgvVentas.DefaultCellStyle.SelectionForeColor = Color.Black;

            RefrescarGrilla();
        }

        private void CargarMetodosPago()
        {
            DataTable dt = cnVenta.ListarMetodosPago().Tables["TablaMetodosPago"];
            guna2ComboBox1.DataSource = dt;
            guna2ComboBox1.DisplayMember = "nombre_metodo";
            guna2ComboBox1.ValueMember = "idMetodoPago";
            guna2ComboBox1.SelectedIndex = -1;
        }

        // ---------- CLIENTE ----------
        private void BuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = cnVenta.ObtenerClientePorDni(guna2TextBox1.Text);
                if (dt.Rows.Count == 0)
                {
                    idClienteSeleccionado = 0;
                    MessageBox.Show("No hay un cliente activo con ese DNI.", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow r = dt.Rows[0];
                idClienteSeleccionado = Convert.ToInt32(r["idCliente"]);
                cargandoCliente = true;
                guna2TextBox1.Text = r["nombre_persona"] + " " + r["apellido_persona"] + " (" + r["dni_persona"] + ")";
                cargandoCliente = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Autocompletado del DNI: se cargan los DNI una vez al abrir el form y el propio cuadro de texto
        // muestra las coincidencias mientras se escribe. Si falla, simplemente no hay sugerencias.
        private void CargarSugerenciasDni()
        {
            try
            {
                DataTable clientes = new CN_Cliente().ListarClientes().Tables["TablaClientes"];
                AutoCompleteStringCollection dnis = new AutoCompleteStringCollection();

                foreach (DataRow r in clientes.Rows)
                {
                    bool activo = !clientes.Columns.Contains("estado_cliente") || Convert.ToBoolean(r["estado_cliente"]);
                    if (activo) dnis.Add(r["dni_persona"].ToString());
                }

                guna2TextBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                guna2TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                guna2TextBox1.AutoCompleteCustomSource = dnis;
            }
            catch { }
        }

        // ---------- PRODUCTOS ----------
        private void BuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = cnVenta.BuscarProductos(guna2TextBox7.Text);
                if (dt.Rows.Count == 0)
                {
                    productoSeleccionado = null;
                    MessageBox.Show("No se encontraron productos con stock con ese nombre.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                productoSeleccionado = dt.Rows.Count == 1 ? dt.Rows[0] : ElegirProducto(dt);
                if (productoSeleccionado != null)
                {
                    guna2TextBox7.Text = productoSeleccionado["nombre_producto"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Mini ventana para elegir cuando la búsqueda devuelve varios productos
        private DataRow ElegirProducto(DataTable dt)
        {
            using (Form f = new Form())
            using (ListBox lb = new ListBox())
            using (Button ok = new Button())
            {
                f.Text = "Elegí un producto";
                f.StartPosition = FormStartPosition.CenterParent;
                f.ClientSize = new Size(360, 260);
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MaximizeBox = false;
                f.MinimizeBox = false;

                lb.Dock = DockStyle.Top;
                lb.Height = 210;
                foreach (DataRow r in dt.Rows)
                {
                    lb.Items.Add(r["nombre_producto"] + "  -  $" +
                                 Convert.ToDecimal(r["precio_producto"]).ToString("N2", cultura) +
                                 "  (stock " + r["stock_producto"] + ")");
                }
                lb.SelectedIndex = 0;
                lb.DoubleClick += (s, ev) => f.DialogResult = DialogResult.OK;

                ok.Text = "Seleccionar";
                ok.Dock = DockStyle.Bottom;
                ok.DialogResult = DialogResult.OK;

                f.Controls.Add(lb);
                f.Controls.Add(ok);
                f.AcceptButton = ok;

                return f.ShowDialog(this) == DialogResult.OK && lb.SelectedIndex >= 0
                    ? dt.Rows[lb.SelectedIndex]
                    : null;
            }
        }

        private void AgregarProducto_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Buscá y elegí un producto primero.", "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProducto = Convert.ToInt32(productoSeleccionado["idProducto"]);
            int stock = Convert.ToInt32(productoSeleccionado["stock_producto"]);
            int cantidad = (int)guna2NumericUpDown1.Value;

            // Si el producto ya está en el detalle, se suma a lo existente
            DetalleVenta existente = detalles.FirstOrDefault(d => d.IdProducto.IdProducto == idProducto);
            int yaAgregado = existente != null ? existente.Cantidad : 0;

            if (yaAgregado + cantidad > stock)
            {
                MessageBox.Show("Stock insuficiente. Disponible: " + stock ,
                                "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (existente != null)
            {
                existente.Cantidad += cantidad;
                CalcularSubtotal(existente);
            }
            else
            {
                decimal precio = Convert.ToDecimal(productoSeleccionado["precio_producto"]);
                detalles.Add(new DetalleVenta
                {
                    IdProducto = new Producto
                    {
                        IdProducto = idProducto,
                        Nombre_producto = productoSeleccionado["nombre_producto"].ToString(),
                        Stock_producto = stock
                    },
                    Cantidad = cantidad,
                    Precio = precio,
                    Subtotal_venta = precio * cantidad
                });
            }

            productoSeleccionado = null;
            guna2TextBox7.Clear();
            guna2NumericUpDown1.Value = 1;
            RefrescarGrilla();
        }

        private void DgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= detalles.Count) return;
            if (dgvVentas.Columns[e.ColumnIndex].Name != "columnaEliminar") return;

            detalles.RemoveAt(e.RowIndex);
            RefrescarGrilla();
        }

        private void VaciarLista_Click(object sender, EventArgs e)
        {
            detalles.Clear();
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            dgvVentas.Rows.Clear();
            foreach (DetalleVenta d in detalles)
            {
                dgvVentas.Rows.Add(
                    d.IdProducto.Nombre_producto,
                    d.Precio.ToString("N2", cultura),
                    d.Cantidad,
                    d.Descuento_detalle.ToString("0.##", cultura),
                    d.Subtotal_venta.ToString("N2", cultura),
                    "Quitar");
            }

            ActualizarTotales();
        }

        // ---------- DESCUENTOS ----------
        // Columna "Desc. %" editable: se agrega por código para no tocar el Designer.
        // Orden resultante: Producto | Precio | Cantidad | Desc. % | Subtotal | Eliminar
        private void ConfigurarColumnaDescuento()
        {
            if (!dgvVentas.Columns.Contains("columnaDescuento"))
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = "columnaDescuento";
                col.HeaderText = "Desc. %";
                col.MinimumWidth = 6;
                dgvVentas.Columns.Insert(dgvVentas.Columns["columnaSubtotal"].Index, col);
            }

            // Solo la columna de descuento se puede editar; el resto sigue de solo lectura
            dgvVentas.ReadOnly = false;
            foreach (DataGridViewColumn c in dgvVentas.Columns)
            {
                c.ReadOnly = c.Name != "columnaDescuento";
                // Si se ordena por una columna, las filas dejan de coincidir con la lista "detalles" y se rompe todo
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvVentas.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        // Subtotal de la línea = precio x cantidad menos el % de la línea.
        // Mismo redondeo que el SP (ROUND a 2 decimales, mitad hacia arriba).
        private void CalcularSubtotal(DetalleVenta d)
        {
            d.Subtotal_venta = Math.Round(d.Precio * d.Cantidad * (1 - d.Descuento_detalle / 100m),
                                          2, MidpointRounding.AwayFromZero);
        }

        // Acepta "10", "10,5", "10.5" y "10%". Vacío = 0. Devuelve false si no es número o no está entre 0 y 100.
        private bool TryParsePorcentaje(string texto, out decimal valor)
        {
            valor = 0m;
            string t = (texto ?? "").Replace("%", "").Trim().Replace('.', ',');
            if (t.Length == 0) return true;

            decimal parsed;
            if (!decimal.TryParse(t, NumberStyles.Number, cultura, out parsed)) return false;

            parsed = Math.Round(parsed, 2);
            if (parsed < 0 || parsed > 100) return false;

            valor = parsed;
            return true;
        }

        private void DgvVentas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= detalles.Count) return;
            if (dgvVentas.Columns[e.ColumnIndex].Name != "columnaDescuento") return;

            DetalleVenta d = detalles[e.RowIndex];
            object ingresado = dgvVentas[e.ColumnIndex, e.RowIndex].Value;

            decimal pct;
            if (!TryParsePorcentaje(ingresado == null ? "" : ingresado.ToString(), out pct))
            {
                MessageBox.Show("El descuento debe ser un número entre 0 y 100.", "Descuento",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pct = d.Descuento_detalle; // vuelve al valor anterior
            }

            d.Descuento_detalle = pct;
            CalcularSubtotal(d);

            // No se reconstruye la grilla acá (estamos dentro de su propio evento): se actualizan las celdas
            dgvVentas[e.ColumnIndex, e.RowIndex].Value = pct.ToString("0.##", cultura);
            dgvVentas["columnaSubtotal", e.RowIndex].Value = d.Subtotal_venta.ToString("N2", cultura);
            ActualizarTotales();
        }

        // Subtotal = suma de líneas (ya con su descuento). Total = subtotal menos el % de la compra.
        private void ActualizarTotales()
        {
            decimal descuentoCompra;
            if (!TryParsePorcentaje(guna2TextBox4.Text, out descuentoCompra)) descuentoCompra = 0m;

            decimal subtotal = detalles.Sum(d => d.Subtotal_venta);
            decimal total = Math.Round(subtotal * (1 - descuentoCompra / 100m), 2, MidpointRounding.AwayFromZero);

            guna2TextBox5.Text = "$ " + subtotal.ToString("N2", cultura);
            guna2TextBox6.Text = "$ " + total.ToString("N2", cultura);
        }

        // ---------- GUARDAR / CANCELAR ----------
        private void GuardarVenta_Click(object sender, EventArgs e)
        {
            dgvVentas.EndEdit(); // confirma un descuento que haya quedado a medio escribir

            decimal descuentoCompra;
            if (!TryParsePorcentaje(guna2TextBox4.Text, out descuentoCompra))
            {
                MessageBox.Show("El descuento de la compra debe ser un número entre 0 y 100.", "Descuento",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Venta venta = new Venta
            {
                Descuento_venta = descuentoCompra,
                Fecha_venta = guna2DateTimePicker1.Value.Date,
                IdUsuario = Sesion.UsuarioActual,
                IdCliente = new Cliente { IdCliente = idClienteSeleccionado },
                IdMetodoPago = new MetodoPago
                {
                    IdMetodoPago = guna2ComboBox1.SelectedValue == null ? 0 : Convert.ToInt32(guna2ComboBox1.SelectedValue)
                }
            };

            int idVenta;
            string mensaje;

            if (cnVenta.RegistrarVenta(venta, detalles, out idVenta, out mensaje))
            {
                MessageBox.Show("Venta N° " + idVenta + " registrada con éxito.", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show(mensaje, "No se pudo registrar la venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            detalles.Clear();
            idClienteSeleccionado = 0;
            productoSeleccionado = null;
            guna2TextBox1.Clear();
            guna2TextBox3.Clear();
            guna2TextBox7.Clear();
            guna2TextBox4.Text = "0";
            guna2ComboBox1.SelectedIndex = -1;
            guna2DateTimePicker1.Value = DateTime.Today;
            guna2NumericUpDown1.Value = 1;
            RefrescarGrilla();
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