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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panelImagen = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.botonInicio = new FontAwesome.Sharp.IconButton();
            this.botonVentas = new FontAwesome.Sharp.IconButton();
            this.botonCompras = new FontAwesome.Sharp.IconButton();
            this.PanelContenedor.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelImagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
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
            this.PanelContenedor.Name = "PanelContenedor";
            this.PanelContenedor.Size = new System.Drawing.Size(1280, 720);
            this.PanelContenedor.TabIndex = 2;
            this.PanelContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelContenedor_Paint);
            // 
            // panelCentral
            // 
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.panelMenu.Controls.Add(this.panelImagen);
            this.panelMenu.Controls.Add(this.botonInicio);
            this.panelMenu.Controls.Add(this.botonVentas);
            this.panelMenu.Controls.Add(this.botonCompras);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 720);
            this.panelMenu.TabIndex = 0;
            this.panelMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMenu_Paint);
            // 
            // panelImagen
            // 
            this.panelImagen.Controls.Add(this.logo);
            this.panelImagen.Location = new System.Drawing.Point(3, 3);
            this.panelImagen.Name = "panelImagen";
            this.panelImagen.Size = new System.Drawing.Size(197, 100);
            this.panelImagen.TabIndex = 0;
            // 
            // logo
            // 
            this.logo.Image = global::PetShopDelLitoral.Properties.Resources.logoPetShop;
            this.logo.Location = new System.Drawing.Point(0, -3);
            this.logo.Name = "logo";
            this.logo.Padding = new System.Windows.Forms.Padding(10);
            this.logo.Size = new System.Drawing.Size(197, 100);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // botonInicio
            // 
            this.botonInicio.AutoSize = true;
            this.botonInicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.botonInicio.FlatAppearance.BorderSize = 0;
            this.botonInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.botonInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonInicio.IconChar = FontAwesome.Sharp.IconChar.HomeUser;
            this.botonInicio.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(111)))));
            this.botonInicio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.botonInicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonInicio.Location = new System.Drawing.Point(3, 109);
            this.botonInicio.Name = "botonInicio";
            this.botonInicio.Size = new System.Drawing.Size(190, 56);
            this.botonInicio.TabIndex = 2;
            this.botonInicio.Text = "Inicio";
            this.botonInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonInicio.UseVisualStyleBackColor = false;
            this.botonInicio.Click += new System.EventHandler(this.botonInicio_Click);
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
            this.botonVentas.Location = new System.Drawing.Point(3, 171);
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
            this.botonCompras.Location = new System.Drawing.Point(3, 222);
            this.botonCompras.Name = "botonCompras";
            this.botonCompras.Size = new System.Drawing.Size(190, 45);
            this.botonCompras.TabIndex = 0;
            this.botonCompras.Text = "Compras";
            this.botonCompras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botonCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonCompras.UseVisualStyleBackColor = true;
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
            this.panelMenu.PerformLayout();
            this.panelImagen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelContenedor;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.FlowLayoutPanel panelMenu;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.Panel panelImagen;
        private System.Windows.Forms.PictureBox logo;
        private FontAwesome.Sharp.IconButton botonInicio;
        private FontAwesome.Sharp.IconButton botonVentas;
        private FontAwesome.Sharp.IconButton botonCompras;
    }
}

