using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Lbl = System.Windows.Forms.Label;

namespace PetShopDelLitoral
{
    // Reportes estadísticos (gráficos) + tabla cruzada.
    // Usa la misma consulta que la pantalla de reportes (CN_Reporte.ObtenerReporteVentas),
    // así que no necesita procedimientos almacenados ni capas nuevas.
    public class FrmEstadisticas : Form
    {
        private static readonly CultureInfo ar = new CultureInfo("es-AR");
        private static readonly Color Oscuro = Color.FromArgb(33, 24, 21);
        private static readonly Color Dorado = Color.FromArgb(255, 204, 112);
        private static readonly Color Crema = Color.FromArgb(255, 255, 192);
        private static readonly Color Gris = Color.FromArgb(190, 190, 190);
        private static readonly Color Verde = Color.FromArgb(120, 220, 120);
        private static readonly Color Rojo = Color.FromArgb(255, 125, 115);
        private static readonly Color[] Paleta =
        {
            Color.FromArgb(255, 204, 112), Color.FromArgb(200, 120, 40), Color.FromArgb(140, 90, 60),
            Color.FromArgb(230, 170, 60), Color.FromArgb(90, 60, 50), Color.FromArgb(250, 225, 170)
        };

        private class VentaFila
        {
            public int Id;
            public DateTime Fecha;
            public string Cliente, Vendedor, Pago;
            public decimal Total;
        }

        private List<VentaFila> ventas = new List<VentaFila>();
        private List<VentaFila> ventasAnt = new List<VentaFila>();          // período anterior (misma duración)
        private DateTime antDesde, antHasta;
        private Dictionary<string, decimal> unidades = new Dictionary<string, decimal>();   // producto -> unidades vendidas
        private System.Windows.Forms.ToolTip tip = new System.Windows.Forms.ToolTip();

        private DateTimePicker dtpDesde, dtpHasta;
        private Button btnActualizar;
        private Lbl kpiVentas, kpiTotal, kpiTicket, kpiProducto;
        private Lbl detVentas, detTotal, detTicket, detProducto;
        private Chart chartTiempo, chartPago, chartVendedores, chartProductos;
        private ComboBox cboFilas, cboValores;
        private DataGridView gridCruzada;

        public FrmEstadisticas()
        {
            Text = "Reportes estadísticos";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1150, 760);
            MinimumSize = new Size(900, 600);
            BackColor = Crema;
            Font = new Font("Segoe UI", 9.5f);

            // ---- barra superior: filtros ----
            FlowLayoutPanel header = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Oscuro,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(12, 12, 12, 0)
            };
            Lbl l1 = new Lbl { Text = "Desde", ForeColor = Dorado, AutoSize = true, Margin = new Padding(0, 6, 6, 0) };
            dtpDesde = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Width = 130,
                MaxDate = DateTime.Today,
                Margin = new Padding(0, 3, 24, 0)
            };
            Lbl l2 = new Lbl { Text = "Hasta", ForeColor = Dorado, AutoSize = true, Margin = new Padding(0, 6, 6, 0) };
            dtpHasta = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Width = 130,
                MaxDate = DateTime.Today,
                Margin = new Padding(0, 3, 24, 0)
            };
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
            btnActualizar = new Button
            {
                Text = "Actualizar",
                Size = new Size(120, 30),
                Margin = new Padding(0, 0, 0, 0),
                FlatStyle = FlatStyle.Flat,
                BackColor = Dorado,
                ForeColor = Oscuro
            };
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Click += (s, e) => Cargar();
            header.Controls.AddRange(new Control[] { l1, dtpDesde, l2, dtpHasta, btnActualizar });

            // ---- tarjetas con los totales ----
            TableLayoutPanel kpis = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 108,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(8, 8, 8, 4)
            };
            for (int i = 0; i < 4; i++) kpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            kpis.Controls.Add(Tarjeta("Ventas realizadas", out kpiVentas, out detVentas), 0, 0);
            kpis.Controls.Add(Tarjeta("Total vendido", out kpiTotal, out detTotal), 1, 0);
            kpis.Controls.Add(Tarjeta("Ticket promedio", out kpiTicket, out detTicket), 2, 0);
            kpis.Controls.Add(Tarjeta("Producto más vendido", out kpiProducto, out detProducto), 3, 0);

            // ---- pestañas ----
            TabControl tabs = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10f) };
            TabPage pgGraficos = new TabPage("Gráficos") { BackColor = Crema };
            TabPage pgCruzada = new TabPage("Tabla cruzada") { BackColor = Crema };
            tabs.TabPages.Add(pgGraficos);
            tabs.TabPages.Add(pgCruzada);

            // gráficos en cuadrícula 2 x 2
            TableLayoutPanel grid = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2 };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            chartTiempo = CrearChart("Ventas por día ($)", SeriesChartType.Column, false);
            chartPago = CrearChart("Ventas por método de pago", SeriesChartType.Doughnut, true);
            chartVendedores = CrearChart("Top 5 vendedores ($)", SeriesChartType.Bar, false);
            chartProductos = CrearChart("Top 5 productos más vendidos (unidades)", SeriesChartType.Bar, false);
            grid.Controls.Add(chartTiempo, 0, 0);
            grid.Controls.Add(chartPago, 1, 0);
            grid.Controls.Add(chartVendedores, 0, 1);
            grid.Controls.Add(chartProductos, 1, 1);
            pgGraficos.Controls.Add(grid);

            // tabla cruzada: selector arriba + grilla
            FlowLayoutPanel opciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 48,
                WrapContents = false,
                Padding = new Padding(8, 10, 8, 0)
            };
            Lbl lf = new Lbl { Text = "Filas:", AutoSize = true, Margin = new Padding(0, 6, 4, 0) };
            cboFilas = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150, Margin = new Padding(0, 2, 28, 0) };
            cboFilas.Items.AddRange(new object[] { "Vendedor", "Cliente", "Mes" });
            cboFilas.SelectedIndex = 0;
            Lbl lc = new Lbl { Text = "Columnas: Método de pago", AutoSize = true, Margin = new Padding(0, 6, 28, 0) };
            Lbl lv = new Lbl { Text = "Valores:", AutoSize = true, Margin = new Padding(0, 6, 4, 0) };
            cboValores = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 210, Margin = new Padding(0, 2, 0, 0) };
            cboValores.Items.AddRange(new object[] { "Monto vendido ($)", "Cantidad de ventas" });
            cboValores.SelectedIndex = 0;
            cboFilas.SelectedIndexChanged += (s, e) => LlenarCruzada();
            cboValores.SelectedIndexChanged += (s, e) => LlenarCruzada();
            opciones.Controls.AddRange(new Control[] { lf, cboFilas, lc, lv, cboValores });

            gridCruzada = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                EnableHeadersVisualStyles = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridCruzada.ColumnHeadersDefaultCellStyle.BackColor = Oscuro;
            gridCruzada.ColumnHeadersDefaultCellStyle.ForeColor = Dorado;
            gridCruzada.ColumnHeadersDefaultCellStyle.SelectionBackColor = Oscuro;   // evita el encabezado azul
            gridCruzada.ColumnHeadersDefaultCellStyle.SelectionForeColor = Dorado;
            gridCruzada.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCruzada.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            gridCruzada.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridCruzada.ColumnHeadersHeight = 34;
            gridCruzada.RowTemplate.Height = 28;
            gridCruzada.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 185, 110);
            gridCruzada.DefaultCellStyle.SelectionForeColor = Color.Black;
            pgCruzada.Controls.Add(gridCruzada);   // Fill primero
            pgCruzada.Controls.Add(opciones);      // Top después

            // El de Fill se agrega primero; los Top después (el último queda arriba de todo)
            Controls.Add(tabs);
            Controls.Add(kpis);
            Controls.Add(header);

            Load += (s, e) => Cargar();   // Load (y no Shown) para que ande también embebido en el panel central
        }

        // ---------- armado de controles ----------
        private Panel Tarjeta(string titulo, out Lbl valor, out Lbl detalle)
        {
            AnchorStyles ancla = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Panel p = new Panel { Dock = DockStyle.Fill, BackColor = Oscuro, Margin = new Padding(4) };
            Lbl t = new Lbl { Text = titulo, ForeColor = Dorado, AutoSize = true, Location = new Point(12, 6) };
            valor = new Lbl
            {
                Text = "-",
                ForeColor = Color.White,
                AutoSize = false,
                AutoEllipsis = true,
                Size = new Size(176, 34),
                Location = new Point(12, 28),
                Anchor = ancla,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold)
            };
            detalle = new Lbl
            {
                Text = " ",
                ForeColor = Gris,
                AutoSize = false,
                AutoEllipsis = true,
                Size = new Size(176, 22),
                Location = new Point(12, 66),
                Anchor = ancla,
                Font = new Font("Segoe UI", 9f)
            };
            p.Controls.Add(t);
            p.Controls.Add(valor);
            p.Controls.Add(detalle);
            return p;
        }

        private Chart CrearChart(string titulo, SeriesChartType tipo, bool conLeyenda)
        {
            Chart ch = new Chart { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(4) };
            ChartArea area = new ChartArea("a");
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.LabelStyle.Format = "N0";
            if (tipo == SeriesChartType.Bar) area.AxisX.IsReversed = true;   // el mayor arriba
            ch.ChartAreas.Add(area);
            ch.Titles.Add(new Title(titulo, Docking.Top, new Font("Segoe UI", 11f, FontStyle.Bold), Oscuro));

            Series s = new Series("s") { ChartType = tipo, ChartArea = "a", Color = Dorado, BorderColor = Oscuro };
            if (tipo != SeriesChartType.Doughnut)
            {
                s.IsValueShownAsLabel = true;
                s.LabelFormat = "N0";
            }
            ch.Series.Add(s);

            if (conLeyenda) ch.Legends.Add(new Legend("l") { Docking = Docking.Right });
            return ch;
        }

        // ---------- datos ----------
        private void Cargar()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime d = dtpDesde.Value.Date, h = dtpHasta.Value.Date;
                if (d > h)
                {
                    MessageBox.Show("La fecha 'Desde' no puede ser posterior a 'Hasta'.", "Estadísticas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CN_Reporte cn = new CN_Reporte();
                ventas = Convertir(cn.ObtenerReporteVentas(d, h, 0));   // 0 = todos los vendedores

                // Período anterior: misma cantidad de días, inmediatamente antes del elegido
                int dias = (int)(h - d).TotalDays + 1;
                antHasta = d.AddDays(-1);
                antDesde = antHasta.AddDays(-(dias - 1));
                ventasAnt = Convertir(cn.ObtenerReporteVentas(antDesde, antHasta, 0));

                // Productos más vendidos: una sola consulta (procedimiento sp_ObtenerProductosMasVendidos)
                unidades = new Dictionary<string, decimal>();
                foreach (DataRow r in cn.ObtenerProductosMasVendidos(d, h, 5).Rows)
                    unidades[Convert.ToString(r["Producto"])] = Convert.ToDecimal(r["Unidades"]);

                LlenarKpis();
                LlenarGraficos();
                LlenarCruzada();

                if (ventas.Count == 0)
                    MessageBox.Show("No hay ventas en el período elegido.", "Estadísticas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar las estadísticas: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // Las columnas se identifican por nombre (fecha, cliente, vendedor, pago, total)
        private static string Col(DataTable t, params string[] claves)
        {
            foreach (string k in claves)
                foreach (DataColumn c in t.Columns)
                    if (c.ColumnName.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
            return null;
        }

        private static string Texto(DataRow r, string col)
        {
            if (col == null || r[col] is DBNull) return "(sin dato)";
            string s = Convert.ToString(r[col]);
            return string.IsNullOrWhiteSpace(s) ? "(sin dato)" : s.Trim();
        }

        private static decimal Numero(object v)
        {
            if (v == null || v is DBNull) return 0m;
            if (v is decimal) return (decimal)v;
            string s = Convert.ToString(v).Replace("$", "").Replace("%", "").Trim();
            decimal d;
            if (decimal.TryParse(s, NumberStyles.Number, ar, out d)) return d;
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d)) return d;
            return 0m;
        }

        private List<VentaFila> Convertir(DataTable dt)
        {
            List<VentaFila> lista = new List<VentaFila>();
            if (dt == null) return lista;

            string cFecha = Col(dt, "fecha"), cCli = Col(dt, "cliente"), cVend = Col(dt, "vendedor", "usuario"),
                   cPago = Col(dt, "pago", "metodo", "método"), cTotal = Col(dt, "total");

            // id de la venta: columna "N° Venta" (o la primera)
            DataColumn colId = dt.Columns.Count > 0 ? dt.Columns[0] : null;
            foreach (DataColumn c in dt.Columns)
            {
                string n = (c.ColumnName ?? "").Trim();
                if (n.StartsWith("N", StringComparison.OrdinalIgnoreCase) &&
                    n.EndsWith("Venta", StringComparison.OrdinalIgnoreCase)) { colId = c; break; }
            }

            foreach (DataRow r in dt.Rows)
            {
                DateTime f = DateTime.Today;
                if (cFecha != null && !(r[cFecha] is DBNull))
                {
                    if (r[cFecha] is DateTime) f = (DateTime)r[cFecha];
                    else DateTime.TryParse(Convert.ToString(r[cFecha]), ar, DateTimeStyles.None, out f);
                }
                lista.Add(new VentaFila
                {
                    Id = (colId == null || r[colId] is DBNull) ? 0 : Convert.ToInt32(r[colId]),
                    Fecha = f,
                    Cliente = Texto(r, cCli),
                    Vendedor = Texto(r, cVend),
                    Pago = Texto(r, cPago),
                    Total = cTotal == null ? 0m : Numero(r[cTotal])
                });
            }
            return lista;
        }

        // ---------- tarjetas ----------
        private void LlenarKpis()
        {
            int n = ventas.Count;
            decimal total = ventas.Sum(v => v.Total);
            decimal ticket = n == 0 ? 0m : total / n;

            int nA = ventasAnt.Count;
            decimal totalA = ventasAnt.Sum(v => v.Total);
            decimal ticketA = nA == 0 ? 0m : totalA / nA;

            kpiVentas.Text = n.ToString("N0", ar);
            kpiTotal.Text = "$ " + total.ToString("N2", ar);
            kpiTicket.Text = "$ " + ticket.ToString("N2", ar);

            MostrarVariacion(detVentas, n, nA);
            MostrarVariacion(detTotal, total, totalA);
            MostrarVariacion(detTicket, ticket, ticketA);

            // Al pasar el mouse por la variación se ve contra qué período se comparó
            string ant = "Período anterior: " + antDesde.ToString("dd/MM/yyyy") + " al " + antHasta.ToString("dd/MM/yyyy");
            tip.SetToolTip(detVentas, ant + "\n" + nA.ToString("N0", ar) + " ventas");
            tip.SetToolTip(detTotal, ant + "\n$ " + totalA.ToString("N2", ar));
            tip.SetToolTip(detTicket, ant + "\n$ " + ticketA.ToString("N2", ar));

            detProducto.ForeColor = Gris;
            if (unidades.Count == 0)
            {
                kpiProducto.Text = "-";
                detProducto.Text = "Sin ventas en el período";
            }
            else
            {
                var top = unidades.OrderByDescending(k => k.Value).First();
                kpiProducto.Text = top.Key;
                detProducto.Text = top.Value.ToString("N0", ar) + " unidades vendidas";
            }
        }

        // "▲ 12,5% vs. período anterior" en verde si sube, rojo si baja
        private static void MostrarVariacion(Lbl lbl, decimal actual, decimal anterior)
        {
            if (anterior == 0)
            {
                lbl.Text = "Sin ventas en el período anterior";
                lbl.ForeColor = Gris;
                return;
            }
            decimal pct = (actual - anterior) / anterior * 100m;
            string flecha = pct > 0 ? "▲" : pct < 0 ? "▼" : "=";
            lbl.Text = flecha + " " + Math.Abs(pct).ToString("N1", ar) + "% vs. período anterior";
            lbl.ForeColor = pct > 0 ? Verde : pct < 0 ? Rojo : Gris;
        }

        // ---------- gráficos ----------
        private void LlenarGraficos()
        {
            // 1) Evolución en el tiempo: por día si el período es corto, por mes si es largo
            bool porDia = (dtpHasta.Value.Date - dtpDesde.Value.Date).TotalDays <= 31;
            chartTiempo.Titles[0].Text = porDia ? "Ventas por día ($)" : "Ventas por mes ($)";
            Series st = chartTiempo.Series[0];
            st.Points.Clear();
            var porTiempo = ventas
                .GroupBy(v => porDia ? v.Fecha.Date : new DateTime(v.Fecha.Year, v.Fecha.Month, 1))
                .OrderBy(x => x.Key);
            foreach (var g in porTiempo)
                st.Points.AddXY(porDia ? g.Key.ToString("dd/MM") : g.Key.ToString("MM/yyyy"), (double)g.Sum(v => v.Total));

            // 2) Torta (dona) por método de pago
            Series sp = chartPago.Series[0];
            sp.Points.Clear();
            sp.Label = "#PERCENT{P0}";
            sp.LegendText = "#VALX";
            int i = 0;
            foreach (var g in ventas.GroupBy(v => v.Pago).OrderByDescending(x => x.Sum(v => v.Total)))
            {
                int idx = sp.Points.AddXY(g.Key, (double)g.Sum(v => v.Total));
                sp.Points[idx].Color = Paleta[i % Paleta.Length];
                i++;
            }

            // 3) y 4) Rankings
            CargarTop(chartVendedores, v => v.Vendedor, 5);
            Series sprod = chartProductos.Series[0];
            sprod.Points.Clear();
            foreach (var kv in unidades.OrderByDescending(k => k.Value).Take(5))
                sprod.Points.AddXY(kv.Key, (double)kv.Value);
        }

        private void CargarTop(Chart ch, Func<VentaFila, string> clave, int n)
        {
            Series s = ch.Series[0];
            s.Points.Clear();
            foreach (var g in ventas.GroupBy(clave).OrderByDescending(x => x.Sum(v => v.Total)).Take(n))
                s.Points.AddXY(g.Key, (double)g.Sum(v => v.Total));
        }

        // ---------- tabla cruzada ----------
        // Filas: vendedor / cliente / mes.  Columnas: método de pago.  Valor: monto o cantidad de ventas.
        private void LlenarCruzada()
        {
            if (gridCruzada == null) return;

            string filas = (string)cboFilas.SelectedItem;
            bool monto = cboValores.SelectedIndex == 0;

            if (ventas.Count == 0) { gridCruzada.DataSource = null; return; }

            Func<VentaFila, string> clave = v =>
                filas == "Vendedor" ? v.Vendedor : filas == "Cliente" ? v.Cliente : v.Fecha.ToString("yyyy-MM");

            List<string> metodos = ventas.Select(v => v.Pago).Distinct().OrderBy(x => x).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add(filas, typeof(string));
            List<string> nombresCol = new List<string>();
            foreach (string m in metodos)
            {
                string nombre = m;
                while (dt.Columns.Contains(nombre)) nombre += " ";   // evita nombres repetidos
                dt.Columns.Add(nombre, typeof(decimal));
                nombresCol.Add(nombre);
            }
            string colTotal = "Total";
            while (dt.Columns.Contains(colTotal)) colTotal += " ";
            dt.Columns.Add(colTotal, typeof(decimal));

            foreach (var g in ventas.GroupBy(clave).OrderBy(x => x.Key))
            {
                DataRow fila = dt.NewRow();
                fila[0] = g.Key;
                decimal totalFila = 0m;
                for (int i = 0; i < metodos.Count; i++)
                {
                    string m = metodos[i];
                    decimal val = monto ? g.Where(v => v.Pago == m).Sum(v => v.Total)
                                        : g.Count(v => v.Pago == m);
                    fila[i + 1] = val;
                    totalFila += val;
                }
                fila[dt.Columns.Count - 1] = totalFila;
                dt.Rows.Add(fila);
            }

            // fila de totales por columna
            DataRow ft = dt.NewRow();
            ft[0] = "TOTAL";
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                decimal suma = 0m;
                foreach (DataRow r in dt.Rows) suma += (decimal)r[j];
                ft[j] = suma;
            }
            dt.Rows.Add(ft);

            gridCruzada.DataSource = dt;
            foreach (DataGridViewColumn c in gridCruzada.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;   // para que TOTAL quede siempre al final
                if (c.Index > 0)
                {
                    c.DefaultCellStyle.Format = monto ? "N2" : "N0";
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            gridCruzada.Columns[gridCruzada.Columns.Count - 1].DefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
            gridCruzada.Rows[gridCruzada.Rows.Count - 1].DefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
            gridCruzada.Rows[gridCruzada.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 200);
            gridCruzada.ClearSelection();
        }
    }
}