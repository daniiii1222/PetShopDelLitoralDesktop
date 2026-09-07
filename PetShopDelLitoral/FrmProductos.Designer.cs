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
            this.LBBuscar = new System.Windows.Forms.Label();
            this.panelGeneralProductos = new System.Windows.Forms.Panel();
            this.iBSearch = new FontAwesome.Sharp.IconButton();
            this.DataGridViewProductos = new Guna.UI2.WinForms.Guna2DataGridView();
            this.ColumnCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CBFiltroCateg = new Guna.UI2.WinForms.Guna2ComboBox();
            this.TxtBuscar = new Guna.UI2.WinForms.Guna2TextBox();
            this.BTCargarproducto = new Guna.UI2.WinForms.Guna2Button();
            this.LBFiltrarcategoria = new System.Windows.Forms.Label();
            this.panelSup.SuspendLayout();
            this.panelGeneralProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewProductos)).BeginInit();
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
            // LBBuscar
            // 
            this.LBBuscar.AutoSize = true;
            this.LBBuscar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.LBBuscar.Location = new System.Drawing.Point(38, 23);
            this.LBBuscar.Name = "LBBuscar";
            this.LBBuscar.Size = new System.Drawing.Size(80, 23);
            this.LBBuscar.TabIndex = 1;
            this.LBBuscar.Text = "Buscar";
            this.LBBuscar.Click += new System.EventHandler(this.label1_Click);
            // 
            // panelGeneralProductos
            // 
            this.panelGeneralProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.panelGeneralProductos.Controls.Add(this.iBSearch);
            this.panelGeneralProductos.Controls.Add(this.DataGridViewProductos);
            this.panelGeneralProductos.Controls.Add(this.CBFiltroCateg);
            this.panelGeneralProductos.Controls.Add(this.TxtBuscar);
            this.panelGeneralProductos.Controls.Add(this.BTCargarproducto);
            this.panelGeneralProductos.Controls.Add(this.LBFiltrarcategoria);
            this.panelGeneralProductos.Controls.Add(this.LBBuscar);
            this.panelGeneralProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGeneralProductos.Location = new System.Drawing.Point(0, 91);
            this.panelGeneralProductos.Margin = new System.Windows.Forms.Padding(0);
            this.panelGeneralProductos.Name = "panelGeneralProductos";
            this.panelGeneralProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelGeneralProductos.Size = new System.Drawing.Size(1072, 479);
            this.panelGeneralProductos.TabIndex = 3;
            this.panelGeneralProductos.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
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
            this.iBSearch.Location = new System.Drawing.Point(306, 17);
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
            this.DataGridViewProductos.EnableHeadersVisualStyles = false;
            this.DataGridViewProductos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(213)))), ((int)(((byte)(195)))));
            this.DataGridViewProductos.Location = new System.Drawing.Point(25, 82);
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
            this.DataGridViewProductos.Size = new System.Drawing.Size(1020, 380);
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
            // ColumnCodigo
            // 
            this.ColumnCodigo.HeaderText = "Codigo";
            this.ColumnCodigo.MinimumWidth = 6;
            this.ColumnCodigo.Name = "ColumnCodigo";
            // 
            // ColumnNombre
            // 
            this.ColumnNombre.HeaderText = "Nombre";
            this.ColumnNombre.MinimumWidth = 6;
            this.ColumnNombre.Name = "ColumnNombre";
            // 
            // ColumnCategoria
            // 
            this.ColumnCategoria.HeaderText = "Categoria";
            this.ColumnCategoria.MinimumWidth = 6;
            this.ColumnCategoria.Name = "ColumnCategoria";
            // 
            // ColumnPrecio
            // 
            this.ColumnPrecio.HeaderText = "Precio";
            this.ColumnPrecio.MinimumWidth = 6;
            this.ColumnPrecio.Name = "ColumnPrecio";
            // 
            // ColumnStock
            // 
            this.ColumnStock.HeaderText = "Stock";
            this.ColumnStock.MinimumWidth = 6;
            this.ColumnStock.Name = "ColumnStock";
            // 
            // ColumnEditar
            // 
            this.ColumnEditar.HeaderText = "Editar";
            this.ColumnEditar.MinimumWidth = 6;
            this.ColumnEditar.Name = "ColumnEditar";
            this.ColumnEditar.Text = "Editar";
            this.ColumnEditar.UseColumnTextForButtonValue = true;
            // 
            // ColumnEliminar
            // 
            this.ColumnEliminar.HeaderText = "Eliminar";
            this.ColumnEliminar.MinimumWidth = 6;
            this.ColumnEliminar.Name = "ColumnEliminar";
            this.ColumnEliminar.Text = "Eliminar";
            this.ColumnEliminar.UseColumnTextForButtonValue = true;
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
            this.CBFiltroCateg.Location = new System.Drawing.Point(571, 17);
            this.CBFiltroCateg.Name = "CBFiltroCateg";
            this.CBFiltroCateg.Size = new System.Drawing.Size(207, 36);
            this.CBFiltroCateg.TabIndex = 10;
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
            this.TxtBuscar.Location = new System.Drawing.Point(124, 17);
            this.TxtBuscar.Name = "TxtBuscar";
            this.TxtBuscar.PasswordChar = '\0';
            this.TxtBuscar.PlaceholderText = "Buscar Producto";
            this.TxtBuscar.SelectedText = "";
            this.TxtBuscar.Size = new System.Drawing.Size(219, 36);
            this.TxtBuscar.TabIndex = 9;
            // 
            // BTCargarproducto
            // 
            this.BTCargarproducto.BorderRadius = 8;
            this.BTCargarproducto.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BTCargarproducto.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BTCargarproducto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BTCargarproducto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BTCargarproducto.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.BTCargarproducto.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTCargarproducto.ForeColor = System.Drawing.Color.White;
            this.BTCargarproducto.Location = new System.Drawing.Point(827, 23);
            this.BTCargarproducto.Name = "BTCargarproducto";
            this.BTCargarproducto.Size = new System.Drawing.Size(166, 30);
            this.BTCargarproducto.TabIndex = 8;
            this.BTCargarproducto.Text = "Cargar Producto";
            this.BTCargarproducto.Click += new System.EventHandler(this.BTCargarproducto_Click);
            // 
            // LBFiltrarcategoria
            // 
            this.LBFiltrarcategoria.AutoSize = true;
            this.LBFiltrarcategoria.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBFiltrarcategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.LBFiltrarcategoria.Location = new System.Drawing.Point(355, 23);
            this.LBFiltrarcategoria.Name = "LBFiltrarcategoria";
            this.LBFiltrarcategoria.Size = new System.Drawing.Size(210, 23);
            this.LBFiltrarcategoria.TabIndex = 4;
            this.LBFiltrarcategoria.Text = "Filtrar por Categoria";
            // 
            // FrmProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1072, 570);
            this.Controls.Add(this.panelGeneralProductos);
            this.Controls.Add(this.panelSup);
            this.Name = "FrmProductos";
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.Productos_Load);
            this.panelSup.ResumeLayout(false);
            this.panelSup.PerformLayout();
            this.panelGeneralProductos.ResumeLayout(false);
            this.panelGeneralProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSup;
        private System.Windows.Forms.Label LBGestionProductos;
        private System.Windows.Forms.Label LBBuscar;
        private System.Windows.Forms.Panel panelGeneralProductos;
        private System.Windows.Forms.Label LBFiltrarcategoria;
        private Guna.UI2.WinForms.Guna2Button BTCargarproducto;
        private Guna.UI2.WinForms.Guna2ComboBox CBFiltroCateg;
        private Guna.UI2.WinForms.Guna2TextBox TxtBuscar;
        private Guna.UI2.WinForms.Guna2DataGridView DataGridViewProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStock;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEditar;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEliminar;
        private FontAwesome.Sharp.IconButton iBSearch;
    }
}