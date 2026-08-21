namespace PetShopDelLitoral
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            FontAwesome.Sharp.IconButton btnAlimentos;
            FontAwesome.Sharp.IconButton botonRopa;
            FontAwesome.Sharp.IconButton botonAccesorios;
            this.PanelContenedor = new System.Windows.Forms.Panel();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.botonVentas = new FontAwesome.Sharp.IconButton();
            this.botonCompras = new FontAwesome.Sharp.IconButton();
            this.botonProductos = new FontAwesome.Sharp.IconButton();
            this.pnlSubMenuProductos = new System.Windows.Forms.FlowLayoutPanel();
            this.botonUsuarios = new FontAwesome.Sharp.IconButton();
            this.botonClientes = new FontAwesome.Sharp.IconButton();
            this.botonProvedores = new FontAwesome.Sharp.IconButton();
            this.botonReportes = new FontAwesome.Sharp.IconButton();
            this.botonInicio = new FontAwesome.Sharp.IconButton();
            btnAlimentos = new FontAwesome.Sharp.IconButton();
            botonRopa = new FontAwesome.Sharp.IconButton();
            botonAccesorios = new FontAwesome.Sharp.IconButton();
            this.PanelContenedor.SuspendLayout();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.pnlSubMenuProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAlimentos
            // 
            btnAlimentos.Dock = System.Windows.Forms.DockStyle.Top;
            btnAlimentos.FlatAppearance.BorderSize = 0;
            btnAlimentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAlimentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            btnAlimentos.IconChar = FontAwesome.Sharp.IconChar.Bone;
            btnAlimentos.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            btnAlimentos.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btnAlimentos.IconSize = 40;
            btnAlimentos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnAlimentos.Location = new System.Drawing.Point(3, 3);
            btnAlimentos.Name = "btnAlimentos";
            btnAlimentos.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            btnAlimentos.Size = new System.Drawing.Size(190, 42);
            btnAlimentos.TabIndex = 0;
            btnAlimentos.Text = "Alimentos";
            btnAlimentos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnAlimentos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnAlimentos.UseVisualStyleBackColor = true;
            // 
            // botonRopa
            // 
            botonRopa.Dock = System.Windows.Forms.DockStyle.Top;
            botonRopa.FlatAppearance.BorderSize = 0;
            botonRopa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            botonRopa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            botonRopa.IconChar = FontAwesome.Sharp.IconChar.Tshirt;
            botonRopa.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            botonRopa.IconFont = FontAwesome.Sharp.IconFont.Solid;
            botonRopa.IconSize = 40;
            botonRopa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            botonRopa.Location = new System.Drawing.Point(3, 51);
            botonRopa.Name = "botonRopa";
            botonRopa.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            botonRopa.Size = new System.Drawing.Size(190, 42);
            botonRopa.TabIndex = 1;
            botonRopa.Text = "Ropa";
            botonRopa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            botonRopa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            botonRopa.UseVisualStyleBackColor = true;
            botonRopa.Click += new System.EventHandler(this.botonRopa_Click);
            // 
            // botonAccesorios
            // 
            botonAccesorios.Dock = System.Windows.Forms.DockStyle.Top;
            botonAccesorios.FlatAppearance.BorderSize = 0;
            botonAccesorios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            botonAccesorios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            botonAccesorios.IconChar = FontAwesome.Sharp.IconChar.Tags;
            botonAccesorios.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            botonAccesorios.IconFont = FontAwesome.Sharp.IconFont.Solid;
            botonAccesorios.IconSize = 40;
            botonAccesorios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            botonAccesorios.Location = new System.Drawing.Point(3, 99);
            botonAccesorios.Name = "botonAccesorios";
            botonAccesorios.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            botonAccesorios.Size = new System.Drawing.Size(190, 42);
            botonAccesorios.TabIndex = 2;
            botonAccesorios.Text = "Accesorios";
            botonAccesorios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            botonAccesorios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            botonAccesorios.UseVisualStyleBackColor = true;
            // 
            // PanelContenedor
            // 
            this.PanelContenedor.BackColor = System.Drawing.Color.White;
            this.PanelContenedor.Controls.Add(this.panelCentral);
            this.PanelContenedor.Controls.Add(this.panelSuperior);
            this.PanelContenedor.Controls.Add(this.panelMenu);
            this.PanelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenedor.Location = new System.Drawing.Point(0, 0);
            this.PanelContenedor.Name = "PanelContenedor";
            this.PanelContenedor.Size = new System.Drawing.Size(1280, 720);
            this.PanelContenedor.TabIndex = 2;
            this.PanelContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelContenedor_Paint);
            // 
            // panelCentral
            // 
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCentral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelCentral.Location = new System.Drawing.Point(200, 100);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Size = new System.Drawing.Size(1080, 620);
            this.panelCentral.TabIndex = 2;
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(200, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1080, 100);
            this.panelSuperior.TabIndex = 1;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.panelMenu.Controls.Add(this.logo);
            this.panelMenu.Controls.Add(this.botonInicio);
            this.panelMenu.Controls.Add(this.botonVentas);
            this.panelMenu.Controls.Add(this.botonCompras);
            this.panelMenu.Controls.Add(this.botonProductos);
            this.panelMenu.Controls.Add(this.pnlSubMenuProductos);
            this.panelMenu.Controls.Add(this.botonUsuarios);
            this.panelMenu.Controls.Add(this.botonClientes);
            this.panelMenu.Controls.Add(this.botonProvedores);
            this.panelMenu.Controls.Add(this.botonReportes);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 720);
            this.panelMenu.TabIndex = 0;
            this.panelMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMenu_Paint);
            // 
            // logo
            // 
            this.logo.Image = global::PetShopDelLitoral.Properties.Resources.logoPetShop;
            this.logo.Location = new System.Drawing.Point(3, 3);
            this.logo.Name = "logo";
            this.logo.Padding = new System.Windows.Forms.Padding(10);
            this.logo.Size = new System.Drawing.Size(190, 94);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // botonVentas
            // 
            this.botonVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.botonVentas.FlatAppearance.BorderSize = 0;
            this.botonVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonVentas.IconChar = FontAwesome.Sharp.IconChar.Store;
            this.botonVentas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonVentas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonVentas.Location = new System.Drawing.Point(3, 154);
            this.botonVentas.Name = "botonVentas";
            this.botonVentas.Size = new System.Drawing.Size(190, 45);
            this.botonVentas.TabIndex = 2;
            this.botonVentas.Text = "Ventas";
            this.botonVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonVentas.UseVisualStyleBackColor = true;
            this.botonVentas.Click += new System.EventHandler(this.botonVentas_Click_1);
            // 
            // botonCompras
            // 
            this.botonCompras.FlatAppearance.BorderSize = 0;
            this.botonCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonCompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonCompras.IconChar = FontAwesome.Sharp.IconChar.BagShopping;
            this.botonCompras.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonCompras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonCompras.Location = new System.Drawing.Point(3, 205);
            this.botonCompras.Name = "botonCompras";
            this.botonCompras.Size = new System.Drawing.Size(190, 45);
            this.botonCompras.TabIndex = 0;
            this.botonCompras.Text = "Compras";
            this.botonCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonCompras.UseVisualStyleBackColor = true;
            // 
            // botonProductos
            // 
            this.botonProductos.FlatAppearance.BorderSize = 0;
            this.botonProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonProductos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonProductos.IconChar = FontAwesome.Sharp.IconChar.Box;
            this.botonProductos.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonProductos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonProductos.Location = new System.Drawing.Point(3, 256);
            this.botonProductos.Name = "botonProductos";
            this.botonProductos.Size = new System.Drawing.Size(190, 45);
            this.botonProductos.TabIndex = 3;
            this.botonProductos.Text = "Productos";
            this.botonProductos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonProductos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonProductos.UseVisualStyleBackColor = true;
            this.botonProductos.Click += new System.EventHandler(this.botonProductos_Click);
            // 
            // pnlSubMenuProductos
            // 
            this.pnlSubMenuProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(16)))));
            this.pnlSubMenuProductos.Controls.Add(btnAlimentos);
            this.pnlSubMenuProductos.Controls.Add(botonRopa);
            this.pnlSubMenuProductos.Controls.Add(botonAccesorios);
            this.pnlSubMenuProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubMenuProductos.Location = new System.Drawing.Point(3, 307);
            this.pnlSubMenuProductos.Name = "pnlSubMenuProductos";
            this.pnlSubMenuProductos.Size = new System.Drawing.Size(197, 149);
            this.pnlSubMenuProductos.TabIndex = 4;
            this.pnlSubMenuProductos.Visible = false;
            // 
            // botonUsuarios
            // 
            this.botonUsuarios.FlatAppearance.BorderSize = 0;
            this.botonUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonUsuarios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.botonUsuarios.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonUsuarios.Location = new System.Drawing.Point(3, 462);
            this.botonUsuarios.Name = "botonUsuarios";
            this.botonUsuarios.Size = new System.Drawing.Size(190, 45);
            this.botonUsuarios.TabIndex = 5;
            this.botonUsuarios.Text = "Usuarios";
            this.botonUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonUsuarios.UseVisualStyleBackColor = true;
            // 
            // botonClientes
            // 
            this.botonClientes.FlatAppearance.BorderSize = 0;
            this.botonClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonClientes.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.botonClientes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonClientes.Location = new System.Drawing.Point(3, 513);
            this.botonClientes.Name = "botonClientes";
            this.botonClientes.Size = new System.Drawing.Size(190, 45);
            this.botonClientes.TabIndex = 6;
            this.botonClientes.Text = "Clientes";
            this.botonClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonClientes.UseVisualStyleBackColor = true;
            // 
            // botonProvedores
            // 
            this.botonProvedores.FlatAppearance.BorderSize = 0;
            this.botonProvedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonProvedores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonProvedores.IconChar = FontAwesome.Sharp.IconChar.Truck;
            this.botonProvedores.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonProvedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonProvedores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonProvedores.Location = new System.Drawing.Point(3, 564);
            this.botonProvedores.Name = "botonProvedores";
            this.botonProvedores.Size = new System.Drawing.Size(190, 45);
            this.botonProvedores.TabIndex = 8;
            this.botonProvedores.Text = "Provedores";
            this.botonProvedores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonProvedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonProvedores.UseVisualStyleBackColor = true;
            this.botonProvedores.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // botonReportes
            // 
            this.botonReportes.FlatAppearance.BorderSize = 0;
            this.botonReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonReportes.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.botonReportes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonReportes.Location = new System.Drawing.Point(3, 615);
            this.botonReportes.Name = "botonReportes";
            this.botonReportes.Size = new System.Drawing.Size(190, 45);
            this.botonReportes.TabIndex = 7;
            this.botonReportes.Text = "Reportes";
            this.botonReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonReportes.UseVisualStyleBackColor = true;
            // 
            // botonInicio
            // 
            this.botonInicio.FlatAppearance.BorderSize = 0;
            this.botonInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonInicio.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            this.botonInicio.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonInicio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonInicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonInicio.Location = new System.Drawing.Point(3, 103);
            this.botonInicio.Name = "botonInicio";
            this.botonInicio.Size = new System.Drawing.Size(190, 45);
            this.botonInicio.TabIndex = 9;
            this.botonInicio.Text = "inicio";
            this.botonInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonInicio.UseVisualStyleBackColor = true;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(221)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.PanelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Inicio";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.PanelContenedor.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.pnlSubMenuProductos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelContenedor;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.FlowLayoutPanel panelMenu;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.PictureBox logo;
        private FontAwesome.Sharp.IconButton botonVentas;
        private FontAwesome.Sharp.IconButton botonCompras;
        private FontAwesome.Sharp.IconButton botonProductos;
        private System.Windows.Forms.FlowLayoutPanel pnlSubMenuProductos;
        private FontAwesome.Sharp.IconButton botonUsuarios;
        private FontAwesome.Sharp.IconButton botonClientes;
        private FontAwesome.Sharp.IconButton botonReportes;
        private FontAwesome.Sharp.IconButton botonProvedores;
        private FontAwesome.Sharp.IconButton botonInicio;
    }
}

