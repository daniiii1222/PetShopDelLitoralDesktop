using FontAwesome.Sharp;

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
            this.PanelContenedor = new System.Windows.Forms.Panel();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.PanelControl = new Guna.UI2.WinForms.Guna2Panel();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.botonInicio = new FontAwesome.Sharp.IconButton();
            this.botonVentas = new FontAwesome.Sharp.IconButton();
            this.botonCompras = new FontAwesome.Sharp.IconButton();
            this.botonProductos = new FontAwesome.Sharp.IconButton();
            this.botonPersonas = new FontAwesome.Sharp.IconButton();
            this.botonReportes = new FontAwesome.Sharp.IconButton();
            this.btnBackup = new FontAwesome.Sharp.IconButton();
            this.panelVentas = new System.Windows.Forms.Panel();
            this.panelCompra = new System.Windows.Forms.Panel();
            this.PanelGeneral = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PanelContenedor.SuspendLayout();
            this.panelCentral.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.PanelGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelContenedor
            // 
            this.PanelContenedor.BackColor = System.Drawing.Color.White;
            this.PanelContenedor.Controls.Add(this.panelCentral);
            this.PanelContenedor.Controls.Add(this.panelSuperior);
            this.PanelContenedor.Controls.Add(this.panelMenu);
            this.PanelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenedor.Location = new System.Drawing.Point(0, 0);
            this.PanelContenedor.Margin = new System.Windows.Forms.Padding(4);
            this.PanelContenedor.Name = "PanelContenedor";
            this.PanelContenedor.Size = new System.Drawing.Size(1707, 886);
            this.PanelContenedor.TabIndex = 2;
            this.PanelContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelContenedor_Paint);
            // 
            // panelCentral
            // 
            this.panelCentral.Controls.Add(this.PanelControl);
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCentral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelCentral.Location = new System.Drawing.Point(267, 123);
            this.panelCentral.Margin = new System.Windows.Forms.Padding(4);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Size = new System.Drawing.Size(1440, 763);
            this.panelCentral.TabIndex = 2;
            this.panelCentral.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCentral_Paint);
            // 
            // PanelControl
            // 
            this.PanelControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.PanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelControl.Location = new System.Drawing.Point(0, 0);
            this.PanelControl.Margin = new System.Windows.Forms.Padding(4);
            this.PanelControl.Name = "PanelControl";
            this.PanelControl.Size = new System.Drawing.Size(1440, 763);
            this.PanelControl.TabIndex = 0;
            this.PanelControl.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelControl_Paint);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.panelSuperior.Controls.Add(this.PanelGeneral);
            this.panelSuperior.Controls.Add(this.panel1);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(267, 0);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(4);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1440, 123);
            this.panelSuperior.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(8, 122);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 123);
            this.panel1.TabIndex = 0;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.panelMenu.Controls.Add(this.logo);
            this.panelMenu.Controls.Add(this.botonInicio);
            this.panelMenu.Controls.Add(this.botonVentas);
            this.panelMenu.Controls.Add(this.botonCompras);
            this.panelMenu.Controls.Add(this.botonProductos);
            this.panelMenu.Controls.Add(this.botonPersonas);
            this.panelMenu.Controls.Add(this.botonReportes);
            this.panelMenu.Controls.Add(this.btnBackup);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(267, 886);
            this.panelMenu.TabIndex = 0;
            this.panelMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMenu_Paint);
            // 
            // logo
            // 
            this.logo.Image = global::PetShopDelLitoral.Properties.Resources.logoPetShop;
            this.logo.Location = new System.Drawing.Point(4, 4);
            this.logo.Margin = new System.Windows.Forms.Padding(4);
            this.logo.Name = "logo";
            this.logo.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.logo.Size = new System.Drawing.Size(263, 119);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.logo_Click);
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
            this.botonInicio.Location = new System.Drawing.Point(4, 131);
            this.botonInicio.Margin = new System.Windows.Forms.Padding(4);
            this.botonInicio.Name = "botonInicio";
            this.botonInicio.Size = new System.Drawing.Size(267, 55);
            this.botonInicio.TabIndex = 9;
            this.botonInicio.Text = "inicio";
            this.botonInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonInicio.UseVisualStyleBackColor = true;
            this.botonInicio.Click += new System.EventHandler(this.botonInicio_Click_1);
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
            this.botonVentas.Location = new System.Drawing.Point(4, 194);
            this.botonVentas.Margin = new System.Windows.Forms.Padding(4);
            this.botonVentas.Name = "botonVentas";
            this.botonVentas.Size = new System.Drawing.Size(253, 55);
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
            this.botonCompras.Location = new System.Drawing.Point(4, 257);
            this.botonCompras.Margin = new System.Windows.Forms.Padding(4);
            this.botonCompras.Name = "botonCompras";
            this.botonCompras.Size = new System.Drawing.Size(253, 55);
            this.botonCompras.TabIndex = 0;
            this.botonCompras.Text = "Compras";
            this.botonCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonCompras.UseVisualStyleBackColor = true;
            this.botonCompras.Click += new System.EventHandler(this.botonCompras_Click);
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
            this.botonProductos.Location = new System.Drawing.Point(4, 320);
            this.botonProductos.Margin = new System.Windows.Forms.Padding(4);
            this.botonProductos.Name = "botonProductos";
            this.botonProductos.Size = new System.Drawing.Size(253, 55);
            this.botonProductos.TabIndex = 3;
            this.botonProductos.Text = "Productos";
            this.botonProductos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonProductos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonProductos.UseVisualStyleBackColor = true;
            this.botonProductos.Click += new System.EventHandler(this.botonProductos_Click);
            // 
            // botonPersonas
            // 
            this.botonPersonas.FlatAppearance.BorderSize = 0;
            this.botonPersonas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonPersonas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonPersonas.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.botonPersonas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonPersonas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonPersonas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonPersonas.Location = new System.Drawing.Point(4, 383);
            this.botonPersonas.Margin = new System.Windows.Forms.Padding(4);
            this.botonPersonas.Name = "botonPersonas";
            this.botonPersonas.Size = new System.Drawing.Size(253, 55);
            this.botonPersonas.TabIndex = 5;
            this.botonPersonas.Text = "Personas";
            this.botonPersonas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonPersonas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonPersonas.UseVisualStyleBackColor = true;
            this.botonPersonas.Click += new System.EventHandler(this.btnPersonas_click);
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
            this.botonReportes.Location = new System.Drawing.Point(4, 446);
            this.botonReportes.Margin = new System.Windows.Forms.Padding(4);
            this.botonReportes.Name = "botonReportes";
            this.botonReportes.Size = new System.Drawing.Size(253, 55);
            this.botonReportes.TabIndex = 7;
            this.botonReportes.Text = "Reportes";
            this.botonReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonReportes.UseVisualStyleBackColor = true;
            this.botonReportes.Click += new System.EventHandler(this.botonReportes_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.btnBackup.IconChar = FontAwesome.Sharp.IconChar.Database;
            this.btnBackup.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.btnBackup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.Location = new System.Drawing.Point(4, 509);
            this.btnBackup.Margin = new System.Windows.Forms.Padding(4);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(253, 55);
            this.btnBackup.TabIndex = 6;
            this.btnBackup.Text = "Backup";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click_2);
            // 
            // panelVentas
            // 
            this.panelVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(16)))));
            this.panelVentas.Location = new System.Drawing.Point(3, 208);
            this.panelVentas.Name = "panelVentas";
            this.panelVentas.Size = new System.Drawing.Size(200, 71);
            this.panelVentas.TabIndex = 10;
            this.panelVentas.Visible = false;
            // 
            // panelCompra
            // 
            this.panelCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(16)))));
            this.panelCompra.Location = new System.Drawing.Point(3, 336);
            this.panelCompra.Name = "panelCompra";
            this.panelCompra.Size = new System.Drawing.Size(200, 71);
            this.panelCompra.TabIndex = 11;
            this.panelCompra.Visible = false;
            // 
            // PanelGeneral
            // 
            this.PanelGeneral.Controls.Add(this.lblFecha);
            this.PanelGeneral.Controls.Add(this.lblBienvenida);
            this.PanelGeneral.Controls.Add(this.label6);
            this.PanelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelGeneral.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.PanelGeneral.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.PanelGeneral.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.PanelGeneral.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.PanelGeneral.Location = new System.Drawing.Point(0, 0);
            this.PanelGeneral.Name = "PanelGeneral";
            this.PanelGeneral.Size = new System.Drawing.Size(1440, 123);
            this.PanelGeneral.TabIndex = 19;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.lblFecha.Location = new System.Drawing.Point(817, 66);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(0, 16);
            this.lblFecha.TabIndex = 1;
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.lblBienvenida.Location = new System.Drawing.Point(30, 24);
            this.lblBienvenida.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(78, 32);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Inicio";
            this.lblBienvenida.UseMnemonic = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.label6.Location = new System.Drawing.Point(31, 56);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 28);
            this.label6.TabIndex = 10;
            this.label6.Text = "Tablero Principal";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(221)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(1707, 886);
            this.Controls.Add(this.PanelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.PanelContenedor.ResumeLayout(false);
            this.panelCentral.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.PanelGeneral.ResumeLayout(false);
            this.PanelGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelCompra;

        public IconButton BotonDetalleCompra { get; private set; }
        public IconButton BotonRegistrarCompra { get; private set; }

        private System.Windows.Forms.Panel panelVentas;

        public IconButton BotonDetalleVentas { get; private set; }
        public IconButton BotonResgistroVenta { get; private set; }
        private System.Windows.Forms.Panel PanelContenedor;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.FlowLayoutPanel panelMenu;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.PictureBox logo;
        private FontAwesome.Sharp.IconButton botonVentas;
        private FontAwesome.Sharp.IconButton botonCompras;
        private FontAwesome.Sharp.IconButton botonProductos;
        private FontAwesome.Sharp.IconButton botonPersonas;
        private FontAwesome.Sharp.IconButton botonReportes;
        private FontAwesome.Sharp.IconButton botonInicio;
        private FontAwesome.Sharp.IconButton btnBackup;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Panel PanelControl;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel PanelGeneral;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label label6;
    }
}

