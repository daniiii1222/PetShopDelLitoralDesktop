using Capa_Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    public partial class FrmReportesAdministrador : Form
    {
        // Lista de usuarios (vendedores) que se carga al abrir la pantalla
        private DataTable dtVendedores;

        // Misma idea que idClienteSeleccionado en FrmVentas: 0 = ninguno elegido / todos
        private int idVendedorSeleccionado = 0;
        private bool cargandoVendedor = false; // evita que el TextChanged borre el vendedor recién encontrado

        public FrmReportesAdministrador()
        {
            InitializeComponent();
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;

            // El Designer no siempre conecta el Load: lo enganchamos acá (el -= evita que corra dos veces si ya estaba)
            this.Load -= FrmReportesAdministrador_Load;
            this.Load += FrmReportesAdministrador_Load;

            // Si se edita el texto después de elegir, el vendedor elegido deja de ser válido
            txtBuscarVendedor.TextChanged += (s, ev) => { if (!cargandoVendedor) idVendedorSeleccionado = 0; };

            // Enter dispara la búsqueda
            txtBuscarVendedor.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; btnBuscar_Click(s, ev); }
            };
        }

        private void FrmReportesAdministrador_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDesde.Value = DateTime.Today;
                dtpHasta.Value = DateTime.Today;

                CargarSugerenciasDni();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar autocompletado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Autocompletado del DNI del vendedor (igual que CargarSugerenciasDni de FrmVentas).
        // Se incluyen también los inactivos: sus ventas viejas se tienen que poder consultar.
        private void CargarSugerenciasDni()
        {
            dtVendedores = new CN_Usuario().ListarUsuarios().Tables[0];
            AutoCompleteStringCollection dnis = new AutoCompleteStringCollection();

            foreach (DataRow f in dtVendedores.Rows)
                dnis.Add(f["dni_persona"].ToString());

            txtBuscarVendedor.PlaceholderText = "DNI del vendedor (vacío = todos)...";
            txtBuscarVendedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBuscarVendedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBuscarVendedor.AutoCompleteCustomSource = dnis;
        }

        // Convierte lo escrito en un idUsuario. Devuelve false si el DNI no corresponde a nadie.
        private bool ResolverVendedor()
        {
            string texto = txtBuscarVendedor.Text.Trim();

            if (texto == "") { idVendedorSeleccionado = 0; return true; }   // todos los vendedores
            if (idVendedorSeleccionado != 0) return true;                   // ya lo había elegido antes

            // Por si el Load no corrió: sin esta lista no hay contra qué comparar el DNI
            if (dtVendedores == null) CargarSugerenciasDni();

            foreach (DataRow f in dtVendedores.Rows)
            {
                if (f["dni_persona"].ToString() == texto)
                {
                    idVendedorSeleccionado = Convert.ToInt32(f["idUsuario"]);

                    cargandoVendedor = true;
                    txtBuscarVendedor.Text = f["nombre_persona"] + " " + f["apellido_persona"] + " (" + f["dni_persona"] + ")";
                    cargandoVendedor = false;
                    return true;
                }
            }

            MessageBox.Show("No hay un vendedor con ese DNI.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // BOTÓN BUSCAR CON FILTROS REALES A LA BASE DE DATOS
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime inicio = dtpDesde.Value.Date;
                DateTime fin = dtpHasta.Value.Date;

                // Si escribió un DNI inválido, cortamos acá para no buscar con un filtro incorrecto
                if (!ResolverVendedor()) return;
                int idVendedor = idVendedorSeleccionado; // 0 = traer todos

                CN_Reporte cnReporte = new CN_Reporte();
                DataTable dt = cnReporte.ObtenerReporteVentas(inicio, fin, idVendedor);

                MostrarVentas(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron ventas con estos filtros.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // El Designer deja la grilla con Visible = false y el texto en color Cornsilk sobre fondo blanco
        // (invisible). Esto la muestra y deja el texto legible.
        private void HacerVisibleGrilla()
        {
            dataGridReportes.Visible = true;
            dataGridReportes.ThemeStyle.RowsStyle.ForeColor = Color.Black;
            dataGridReportes.DefaultCellStyle.ForeColor = Color.Black;
            dataGridReportes.AllowUserToAddRows = false;   // saca la fila vacía de abajo
            dataGridReportes.AllowUserToDeleteRows = false;
            dataGridReportes.ReadOnly = true;              // los reportes no se editan (el botón "Ver Detalle" sigue andando)
        }

        // Columnas fijas (no se pueden agrandar ni mover) con todo el texto visible.
        // Mide con TextRenderer el texto más largo de cada columna (encabezado, celdas y texto del
        // botón) y lo usa como ancho mínimo; el ancho sobrante se reparte proporcional para que la
        // grilla ocupe todo el ancho. Devuelve el ancho total mínimo que necesita la grilla.
        private int AjustarColumnas(DataGridView dgv)
        {
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False; // encabezado en una línea

            // Datos y encabezados centrados (el Designer los dejaba alineados a la izquierda)
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Encabezado de altura fija: no se puede estirar con el mouse
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 32;
            Guna.UI2.WinForms.Guna2DataGridView guna = dgv as Guna.UI2.WinForms.Guna2DataGridView;
            if (guna != null)
            {
                guna.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                guna.ThemeStyle.HeaderStyle.Height = 32;
            }

            // El encabezado seleccionado/ordenado no cambia de color (antes se ponía celeste)
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;

            Font fuenteCelda = dgv.DefaultCellStyle.Font ?? dgv.Font;
            Font fuenteEncabezado = dgv.ColumnHeadersDefaultCellStyle.Font ?? dgv.Font;

            int[] anchos = new int[dgv.Columns.Count];
            int total = 0;

            for (int i = 0; i < anchos.Length; i++)
            {
                DataGridViewColumn col = dgv.Columns[i];
                col.SortMode = DataGridViewColumnSortMode.NotSortable;   // clic en el encabezado no ordena
                // La columna del id mantiene su título "N° Venta" pero con menos aire alrededor
                int relleno = EsNombreColumnaIdVenta(col.Name) ? 16 : 30;
                int ancho = TextRenderer.MeasureText(col.HeaderText ?? "", fuenteEncabezado).Width + relleno;

                DataGridViewButtonColumn colBoton = col as DataGridViewButtonColumn;
                if (colBoton != null)
                {
                    ancho = Math.Max(ancho, TextRenderer.MeasureText(colBoton.Text ?? "", fuenteCelda).Width + 30);
                }
                else
                {
                    int filas = Math.Min(dgv.Rows.Count, 500);
                    for (int r = 0; r < filas; r++)
                    {
                        string texto = Convert.ToString(dgv.Rows[r].Cells[i].FormattedValue) ?? "";
                        ancho = Math.Max(ancho, TextRenderer.MeasureText(texto, fuenteCelda).Width + 24);
                    }
                }

                anchos[i] = ancho;
                total += ancho;
            }

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < anchos.Length; i++)
            {
                DataGridViewColumn col = dgv.Columns[i];
                col.MinimumWidth = anchos[i];                  // nunca más angosta que su texto
                if (EsNombreColumnaIdVenta(col.Name))
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;   // ancho justo, no recibe sobrante
                    col.Width = anchos[i];
                }
                else
                {
                    col.FillWeight = anchos[i];                // el sobrante se reparte proporcional
                }
            }

            return total;
        }

        private void MostrarVentas(DataTable dt)
        {
            // Las columnas que armó el Designer no tienen DataPropertyName, así que quedarían vacías
            // al lado de las que genera el DataTable. Se sacan y se deja solo el botón "Ver Detalle".
            DataGridViewColumn[] manuales = { columnaCodigo, columnaFecha, columnaVendedor, columnaCliente, columnaMetodoPago, columnaTotal };
            foreach (DataGridViewColumn c in manuales)
                if (dataGridReportes.Columns.Contains(c)) dataGridReportes.Columns.Remove(c);

            HacerVisibleGrilla();
            dataGridReportes.AutoGenerateColumns = true;
            dataGridReportes.DataSource = dt;

            // ConfigurarGrilla() hace Columns.Clear(), así que el botón puede haberse perdido
            if (!dataGridReportes.Columns.Contains(columnaDetalle))
                dataGridReportes.Columns.Add(columnaDetalle);
            columnaDetalle.DisplayIndex = dataGridReportes.Columns.Count - 1;

            AjustarColumnas(dataGridReportes);
        }

        // true si el nombre es "N° Venta" / "Nº Venta" (grado u ordinal)
        private static bool EsNombreColumnaIdVenta(string nombre)
        {
            nombre = (nombre ?? "").Trim();
            return nombre.StartsWith("N", StringComparison.OrdinalIgnoreCase) &&
                   nombre.EndsWith("Venta", StringComparison.OrdinalIgnoreCase);
        }

        // Busca la columna del id de venta; si no la encuentra usa la primera
        private DataColumn ColumnaIdVenta(DataTable tabla)
        {
            foreach (DataColumn c in tabla.Columns)
                if (EsNombreColumnaIdVenta(c.ColumnName)) return c;
            return tabla.Columns[0];
        }

        // VENTANA DEL DETALLE AL HACER CLIC EN LA COLUMNA "Detalle"
        // Los dos handlers existen porque el Designer puede estar enganchado a cualquiera de los dos.
        private void dataGridReportes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AbrirDetalleSiCorresponde(e);
        }

        private void dataGridReportes_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            AbrirDetalleSiCorresponde(e);
        }

        private void AbrirDetalleSiCorresponde(DataGridViewCellEventArgs e)
        {
            // Clic en los encabezados: no hacemos nada
            if (e.RowIndex < 0) return;

            if (dataGridReportes.Columns[e.ColumnIndex].HeaderText != "Detalle" &&
                dataGridReportes.Columns[e.ColumnIndex].Name != "columnaDetalle") return;

            try
            {
                // El id es la primera columna del DataTable (N° Venta). Se lee de ahí y no por nombre,
                // porque "N°" (grado) y "Nº" (ordinal) se parecen pero son caracteres distintos.
                DataRowView fila = dataGridReportes.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (fila == null) return;
                int idVenta = Convert.ToInt32(fila.Row[ColumnaIdVenta(fila.Row.Table)]);

                CN_Reporte cnReporte = new CN_Reporte();
                DataTable dtDetalle = cnReporte.ObtenerDetalleVenta(idVenta);

                using (Form frmDetalle = new Form())
                {
                    frmDetalle.Text = "Detalle de Venta N° " + idVenta;
                    frmDetalle.Size = new Size(600, 350);
                    frmDetalle.StartPosition = FormStartPosition.CenterParent;
                    frmDetalle.ShowIcon = false;

                    DataGridView dgvDetalle = new DataGridView
                    {
                        DataSource = dtDetalle,
                        Dock = DockStyle.Fill,
                        ReadOnly = true,
                        AllowUserToAddRows = false,
                        RowHeadersVisible = false,
                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                        BackgroundColor = Color.White
                    };

                    // La ventana se agranda lo necesario para que se lea todo (entre 450 y 1000 px)
                    int anchoNecesario = AjustarColumnas(dgvDetalle);
                    frmDetalle.ClientSize = new Size(Math.Min(Math.Max(anchoNecesario + 20, 450), 1000), 300);

                    frmDetalle.Controls.Add(dgvDetalle);
                    frmDetalle.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el detalle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------------------------
        // EVENTOS RESTANTES (DISEÑO Y BOTONES ESTÁTICOS QUE QUERÍAS MANTENER)
        // -------------------------------------------------------------------------

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
                    dt.Columns.Add("Stock");
                    dt.Rows.Add("P001", "Alimento Balanceado 15kg", "12");
                    break;
                case "stock":
                    dt.Columns.Add("Codigo");
                    dt.Columns.Add("Descripcion");
                    dt.Columns.Add("Stock");
                    dt.Rows.Add("P002", "Rascador para Gatos", "4");
                    break;
                case "vendedores":
                    dt.Columns.Add("Vendedor");
                    dt.Columns.Add("CantidadVentas");
                    dt.Rows.Add("Carlos Gomez", "15");
                    break;
            }

            HacerVisibleGrilla();
            dataGridReportes.AutoGenerateColumns = true;
            dataGridReportes.DataSource = dt;
            AjustarColumnas(dataGridReportes);
            dataGridReportes.ReadOnly = true;
            dataGridReportes.AllowUserToAddRows = false;
            dataGridReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridReportes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            dataGridReportes.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void botonProductos_Click(object sender, EventArgs e) { ConfigurarGrilla("productos"); }
        private void botonStockMin_Click(object sender, EventArgs e) { ConfigurarGrilla("stock"); }
        private void botonVendedor_Click(object sender, EventArgs e) { ConfigurarGrilla("vendedores"); }
        private void botonVentas_Click(object sender, EventArgs e) { btnBuscar_Click(sender, e); }
    }
}