namespace PetShopDelLitoral
{
    partial class FrmProductos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSup = new System.Windows.Forms.Panel();
            this.LBGestionProductos = new System.Windows.Forms.Label();
            this.guna2Panel6 = new Guna.UI2.WinForms.Guna2Panel();
            this.CBFiltroCateg = new Guna.UI2.WinForms.Guna2ComboBox();
            this.LBFiltrarcategoria = new System.Windows.Forms.Label();
            this.TxtBuscar = new Guna.UI2.WinForms.Guna2TextBox();
            this.iBSearch = new FontAwesome.Sharp.IconButton();
            this.DataGridViewProductos = new Guna.UI2.WinForms.Guna2DataGridView();
            this.ColumnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnEditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2Panel7 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2CustomGradientPanel6 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.btnCargarproducto = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.panelSup.SuspendLayout();
            this.guna2Panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewProductos)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.guna2CustomGradientPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSup
            // 
            this.panelSup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.panelSup.Controls.Add(this.LBGestionProductos);
            this.panelSup.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.panelSup.Location = new System.Drawing.Point(0, 0);
            this.panelSup.Name = "panelSup";
            this.panelSup.Size = new System.Drawing.Size(1072, 91);
            this.panelSup.TabIndex = 0;
            // 
            // LBGestionProductos
            // 
            this.LBGestionProductos.AutoSize = true;
            this.LBGestionProductos.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBGestionProductos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.LBGestionProductos.Location = new System.Drawing.Point(13, 28);
            this.LBGestionProductos.Name = "LBGestionProductos";
            this.LBGestionProductos.Size = new System.Drawing.Size(253, 27);
            this.LBGestionProductos.TabIndex = 0;
            this.LBGestionProductos.Text = "Gestión de Productos";
            // 
            // guna2Panel6
            // 
            this.guna2Panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2Panel6.Controls.Add(this.DataGridViewProductos);
            this.guna2Panel6.Controls.Add(this.guna2Panel4);
            this.guna2Panel6.Controls.Add(this.guna2Panel3);
            this.guna2Panel6.Controls.Add(this.guna2Panel2);
            this.guna2Panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel6.Location = new System.Drawing.Point(0, 183);
            this.guna2Panel6.Name = "guna2Panel6";
            this.guna2Panel6.Size = new System.Drawing.Size(1072, 387);
            this.guna2Panel6.TabIndex = 0;
            // 
            // CBFiltroCateg
            // 
            this.CBFiltroCateg.BackColor = System.Drawing.Color.Transparent;
            this.CBFiltroCateg.BorderRadius = 8;
            this.CBFiltroCateg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBFiltroCateg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBFiltroCateg.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBFiltroCateg.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBFiltroCateg.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBFiltroCateg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.CBFiltroCateg.ItemHeight = 30;
            this.CBFiltroCateg.Location = new System.Drawing.Point(469, 30);
            this.CBFiltroCateg.Name = "CBFiltroCateg";
            this.CBFiltroCateg.Size = new System.Drawing.Size(351, 36);
            this.CBFiltroCateg.TabIndex = 10;
            // 
            // LBFiltrarcategoria
            // 
            this.LBFiltrarcategoria.AutoSize = true;
            this.LBFiltrarcategoria.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBFiltrarcategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.LBFiltrarcategoria.Location = new System.Drawing.Point(476, 3);
            this.LBFiltrarcategoria.Name = "LBFiltrarcategoria";
            this.LBFiltrarcategoria.Size = new System.Drawing.Size(177, 20);
            this.LBFiltrarcategoria.TabIndex = 4;
            this.LBFiltrarcategoria.Text = "Filtrar por Categoria";
            // 
            // TxtBuscar
            // 
            this.TxtBuscar.BorderRadius = 8;
            this.TxtBuscar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtBuscar.DefaultText = "";
            this.TxtBuscar.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtBuscar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtBuscar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtBuscar.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtBuscar.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtBuscar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtBuscar.Location = new System.Drawing.Point(29, 30);
            this.TxtBuscar.Name = "TxtBuscar";
            this.TxtBuscar.PasswordChar = '\0';
            this.TxtBuscar.PlaceholderText = "Buscar Producto";
            this.TxtBuscar.SelectedText = "";
            this.TxtBuscar.Size = new System.Drawing.Size(351, 36);
            this.TxtBuscar.TabIndex = 9;
            this.TxtBuscar.TextChanged += new System.EventHandler(this.TxtBuscar_TextChanged);
            // 
            // iBSearch
            // 
            this.iBSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.iBSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iBSearch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBSearch.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.iBSearch.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.iBSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBSearch.IconSize = 30;
            this.iBSearch.Location = new System.Drawing.Point(377, 30);
            this.iBSearch.Name = "iBSearch";
            this.iBSearch.Size = new System.Drawing.Size(37, 36);
            this.iBSearch.TabIndex = 12;
            this.iBSearch.UseVisualStyleBackColor = false;
            // 
            // DataGridViewProductos
            // 
            this.DataGridViewProductos.AllowUserToResizeColumns = false;
            this.DataGridViewProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.DataGridViewProductos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridViewProductos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(235)))));
            this.DataGridViewProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridViewProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridViewProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(44)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewProductos.ColumnHeadersHeight = 22;
            this.DataGridViewProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCodigo,
            this.ColumnNombre,
            this.ColumnCategoria,
            this.ColumnPrecio,
            this.ColumnStock,
            this.ColumnEditar,
            this.ColumnEliminar});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewProductos.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewProductos.EnableHeadersVisualStyles = false;
            this.DataGridViewProductos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(213)))), ((int)(((byte)(195)))));
            this.DataGridViewProductos.Location = new System.Drawing.Point(20, 0);
            this.DataGridViewProductos.Name = "DataGridViewProductos";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(213)))), ((int)(((byte)(195)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(44)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewProductos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridViewProductos.RowHeadersVisible = false;
            this.DataGridViewProductos.RowHeadersWidth = 51;
            this.DataGridViewProductos.RowTemplate.Height = 24;
            this.DataGridViewProductos.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DataGridViewProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewProductos.ShowRowErrors = false;
            this.DataGridViewProductos.Size = new System.Drawing.Size(1032, 367);
            this.DataGridViewProductos.TabIndex = 11;
            this.DataGridViewProductos.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewProductos.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.DataGridViewProductos.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.DataGridViewProductos.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.DataGridViewProductos.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.DataGridViewProductos.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(235)))));
            this.DataGridViewProductos.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(213)))), ((int)(((byte)(195)))));
            this.DataGridViewProductos.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.DataGridViewProductos.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridViewProductos.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.DataGridViewProductos.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.DataGridViewProductos.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.DataGridViewProductos.ThemeStyle.HeaderStyle.Height = 22;
            this.DataGridViewProductos.ThemeStyle.ReadOnly = false;
            this.DataGridViewProductos.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewProductos.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridViewProductos.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.DataGridViewProductos.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DataGridViewProductos.ThemeStyle.RowsStyle.Height = 24;
            this.DataGridViewProductos.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewProductos.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DataGridViewProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.guna2DataGridView1_CellContentClick);
            // 
            // ColumnEliminar
            // 
            this.ColumnEliminar.HeaderText = "Eliminar";
            this.ColumnEliminar.MinimumWidth = 6;
            this.ColumnEliminar.Name = "ColumnEliminar";
            this.ColumnEliminar.Text = "Eliminar";
            this.ColumnEliminar.UseColumnTextForButtonValue = true;
            // 
            // ColumnEditar
            // 
            this.ColumnEditar.HeaderText = "Editar";
            this.ColumnEditar.MinimumWidth = 6;
            this.ColumnEditar.Name = "ColumnEditar";
            this.ColumnEditar.Text = "Editar";
            this.ColumnEditar.UseColumnTextForButtonValue = true;
            // 
            // ColumnStock
            // 
            this.ColumnStock.HeaderText = "Stock";
            this.ColumnStock.MinimumWidth = 6;
            this.ColumnStock.Name = "ColumnStock";
            // 
            // ColumnPrecio
            // 
            this.ColumnPrecio.HeaderText = "Precio";
            this.ColumnPrecio.MinimumWidth = 6;
            this.ColumnPrecio.Name = "ColumnPrecio";
            // 
            // ColumnCategoria
            // 
            this.ColumnCategoria.HeaderText = "Categoria";
            this.ColumnCategoria.MinimumWidth = 6;
            this.ColumnCategoria.Name = "ColumnCategoria";
            // 
            // ColumnNombre
            // 
            this.ColumnNombre.HeaderText = "Nombre";
            this.ColumnNombre.MinimumWidth = 6;
            this.ColumnNombre.Name = "ColumnNombre";
            // 
            // ColumnCodigo
            // 
            this.ColumnCodigo.HeaderText = "Codigo";
            this.ColumnCodigo.MinimumWidth = 6;
            this.ColumnCodigo.Name = "ColumnCodigo";
            // 
            // guna2Panel7
            // 
            this.guna2Panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2Panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel7.Location = new System.Drawing.Point(0, 163);
            this.guna2Panel7.Name = "guna2Panel7";
            this.guna2Panel7.Size = new System.Drawing.Size(1072, 20);
            this.guna2Panel7.TabIndex = 14;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.guna2CustomGradientPanel6);
            this.guna2Panel1.Controls.Add(this.TxtBuscar);
            this.guna2Panel1.Controls.Add(this.CBFiltroCateg);
            this.guna2Panel1.Controls.Add(this.LBFiltrarcategoria);
            this.guna2Panel1.Controls.Add(this.iBSearch);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 91);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1072, 72);
            this.guna2Panel1.TabIndex = 13;
            // 
            // guna2CustomGradientPanel6
            // 
            this.guna2CustomGradientPanel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2CustomGradientPanel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2CustomGradientPanel6.BorderRadius = 10;
            this.guna2CustomGradientPanel6.Controls.Add(this.btnCargarproducto);
            this.guna2CustomGradientPanel6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2CustomGradientPanel6.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2CustomGradientPanel6.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2CustomGradientPanel6.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.guna2CustomGradientPanel6.Location = new System.Drawing.Point(849, 26);
            this.guna2CustomGradientPanel6.Name = "guna2CustomGradientPanel6";
            this.guna2CustomGradientPanel6.Size = new System.Drawing.Size(180, 40);
            this.guna2CustomGradientPanel6.TabIndex = 17;
            // 
            // btnCargarproducto
            // 
            this.btnCargarproducto.BackColor = System.Drawing.Color.Transparent;
            this.btnCargarproducto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(160)))));
            this.btnCargarproducto.FlatAppearance.BorderSize = 0;
            this.btnCargarproducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(160)))));
            this.btnCargarproducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(160)))));
            this.btnCargarproducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarproducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCargarproducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.btnCargarproducto.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnCargarproducto.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.btnCargarproducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCargarproducto.IconSize = 25;
            this.btnCargarproducto.Location = new System.Drawing.Point(0, 0);
            this.btnCargarproducto.Name = "btnCargarproducto";
            this.btnCargarproducto.Size = new System.Drawing.Size(180, 40);
            this.btnCargarproducto.TabIndex = 1;
            this.btnCargarproducto.Text = "Cargar Producto";
            this.btnCargarproducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCargarproducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCargarproducto.UseVisualStyleBackColor = false;
            this.btnCargarproducto.Click += new System.EventHandler(this.BTCargarproducto_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.label1.Location = new System.Drawing.Point(25, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Buscar";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(20, 387);
            this.guna2Panel2.TabIndex = 12;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2Panel3.Location = new System.Drawing.Point(1052, 0);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(20, 387);
            this.guna2Panel3.TabIndex = 13;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel4.Location = new System.Drawing.Point(20, 367);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(1032, 20);
            this.guna2Panel4.TabIndex = 14;
            // 
            // FrmProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.ClientSize = new System.Drawing.Size(1072, 570);
            this.Controls.Add(this.guna2Panel6);
            this.Controls.Add(this.guna2Panel7);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.panelSup);
            this.Name = "FrmProductos";
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.Productos_Load);
            this.panelSup.ResumeLayout(false);
            this.panelSup.PerformLayout();
            this.guna2Panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewProductos)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2CustomGradientPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSup;
        private System.Windows.Forms.Label LBGestionProductos;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel6;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel6;
        private FontAwesome.Sharp.IconButton btnCargarproducto;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel7;
        private Guna.UI2.WinForms.Guna2DataGridView DataGridViewProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStock;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEditar;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEliminar;
        private FontAwesome.Sharp.IconButton iBSearch;
        private Guna.UI2.WinForms.Guna2TextBox TxtBuscar;
        private System.Windows.Forms.Label LBFiltrarcategoria;
        private Guna.UI2.WinForms.Guna2ComboBox CBFiltroCateg;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
    }
}