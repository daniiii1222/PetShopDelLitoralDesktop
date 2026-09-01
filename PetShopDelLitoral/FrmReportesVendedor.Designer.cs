namespace PetShopDelLitoral
{
    partial class FrmReportesVendedor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTituloSec = new Guna.UI2.WinForms.Guna2Panel();
            this.tituloReportes = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.panelPrincipalSec = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2DateTimePicker2 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2CustomGradientPanel5 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.iconButton6 = new FontAwesome.Sharp.IconButton();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.columnaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaVendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaMetodoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaDetalle = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelTituloSec.SuspendLayout();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.panelPrincipalSec.SuspendLayout();
            this.guna2CustomGradientPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTituloSec
            // 
            this.panelTituloSec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.panelTituloSec.Controls.Add(this.tituloReportes);
            this.panelTituloSec.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTituloSec.Location = new System.Drawing.Point(0, 0);
            this.panelTituloSec.Name = "panelTituloSec";
            this.panelTituloSec.Size = new System.Drawing.Size(1062, 77);
            this.panelTituloSec.TabIndex = 0;
            this.panelTituloSec.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTituloSec_Paint);
            // 
            // tituloReportes
            // 
            this.tituloReportes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.tituloReportes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tituloReportes.DefaultText = "Reportes de ventas";
            this.tituloReportes.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tituloReportes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tituloReportes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tituloReportes.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tituloReportes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.tituloReportes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tituloReportes.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.tituloReportes.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tituloReportes.Location = new System.Drawing.Point(26, 16);
            this.tituloReportes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tituloReportes.Name = "tituloReportes";
            this.tituloReportes.PasswordChar = '\0';
            this.tituloReportes.PlaceholderText = "";
            this.tituloReportes.SelectedText = "";
            this.tituloReportes.Size = new System.Drawing.Size(350, 56);
            this.tituloReportes.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(10, 10);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1042, 10);
            this.guna2Panel1.TabIndex = 0;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2CustomGradientPanel1.BorderRadius = 10;
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2CustomGradientPanel5);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2DateTimePicker2);
            this.guna2CustomGradientPanel1.Controls.Add(this.label2);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2DateTimePicker1);
            this.guna2CustomGradientPanel1.Controls.Add(this.label1);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(20, 20);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(1022, 80);
            this.guna2CustomGradientPanel1.TabIndex = 1;
            // 
            // panelPrincipalSec
            // 
            this.panelPrincipalSec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.panelPrincipalSec.Controls.Add(this.guna2DataGridView1);
            this.panelPrincipalSec.Controls.Add(this.guna2Panel5);
            this.panelPrincipalSec.Controls.Add(this.guna2Panel4);
            this.panelPrincipalSec.Controls.Add(this.guna2CustomGradientPanel1);
            this.panelPrincipalSec.Controls.Add(this.guna2Panel3);
            this.panelPrincipalSec.Controls.Add(this.guna2Panel2);
            this.panelPrincipalSec.Controls.Add(this.guna2Panel1);
            this.panelPrincipalSec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipalSec.ForeColor = System.Drawing.Color.Cornsilk;
            this.panelPrincipalSec.Location = new System.Drawing.Point(0, 77);
            this.panelPrincipalSec.Margin = new System.Windows.Forms.Padding(10);
            this.panelPrincipalSec.Name = "panelPrincipalSec";
            this.panelPrincipalSec.Padding = new System.Windows.Forms.Padding(10);
            this.panelPrincipalSec.Size = new System.Drawing.Size(1062, 496);
            this.panelPrincipalSec.TabIndex = 1;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel2.Location = new System.Drawing.Point(10, 20);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(10, 466);
            this.guna2Panel2.TabIndex = 2;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2Panel3.Location = new System.Drawing.Point(1042, 20);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(10, 466);
            this.guna2Panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.label1.Location = new System.Drawing.Point(47, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desde";
            // 
            // guna2DateTimePicker1
            // 
            this.guna2DateTimePicker1.BorderRadius = 5;
            this.guna2DateTimePicker1.Checked = true;
            this.guna2DateTimePicker1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2DateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.guna2DateTimePicker1.Location = new System.Drawing.Point(121, 16);
            this.guna2DateTimePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            this.guna2DateTimePicker1.Size = new System.Drawing.Size(235, 40);
            this.guna2DateTimePicker1.TabIndex = 15;
            this.guna2DateTimePicker1.Value = new System.DateTime(2026, 8, 28, 21, 35, 18, 468);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.label2.Location = new System.Drawing.Point(446, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "Hasta";
            // 
            // guna2DateTimePicker2
            // 
            this.guna2DateTimePicker2.BorderRadius = 5;
            this.guna2DateTimePicker2.Checked = true;
            this.guna2DateTimePicker2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2DateTimePicker2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.guna2DateTimePicker2.Location = new System.Drawing.Point(522, 16);
            this.guna2DateTimePicker2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker2.Name = "guna2DateTimePicker2";
            this.guna2DateTimePicker2.Size = new System.Drawing.Size(235, 40);
            this.guna2DateTimePicker2.TabIndex = 17;
            this.guna2DateTimePicker2.Value = new System.DateTime(2026, 8, 28, 21, 35, 18, 468);
            // 
            // guna2CustomGradientPanel5
            // 
            this.guna2CustomGradientPanel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2CustomGradientPanel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2CustomGradientPanel5.BorderRadius = 10;
            this.guna2CustomGradientPanel5.Controls.Add(this.iconButton6);
            this.guna2CustomGradientPanel5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2CustomGradientPanel5.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2CustomGradientPanel5.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2CustomGradientPanel5.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2CustomGradientPanel5.Location = new System.Drawing.Point(832, 16);
            this.guna2CustomGradientPanel5.Name = "guna2CustomGradientPanel5";
            this.guna2CustomGradientPanel5.Size = new System.Drawing.Size(125, 40);
            this.guna2CustomGradientPanel5.TabIndex = 18;
            // 
            // iconButton6
            // 
            this.iconButton6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.iconButton6.FlatAppearance.BorderSize = 0;
            this.iconButton6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(160)))));
            this.iconButton6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(160)))));
            this.iconButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.iconButton6.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.iconButton6.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton6.IconSize = 25;
            this.iconButton6.Location = new System.Drawing.Point(0, 0);
            this.iconButton6.Name = "iconButton6";
            this.iconButton6.Size = new System.Drawing.Size(125, 40);
            this.iconButton6.TabIndex = 1;
            this.iconButton6.Text = "Buscar";
            this.iconButton6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton6.UseVisualStyleBackColor = true;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel4.Location = new System.Drawing.Point(20, 100);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(1022, 10);
            this.guna2Panel4.TabIndex = 4;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel5.Location = new System.Drawing.Point(20, 476);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(1022, 10);
            this.guna2Panel5.TabIndex = 5;
            // 
            // guna2DataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.guna2DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.guna2DataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.guna2DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.guna2DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.guna2DataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.guna2DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.guna2DataGridView1.ColumnHeadersHeight = 30;
            this.guna2DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnaCodigo,
            this.columnaFecha,
            this.columnaVendedor,
            this.columnaCliente,
            this.columnaMetodoPago,
            this.columnaTotal,
            this.columnaDetalle});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Cornsilk;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.guna2DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2DataGridView1.EnableHeadersVisualStyles = false;
            this.guna2DataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView1.Location = new System.Drawing.Point(20, 110);
            this.guna2DataGridView1.Name = "guna2DataGridView1";
            this.guna2DataGridView1.RowHeadersVisible = false;
            this.guna2DataGridView1.RowHeadersWidth = 51;
            this.guna2DataGridView1.RowTemplate.Height = 24;
            this.guna2DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.guna2DataGridView1.Size = new System.Drawing.Size(1022, 366);
            this.guna2DataGridView1.TabIndex = 6;
            this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView1.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView1.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 30;
            this.guna2DataGridView1.ThemeStyle.ReadOnly = false;
            this.guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.guna2DataGridView1.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Cornsilk;
            this.guna2DataGridView1.ThemeStyle.RowsStyle.Height = 24;
            this.guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // columnaCodigo
            // 
            this.columnaCodigo.HeaderText = "Codigo";
            this.columnaCodigo.MinimumWidth = 6;
            this.columnaCodigo.Name = "columnaCodigo";
            // 
            // columnaFecha
            // 
            this.columnaFecha.HeaderText = "Fecha";
            this.columnaFecha.MinimumWidth = 6;
            this.columnaFecha.Name = "columnaFecha";
            // 
            // columnaVendedor
            // 
            this.columnaVendedor.HeaderText = "Vendedor";
            this.columnaVendedor.MinimumWidth = 6;
            this.columnaVendedor.Name = "columnaVendedor";
            // 
            // columnaCliente
            // 
            this.columnaCliente.HeaderText = "Cliente";
            this.columnaCliente.MinimumWidth = 6;
            this.columnaCliente.Name = "columnaCliente";
            // 
            // columnaMetodoPago
            // 
            this.columnaMetodoPago.HeaderText = "Metodo de pago";
            this.columnaMetodoPago.MinimumWidth = 6;
            this.columnaMetodoPago.Name = "columnaMetodoPago";
            // 
            // columnaTotal
            // 
            this.columnaTotal.HeaderText = "Total";
            this.columnaTotal.MinimumWidth = 6;
            this.columnaTotal.Name = "columnaTotal";
            // 
            // columnaDetalle
            // 
            this.columnaDetalle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnaDetalle.HeaderText = "Detalle ";
            this.columnaDetalle.MinimumWidth = 6;
            this.columnaDetalle.Name = "columnaDetalle";
            this.columnaDetalle.Text = "Ver Detalle";
            this.columnaDetalle.UseColumnTextForButtonValue = true;
            // 
            // FrmReportesVendedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1062, 573);
            this.Controls.Add(this.panelPrincipalSec);
            this.Controls.Add(this.panelTituloSec);
            this.Name = "FrmReportesVendedor";
            this.Text = "Form1";
            this.panelTituloSec.ResumeLayout(false);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.panelPrincipalSec.ResumeLayout(false);
            this.guna2CustomGradientPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTituloSec;
        private Guna.UI2.WinForms.Guna2TextBox tituloReportes;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel panelPrincipalSec;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker2;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel5;
        private FontAwesome.Sharp.IconButton iconButton6;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaVendedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaMetodoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaTotal;
        private System.Windows.Forms.DataGridViewButtonColumn columnaDetalle;
    }
}