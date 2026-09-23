using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PetShopDelLitoral
{
    public partial class FrmBackup : Form
    {
        private readonly string connectionString = "Server=localhost;Database=petshopdellitoral;Uid=root;Pwd=;";

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
            this.BackColor = Color.FromArgb(255, 253, 231);

            if (!this.Controls.OfType<Panel>().Any(p => p.Name == "panelTituloSuperior"))
            {
                Panel panelTitulo = new Panel();
                panelTitulo.Name = "panelTituloSuperior";
                panelTitulo.Dock = DockStyle.Top;
                panelTitulo.Height = 50;
                panelTitulo.BackColor = Color.FromArgb(33, 24, 20);

                Label lblTitulo = new Label();
                lblTitulo.Text = "Copia de Seguridad (Backup)";
                lblTitulo.ForeColor = Color.FromArgb(229, 184, 105);
                lblTitulo.Font = new Font("Poppins", 14F, FontStyle.Bold);
                lblTitulo.AutoSize = true;
                lblTitulo.Location = new Point(20, 12);

                panelTitulo.Controls.Add(lblTitulo);
                this.Controls.Add(panelTitulo);
                panelTitulo.BringToFront();
            }
        }

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            CargarHistorialBackups();
        }

        private DataGridView ObtenerTablaBackups()
        {
            return this.Controls.OfType<DataGridView>().FirstOrDefault()
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
                    tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
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
                    colDescargar.DefaultCellStyle.BackColor = Color.FromArgb(230, 160, 60);
                    colDescargar.DefaultCellStyle.ForeColor = Color.White;
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
            Control lblTotales = this.Controls.Find("lblBackupsTotales", true).FirstOrDefault();
            if (lblTotales != null) lblTotales.Text = total.ToString();

            Control lblEspacio = this.Controls.Find("lblEspacioUtilizado", true).FirstOrDefault();
            if (lblEspacio != null) lblEspacio.Text = $"{Math.Round((double)bytes / 1024 / 1024, 2)} MB";

            string textoHora = ultimaFecha.HasValue
                ? $"ÚLTIMA COPIA: {ultimaFecha.Value:dd/MM HH:mm}"
                : "SIN COPIAS PREVIAS";

            Button btnHora = GetAllControls(this).OfType<Button>().FirstOrDefault(b => b.Name == "btnAjustarHora" || b.Name == "btnConfigurarHora" || b.Text.Contains("AJUSTAR") || b.Text.Contains("ÚLTIMA"));

            if (btnHora != null)
            {
                btnHora.Text = textoHora;
            }
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

                using (MySqlConnection conn = new MySqlConnection(connectionString))
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

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}