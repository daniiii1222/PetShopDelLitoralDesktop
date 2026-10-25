using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using CapaDatos;

namespace PetShopDelLitoral
{
    public partial class FrmBackup : Form
    {
        public FrmBackup()
        {
            InitializeComponent();
            ConfigurarEscaladoYEstilos();
            this.Activated += (s, e) => CargarHistorialBackups();
            this.Shown += (s, e) => CargarHistorialBackups();
        }

        private string ObtenerCarpetaBackups()
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            return ruta;
        }

        private void ConfigurarEscaladoYEstilos()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(254, 249, 231);

            ConstruirLayoutResponsivo();
        }

        private void ConstruirLayoutResponsivo()
        {
            this.Controls.Clear();

            // 1. Barra de título superior
            Panel panelTitulo = new Panel();
            panelTitulo.Name = "panelTituloSuperior";
            panelTitulo.Dock = DockStyle.Top;
            panelTitulo.Height = 52;
            panelTitulo.BackColor = Color.FromArgb(33, 24, 20);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Copia de Seguridad (Backup)";
            lblTitulo.ForeColor = Color.FromArgb(229, 184, 105);
            try { lblTitulo.Font = new Font("Poppins", 14F, FontStyle.Bold); }
            catch { lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold); }
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(22, 12);
            panelTitulo.Controls.Add(lblTitulo);

            // 2. Contenedor principal responsive
            Panel panelPrincipal = new Panel();
            panelPrincipal.Name = "panelPrincipalBackup";
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.AutoScroll = true;
            panelPrincipal.Padding = new Padding(24, 16, 24, 20);
            panelPrincipal.BackColor = Color.FromArgb(254, 249, 231);

            // --- Fila Superior: Tarjetas de Accion (50% / 50%) ---
            TableLayoutPanel tlpTarjetasSuperiores = new TableLayoutPanel();
            tlpTarjetasSuperiores.Dock = DockStyle.Top;
            tlpTarjetasSuperiores.Height = 135;
            tlpTarjetasSuperiores.ColumnCount = 2;
            tlpTarjetasSuperiores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetasSuperiores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetasSuperiores.RowCount = 1;
            tlpTarjetasSuperiores.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTarjetasSuperiores.Margin = new Padding(0);

            // Tarjeta 1: Respaldo Manual
            panel3.Dock = DockStyle.Fill;
            panel3.Margin = new Padding(0, 0, 10, 0);
            panel3.BackColor = Color.White;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Clear();

            pictureBox1.Size = new Size(80, 80);
            pictureBox1.Location = new Point(18, 26);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            panel3.Controls.Add(pictureBox1);

            label3.Text = "RESPALDO MANUAL";
            label3.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(33, 24, 20);
            label3.Location = new Point(110, 20);
            label3.AutoSize = true;
            panel3.Controls.Add(label3);

            Label lblSub1 = new Label();
            lblSub1.Text = "Crear una copia de seguridad completa de MySQL.";
            lblSub1.Font = new Font("Segoe UI", 8.5F);
            lblSub1.ForeColor = Color.FromArgb(115, 100, 90);
            lblSub1.Location = new Point(110, 45);
            lblSub1.AutoSize = true;
            panel3.Controls.Add(lblSub1);

            btnGenerarBackup.Text = "RESPALDAR BD";
            btnGenerarBackup.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGenerarBackup.BackColor = Color.FromArgb(232, 157, 66);
            btnGenerarBackup.ForeColor = Color.White;
            btnGenerarBackup.FlatStyle = FlatStyle.Flat;
            btnGenerarBackup.FlatAppearance.BorderSize = 0;
            btnGenerarBackup.Size = new Size(160, 36);
            btnGenerarBackup.Location = new Point(110, 75);
            panel3.Controls.Add(btnGenerarBackup);

            tlpTarjetasSuperiores.Controls.Add(panel3, 0, 0);

            // Tarjeta 2: Estado de Respaldo
            panel6.Dock = DockStyle.Fill;
            panel6.Margin = new Padding(10, 0, 0, 0);
            panel6.BackColor = Color.White;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Clear();

            pictureBox2.Size = new Size(80, 80);
            pictureBox2.Location = new Point(18, 26);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            panel6.Controls.Add(pictureBox2);

            label5.Text = "ESTADO DE COPIAS";
            label5.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(33, 24, 20);
            label5.Location = new Point(110, 20);
            label5.AutoSize = true;
            panel6.Controls.Add(label5);

            Label lblSub2 = new Label();
            lblSub2.Text = "Información y registro de la última copia realizada.";
            lblSub2.Font = new Font("Segoe UI", 8.5F);
            lblSub2.ForeColor = Color.FromArgb(115, 100, 90);
            lblSub2.Location = new Point(110, 45);
            lblSub2.AutoSize = true;
            panel6.Controls.Add(lblSub2);

            button1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button1.BackColor = Color.FromArgb(232, 157, 66);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Size = new Size(210, 36);
            button1.Location = new Point(110, 75);
            panel6.Controls.Add(button1);

            tlpTarjetasSuperiores.Controls.Add(panel6, 1, 0);

            // --- Encabezado del Historial ---
            Panel panelTituloGrilla = new Panel();
            panelTituloGrilla.Dock = DockStyle.Top;
            panelTituloGrilla.Height = 40;
            panelTituloGrilla.BackColor = Color.Transparent;

            Label lblTituloGrilla = new Label();
            lblTituloGrilla.Text = "HISTORIAL DE COPIAS DE SEGURIDAD";
            lblTituloGrilla.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloGrilla.ForeColor = Color.FromArgb(33, 24, 20);
            lblTituloGrilla.Location = new Point(0, 16);
            lblTituloGrilla.AutoSize = true;
            panelTituloGrilla.Controls.Add(lblTituloGrilla);

            // --- Grilla de Historial ---
            dgvHistorial.Dock = DockStyle.Top;
            dgvHistorial.Height = 220;
            dgvHistorial.Margin = new Padding(0);

            // --- Fila Inferior: Estadísticas (50% / 50%) ---
            TableLayoutPanel tlpTarjetasInferiores = new TableLayoutPanel();
            tlpTarjetasInferiores.Dock = DockStyle.Top;
            tlpTarjetasInferiores.Height = 100;
            tlpTarjetasInferiores.ColumnCount = 2;
            tlpTarjetasInferiores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetasInferiores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTarjetasInferiores.RowCount = 1;
            tlpTarjetasInferiores.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTarjetasInferiores.Margin = new Padding(0);

            // panel5 (Backups Totales)
            panel5.Dock = DockStyle.Fill;
            panel5.Margin = new Padding(0, 16, 10, 0);
            panel5.BackColor = Color.White;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Clear();

            label6.Text = "BACKUPS TOTALES";
            label6.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(120, 105, 95);
            label6.Location = new Point(20, 12);
            label6.AutoSize = true;
            panel5.Controls.Add(label6);

            lblTotalBackups.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalBackups.ForeColor = Color.FromArgb(33, 24, 20);
            lblTotalBackups.Location = new Point(20, 36);
            lblTotalBackups.AutoSize = true;
            panel5.Controls.Add(lblTotalBackups);

            tlpTarjetasInferiores.Controls.Add(panel5, 0, 0);

            // panel7 (Espacio Utilizado)
            panel7.Dock = DockStyle.Fill;
            panel7.Margin = new Padding(10, 16, 0, 0);
            panel7.BackColor = Color.White;
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel7.Controls.Clear();

            label7.Text = "ESPACIO UTILIZADO";
            label7.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(120, 105, 95);
            label7.Location = new Point(20, 12);
            label7.AutoSize = true;
            panel7.Controls.Add(label7);

            lblEspacioUtilizado.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblEspacioUtilizado.ForeColor = Color.FromArgb(33, 24, 20);
            lblEspacioUtilizado.Location = new Point(20, 36);
            lblEspacioUtilizado.AutoSize = true;
            panel7.Controls.Add(lblEspacioUtilizado);

            tlpTarjetasInferiores.Controls.Add(panel7, 1, 0);

            // --- Barra de Ubicación de Archivos al fondo ---
            Panel panelRuta = new Panel();
            panelRuta.Dock = DockStyle.Top;
            panelRuta.Height = 65;
            panelRuta.BackColor = Color.Transparent;

            label8.Text = "UBICACIÓN DE ARCHIVOS:";
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(33, 24, 20);
            label8.Location = new Point(0, 22);
            label8.AutoSize = true;
            panelRuta.Controls.Add(label8);

            txtRuta.Location = new Point(180, 18);
            txtRuta.Size = new Size(panelPrincipal.ClientSize.Width - 340, 26);
            txtRuta.Font = new Font("Segoe UI", 9.5F);
            txtRuta.ReadOnly = true;
            txtRuta.BackColor = Color.White;
            txtRuta.ForeColor = Color.FromArgb(40, 40, 40);
            txtRuta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRuta.Text = ObtenerCarpetaBackups();
            panelRuta.Controls.Add(txtRuta);

            Button btnAbrirCarpeta = new Button();
            btnAbrirCarpeta.Text = "📁 Abrir Carpeta";
            btnAbrirCarpeta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAbrirCarpeta.BackColor = Color.FromArgb(33, 24, 20);
            btnAbrirCarpeta.ForeColor = Color.FromArgb(229, 184, 105);
            btnAbrirCarpeta.FlatStyle = FlatStyle.Flat;
            btnAbrirCarpeta.FlatAppearance.BorderSize = 0;
            btnAbrirCarpeta.Size = new Size(130, 28);
            btnAbrirCarpeta.Location = new Point(panelPrincipal.ClientSize.Width - 150, 17);
            btnAbrirCarpeta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAbrirCarpeta.Click += (s, ev) =>
            {
                try
                {
                    string c = ObtenerCarpetaBackups();
                    if (Directory.Exists(c)) System.Diagnostics.Process.Start("explorer.exe", c);
                }
                catch { }
            };
            panelRuta.Controls.Add(btnAbrirCarpeta);

            // Agregar al contenedor principal en orden inverso de DockStyle.Top
            panelPrincipal.Controls.Add(panelRuta);
            panelPrincipal.Controls.Add(tlpTarjetasInferiores);
            panelPrincipal.Controls.Add(dgvHistorial);
            panelPrincipal.Controls.Add(panelTituloGrilla);
            panelPrincipal.Controls.Add(tlpTarjetasSuperiores);

            this.Controls.Add(panelPrincipal);
            this.Controls.Add(panelTitulo);
        }

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            CargarHistorialBackups();
        }

        private DataGridView ObtenerTablaBackups()
        {
            return dgvHistorial 
                ?? this.Controls.OfType<DataGridView>().FirstOrDefault()
                ?? this.Controls.OfType<Control>().SelectMany(c => c.Controls.OfType<DataGridView>()).FirstOrDefault();
        }

        private void CargarHistorialBackups()
        {
            try
            {
                DataGridView tabla = ObtenerTablaBackups();

                if (tabla != null)
                {
                    tabla.Columns.Clear();
                    tabla.CellClick -= Tabla_CellClick;

                    tabla.BackgroundColor = Color.White;
                    tabla.BorderStyle = BorderStyle.None;
                    tabla.EnableHeadersVisualStyles = false;
                    tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(229, 184, 105);
                    tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 24, 20);
                    tabla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 184, 105);
                    tabla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 24, 20);
                    tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    tabla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    tabla.ColumnHeadersHeight = 36;
                    tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                    tabla.DefaultCellStyle.BackColor = Color.White;
                    tabla.DefaultCellStyle.ForeColor = Color.FromArgb(33, 24, 20);
                    tabla.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    tabla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 225, 175);
                    tabla.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 24, 20);
                    tabla.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    tabla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(253, 250, 242);
                    tabla.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(33, 24, 20);
                    tabla.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 225, 175);
                    tabla.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 24, 20);
                    tabla.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    tabla.RowTemplate.Height = 32;
                    tabla.GridColor = Color.FromArgb(235, 225, 205);
                    tabla.RowHeadersVisible = false;
                    tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("FECHA");
                    dt.Columns.Add("NOMBRE DE ARCHIVO");
                    dt.Columns.Add("TAMAÑO");
                    dt.Columns.Add("ESTADO");
                    dt.Columns.Add("NOMBRE_REAL");

                    string carpeta = ObtenerCarpetaBackups();
                    DateTime? ultimaFecha = null;
                    long pesoTotalBytes = 0;
                    int totalArchivos = 0;

                    if (Directory.Exists(carpeta))
                    {
                        string[] archivos = Directory.GetFiles(carpeta, "*.sql");
                        totalArchivos = archivos.Length;

                        foreach (string archivo in archivos)
                        {
                            FileInfo info = new FileInfo(archivo);
                            pesoTotalBytes += info.Length;

                            dt.Rows.Add(
                                info.LastWriteTime.ToString("yyyy-MM-dd HH:mm"),
                                info.Name,
                                $"{Math.Round((double)info.Length / 1024 / 1024, 2)} MB",
                                "Completado",
                                info.Name
                            );

                            if (ultimaFecha == null || info.LastWriteTime > ultimaFecha)
                            {
                                ultimaFecha = info.LastWriteTime;
                            }
                        }
                    }

                    tabla.DataSource = dt;

                    if (tabla.Columns.Contains("NOMBRE_REAL"))
                    {
                        tabla.Columns["NOMBRE_REAL"].Visible = false;
                    }

                    // Columna Exportar
                    DataGridViewButtonColumn colDescargar = new DataGridViewButtonColumn();
                    colDescargar.Name = "colDescargar";
                    colDescargar.HeaderText = "ACCIONES";
                    colDescargar.Text = "Exportar";
                    colDescargar.UseColumnTextForButtonValue = true;
                    colDescargar.FlatStyle = FlatStyle.Flat;
                    colDescargar.DefaultCellStyle.BackColor = Color.FromArgb(232, 157, 66);
                    colDescargar.DefaultCellStyle.ForeColor = Color.White;
                    colDescargar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 140, 50);
                    colDescargar.DefaultCellStyle.SelectionForeColor = Color.White;
                    colDescargar.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    tabla.Columns.Add(colDescargar);

                    // Columna Eliminar
                    DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn();
                    colEliminar.Name = "colEliminar";
                    colEliminar.HeaderText = "";
                    colEliminar.Text = "Eliminar";
                    colEliminar.UseColumnTextForButtonValue = true;
                    colEliminar.FlatStyle = FlatStyle.Flat;
                    colEliminar.DefaultCellStyle.BackColor = Color.FromArgb(215, 85, 65);
                    colEliminar.DefaultCellStyle.ForeColor = Color.White;
                    colEliminar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(190, 70, 50);
                    colEliminar.DefaultCellStyle.SelectionForeColor = Color.White;
                    colEliminar.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    tabla.Columns.Add(colEliminar);

                    tabla.CellClick += Tabla_CellClick;

                    ActualizarResumen(totalArchivos, pesoTotalBytes, ultimaFecha);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarResumen(int total, long bytes, DateTime? ultimaFecha)
        {
            if (lblTotalBackups != null) lblTotalBackups.Text = total.ToString();

            Control lblTotales = this.Controls.Find("lblBackupsTotales", true).FirstOrDefault();
            if (lblTotales != null) lblTotales.Text = total.ToString();

            if (lblEspacioUtilizado != null) lblEspacioUtilizado.Text = $"{Math.Round((double)bytes / 1024 / 1024, 2)} MB";

            string textoHora = ultimaFecha.HasValue
                ? $"ÚLTIMA COPIA: {ultimaFecha.Value:dd/MM HH:mm}"
                : "SIN COPIAS PREVIAS";

            if (button1 != null) button1.Text = textoHora;

            Button btnHora = GetAllControls(this).OfType<Button>().FirstOrDefault(b => b.Name == "button1" || b.Name == "btnAjustarHora" || b.Name == "btnConfigurarHora" || b.Text.Contains("AJUSTAR") || b.Text.Contains("ÚLTIMA"));
            if (btnHora != null) btnHora.Text = textoHora;

            if (txtRuta != null) txtRuta.Text = ObtenerCarpetaBackups();
        }

        private System.Collections.Generic.IEnumerable<Control> GetAllControls(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl)).Concat(controls);
        }

        private void Tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView tabla = sender as DataGridView;
            if (e.RowIndex < 0) return;

            string colName = tabla.Columns[e.ColumnIndex].Name;

            string nombreArchivo = tabla.Rows[e.RowIndex].Cells["NOMBRE_REAL"].Value?.ToString()
                                ?? tabla.Rows[e.RowIndex].Cells["NOMBRE DE ARCHIVO"].Value?.ToString();

            if (string.IsNullOrEmpty(nombreArchivo)) return;

            string rutaOrigen = Path.Combine(ObtenerCarpetaBackups(), nombreArchivo);

            if (colName == "colDescargar")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = nombreArchivo;
                sfd.Filter = "Archivo SQL (*.sql)|*.sql";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(rutaOrigen, sfd.FileName, true);
                        MessageBox.Show("Copia exportada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (colName == "colEliminar")
            {
                DialogResult res = MessageBox.Show($"¿Desea eliminar '{nombreArchivo}'?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(rutaOrigen))
                        {
                            File.Delete(rutaOrigen);
                            MessageBox.Show("Archivo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarHistorialBackups();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void EjecutarRespaldoReal()
        {
            try
            {
                string carpeta = ObtenerCarpetaBackups();
                string nombreArchivo = $"petshop_db_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                string rutaDestino = Path.Combine(carpeta, nombreArchivo);

                using (MySqlConnection conn = ClsDataBase.CrearConexion())
                {
                    conn.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"-- Backup {DateTime.Now}");

                    DataTable dtTables = new DataTable();
                    using (MySqlDataAdapter da = new MySqlDataAdapter("SHOW TABLES", conn))
                    {
                        da.Fill(dtTables);
                    }

                    foreach (DataRow row in dtTables.Rows)
                    {
                        string tableName = row[0].ToString();

                        DataTable dtCreate = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter($"SHOW CREATE TABLE `{tableName}`", conn))
                        {
                            da.Fill(dtCreate);
                        }

                        sb.AppendLine($"DROP TABLE IF EXISTS `{tableName}`;");
                        sb.AppendLine(dtCreate.Rows[0]["Create Table"].ToString() + ";\n");

                        DataTable dtData = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter($"SELECT * FROM `{tableName}`", conn))
                        {
                            da.Fill(dtData);
                        }

                        foreach (DataRow dataRow in dtData.Rows)
                        {
                            sb.Append($"INSERT INTO `{tableName}` VALUES (");
                            for (int i = 0; i < dtData.Columns.Count; i++)
                            {
                                object val = dataRow[i];
                                if (val == DBNull.Value)
                                {
                                    sb.Append("NULL");
                                }
                                else
                                {
                                    string valStr = val.ToString().Replace("\\", "\\\\").Replace("'", "\\'");
                                    sb.Append($"'{valStr}'");
                                }
                                if (i < dtData.Columns.Count - 1) sb.Append(", ");
                            }
                            sb.AppendLine(");");
                        }
                        sb.AppendLine();
                    }

                    // --- Backup de Stored Procedures ---
                    sb.AppendLine();
                    sb.AppendLine("-- ================================");
                    sb.AppendLine("-- STORED PROCEDURES");
                    sb.AppendLine("-- ================================");
                    sb.AppendLine("DELIMITER $$");
                    sb.AppendLine();

                    DataTable dtProcedures = new DataTable();
                    using (MySqlCommand cmdProcs = new MySqlCommand("SHOW PROCEDURE STATUS WHERE Db = @db", conn))
                    {
                        cmdProcs.Parameters.AddWithValue("@db", conn.Database);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmdProcs))
                        {
                            da.Fill(dtProcedures);
                        }
                    }

                    foreach (DataRow procRow in dtProcedures.Rows)
                    {
                        string nombreProc = procRow["Name"].ToString();

                        DataTable dtCreateProc = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter($"SHOW CREATE PROCEDURE `{nombreProc}`", conn))
                        {
                            da.Fill(dtCreateProc);
                        }

                        if (dtCreateProc.Rows.Count > 0)
                        {
                            string createStatement = dtCreateProc.Rows[0]["Create Procedure"].ToString();

                            sb.AppendLine($"DROP PROCEDURE IF EXISTS `{nombreProc}`$$");
                            sb.AppendLine(createStatement + "$$");
                            sb.AppendLine();
                        }
                    }

                    sb.AppendLine("DELIMITER ;");

                    File.WriteAllText(rutaDestino, sb.ToString(), Encoding.UTF8);
                }

                MessageBox.Show("Copia realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarHistorialBackups();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al respaldar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarBackup_Click(object sender, EventArgs e) => EjecutarRespaldoReal();
        private void btnRespaldar_Click(object sender, EventArgs e) => EjecutarRespaldoReal();

        private void btnAjustarHora_Click(object sender, EventArgs e) => CargarHistorialBackups();
        private void lblEspacioUtilizado_Click(object sender, EventArgs e) { }

        private void FrmBackup_Load_1(object sender, EventArgs e)
        {
            CargarHistorialBackups();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
