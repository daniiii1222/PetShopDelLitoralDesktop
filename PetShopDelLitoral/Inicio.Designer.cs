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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            PanelContenedor = new Panel();
            panelCentral = new Panel();
            PanelControl = new Guna.UI2.WinForms.Guna2Panel();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            panelSuperior = new Panel();
            panel1 = new Panel();
            panelMenu = new FlowLayoutPanel();
            logo = new PictureBox();
            botonInicio = new IconButton();
            botonVentas = new IconButton();
            botonCompras = new IconButton();
            botonProductos = new IconButton();
            botonUsuarios = new IconButton();
            botonClientes = new IconButton();
            botonProvedores = new IconButton();
            botonReportes = new IconButton();
            BotonMantenedor = new IconButton();
            panelVentas = new Panel();
            panelCompra = new Panel();
            PanelContenedor.SuspendLayout();
            panelCentral.SuspendLayout();
            PanelControl.SuspendLayout();
            panelSuperior.SuspendLayout();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logo).BeginInit();
            SuspendLayout();
            // 
            // PanelContenedor
            // 
            PanelContenedor.BackColor = Color.White;
            PanelContenedor.Controls.Add(panelCentral);
            PanelContenedor.Controls.Add(panelSuperior);
            PanelContenedor.Controls.Add(panelMenu);
            PanelContenedor.Dock = DockStyle.Fill;
            PanelContenedor.Location = new Point(0, 0);
            PanelContenedor.Margin = new Padding(3, 4, 3, 4);
            PanelContenedor.Name = "PanelContenedor";
            PanelContenedor.Size = new Size(1280, 900);
            PanelContenedor.TabIndex = 2;
            PanelContenedor.Paint += PanelContenedor_Paint;
            // 
            // panelCentral
            // 
            panelCentral.Controls.Add(PanelControl);
            panelCentral.Dock = DockStyle.Top;
            panelCentral.Font = new Font("Microsoft Sans Serif", 8.25F);
            panelCentral.Location = new Point(200, 125);
            panelCentral.Margin = new Padding(3, 4, 3, 4);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(1080, 775);
            panelCentral.TabIndex = 2;
            panelCentral.Paint += panelCentral_Paint;
            // 
            // PanelControl
            // 
            PanelControl.BackColor = Color.FromArgb(33, 24, 21);
            PanelControl.Controls.Add(guna2Button1);
            PanelControl.CustomizableEdges = customizableEdges3;
            PanelControl.Location = new Point(0, 4);
            PanelControl.Margin = new Padding(3, 4, 3, 4);
            PanelControl.Name = "PanelControl";
            PanelControl.ShadowDecoration.CustomizableEdges = customizableEdges4;
            PanelControl.Size = new Size(1080, 771);
            PanelControl.TabIndex = 0;
            PanelControl.Paint += PanelControl_Paint;
            // 
            // guna2Button1
            // 
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(392, 237);
            guna2Button1.Margin = new Padding(3, 4, 3, 4);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(180, 56);
            guna2Button1.TabIndex = 0;
            guna2Button1.Text = "guna2Button1";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // panelSuperior
            // 
            panelSuperior.BackColor = Color.FromArgb(247, 251, 254);
            panelSuperior.Controls.Add(panel1);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(200, 0);
            panelSuperior.Margin = new Padding(3, 4, 3, 4);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(1080, 125);
            panelSuperior.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Location = new Point(6, 124);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 125);
            panel1.TabIndex = 0;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(33, 24, 21);
            panelMenu.Controls.Add(logo);
            panelMenu.Controls.Add(botonInicio);
            panelMenu.Controls.Add(botonVentas);
            panelMenu.Controls.Add(botonCompras);
            panelMenu.Controls.Add(botonProductos);
            panelMenu.Controls.Add(botonUsuarios);
            panelMenu.Controls.Add(botonClientes);
            panelMenu.Controls.Add(botonProvedores);
            panelMenu.Controls.Add(botonReportes);
            panelMenu.Controls.Add(BotonMantenedor);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Margin = new Padding(3, 4, 3, 4);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(200, 900);
            panelMenu.TabIndex = 0;
            panelMenu.Paint += panelMenu_Paint;
            // 
            // logo
            // 
            logo.Location = new Point(3, 4);
            logo.Margin = new Padding(3, 4, 3, 4);
            logo.Name = "logo";
            logo.Padding = new Padding(10, 12, 10, 12);
            logo.Size = new Size(197, 121);
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.TabIndex = 0;
            logo.TabStop = false;
            logo.Click += logo_Click;
            // 
            // botonInicio
            // 
            botonInicio.FlatAppearance.BorderSize = 0;
            botonInicio.FlatStyle = FlatStyle.Flat;
            botonInicio.ForeColor = Color.FromArgb(253, 204, 111);
            botonInicio.IconChar = IconChar.HomeLg;
            botonInicio.IconColor = Color.FromArgb(253, 204, 111);
            botonInicio.IconFont = IconFont.Auto;
            botonInicio.ImageAlign = ContentAlignment.MiddleLeft;
            botonInicio.Location = new Point(3, 133);
            botonInicio.Margin = new Padding(3, 4, 3, 4);
            botonInicio.Name = "botonInicio";
            botonInicio.Size = new Size(200, 56);
            botonInicio.TabIndex = 9;
            botonInicio.Text = "inicio";
            botonInicio.TextAlign = ContentAlignment.MiddleLeft;
            botonInicio.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonInicio.UseVisualStyleBackColor = true;
            botonInicio.Click += botonInicio_Click_1;
            // 
            // botonVentas
            // 
            botonVentas.Dock = DockStyle.Top;
            botonVentas.FlatAppearance.BorderSize = 0;
            botonVentas.FlatStyle = FlatStyle.Flat;
            botonVentas.ForeColor = Color.FromArgb(253, 204, 111);
            botonVentas.IconChar = IconChar.Store;
            botonVentas.IconColor = Color.FromArgb(253, 204, 111);
            botonVentas.IconFont = IconFont.Auto;
            botonVentas.ImageAlign = ContentAlignment.MiddleLeft;
            botonVentas.Location = new Point(3, 197);
            botonVentas.Margin = new Padding(3, 4, 3, 4);
            botonVentas.Name = "botonVentas";
            botonVentas.Size = new Size(190, 56);
            botonVentas.TabIndex = 2;
            botonVentas.Text = "Ventas";
            botonVentas.TextAlign = ContentAlignment.MiddleLeft;
            botonVentas.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonVentas.UseVisualStyleBackColor = true;
            botonVentas.Click += botonVentas_Click_1;
            // 
            // botonCompras
            // 
            botonCompras.FlatAppearance.BorderSize = 0;
            botonCompras.FlatStyle = FlatStyle.Flat;
            botonCompras.ForeColor = Color.FromArgb(253, 204, 111);
            botonCompras.IconChar = IconChar.ShoppingBag;
            botonCompras.IconColor = Color.FromArgb(253, 204, 111);
            botonCompras.IconFont = IconFont.Auto;
            botonCompras.ImageAlign = ContentAlignment.MiddleLeft;
            botonCompras.Location = new Point(3, 261);
            botonCompras.Margin = new Padding(3, 4, 3, 4);
            botonCompras.Name = "botonCompras";
            botonCompras.Size = new Size(190, 56);
            botonCompras.TabIndex = 0;
            botonCompras.Text = "Compras";
            botonCompras.TextAlign = ContentAlignment.MiddleLeft;
            botonCompras.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonCompras.UseVisualStyleBackColor = true;
            botonCompras.Click += botonCompras_Click;
            // 
            // botonProductos
            // 
            botonProductos.FlatAppearance.BorderSize = 0;
            botonProductos.FlatStyle = FlatStyle.Flat;
            botonProductos.ForeColor = Color.FromArgb(253, 204, 111);
            botonProductos.IconChar = IconChar.Box;
            botonProductos.IconColor = Color.FromArgb(253, 204, 111);
            botonProductos.IconFont = IconFont.Auto;
            botonProductos.ImageAlign = ContentAlignment.MiddleLeft;
            botonProductos.Location = new Point(3, 325);
            botonProductos.Margin = new Padding(3, 4, 3, 4);
            botonProductos.Name = "botonProductos";
            botonProductos.Size = new Size(190, 56);
            botonProductos.TabIndex = 3;
            botonProductos.Text = "Productos";
            botonProductos.TextAlign = ContentAlignment.MiddleLeft;
            botonProductos.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonProductos.UseVisualStyleBackColor = true;
            botonProductos.Click += botonProductos_Click;
            // 
            // botonUsuarios
            // 
            botonUsuarios.FlatAppearance.BorderSize = 0;
            botonUsuarios.FlatStyle = FlatStyle.Flat;
            botonUsuarios.ForeColor = Color.FromArgb(253, 204, 111);
            botonUsuarios.IconChar = IconChar.UserPen;
            botonUsuarios.IconColor = Color.FromArgb(253, 204, 111);
            botonUsuarios.IconFont = IconFont.Auto;
            botonUsuarios.ImageAlign = ContentAlignment.MiddleLeft;
            botonUsuarios.Location = new Point(3, 389);
            botonUsuarios.Margin = new Padding(3, 4, 3, 4);
            botonUsuarios.Name = "botonUsuarios";
            botonUsuarios.Size = new Size(190, 56);
            botonUsuarios.TabIndex = 5;
            botonUsuarios.Text = "Usuarios";
            botonUsuarios.TextAlign = ContentAlignment.MiddleLeft;
            botonUsuarios.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonUsuarios.UseVisualStyleBackColor = true;
            // 
            // botonClientes
            // 
            botonClientes.FlatAppearance.BorderSize = 0;
            botonClientes.FlatStyle = FlatStyle.Flat;
            botonClientes.ForeColor = Color.FromArgb(253, 204, 111);
            botonClientes.IconChar = IconChar.Users;
            botonClientes.IconColor = Color.FromArgb(253, 204, 111);
            botonClientes.IconFont = IconFont.Auto;
            botonClientes.ImageAlign = ContentAlignment.MiddleLeft;
            botonClientes.Location = new Point(3, 453);
            botonClientes.Margin = new Padding(3, 4, 3, 4);
            botonClientes.Name = "botonClientes";
            botonClientes.Size = new Size(190, 56);
            botonClientes.TabIndex = 6;
            botonClientes.Text = "Clientes";
            botonClientes.TextAlign = ContentAlignment.MiddleLeft;
            botonClientes.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonClientes.UseVisualStyleBackColor = true;
            // 
            // botonProvedores
            // 
            botonProvedores.FlatAppearance.BorderSize = 0;
            botonProvedores.FlatStyle = FlatStyle.Flat;
            botonProvedores.ForeColor = Color.FromArgb(253, 204, 111);
            botonProvedores.IconChar = IconChar.Truck;
            botonProvedores.IconColor = Color.FromArgb(253, 204, 111);
            botonProvedores.IconFont = IconFont.Auto;
            botonProvedores.ImageAlign = ContentAlignment.MiddleLeft;
            botonProvedores.Location = new Point(3, 517);
            botonProvedores.Margin = new Padding(3, 4, 3, 4);
            botonProvedores.Name = "botonProvedores";
            botonProvedores.Size = new Size(190, 56);
            botonProvedores.TabIndex = 8;
            botonProvedores.Text = "Provedores";
            botonProvedores.TextAlign = ContentAlignment.MiddleLeft;
            botonProvedores.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonProvedores.UseVisualStyleBackColor = true;
            // 
            // botonReportes
            // 
            botonReportes.FlatAppearance.BorderSize = 0;
            botonReportes.FlatStyle = FlatStyle.Flat;
            botonReportes.ForeColor = Color.FromArgb(253, 204, 111);
            botonReportes.IconChar = IconChar.ChartLine;
            botonReportes.IconColor = Color.FromArgb(253, 204, 111);
            botonReportes.IconFont = IconFont.Auto;
            botonReportes.ImageAlign = ContentAlignment.MiddleLeft;
            botonReportes.Location = new Point(3, 581);
            botonReportes.Margin = new Padding(3, 4, 3, 4);
            botonReportes.Name = "botonReportes";
            botonReportes.Size = new Size(190, 56);
            botonReportes.TabIndex = 7;
            botonReportes.Text = "Reportes";
            botonReportes.TextAlign = ContentAlignment.MiddleLeft;
            botonReportes.TextImageRelation = TextImageRelation.ImageBeforeText;
            botonReportes.UseVisualStyleBackColor = true;
            botonReportes.Click += botonReportes_Click;
            // 
            // BotonMantenedor
            // 
            BotonMantenedor.FlatAppearance.BorderSize = 0;
            BotonMantenedor.FlatStyle = FlatStyle.Flat;
            BotonMantenedor.ForeColor = Color.FromArgb(253, 204, 111);
            BotonMantenedor.IconChar = IconChar.Database;
            BotonMantenedor.IconColor = Color.FromArgb(253, 204, 111);
            BotonMantenedor.IconFont = IconFont.Auto;
            BotonMantenedor.ImageAlign = ContentAlignment.MiddleLeft;
            BotonMantenedor.Location = new Point(3, 645);
            BotonMantenedor.Margin = new Padding(3, 4, 3, 4);
            BotonMantenedor.Name = "BotonMantenedor";
            BotonMantenedor.Size = new Size(190, 56);
            BotonMantenedor.TabIndex = 6;
            BotonMantenedor.Text = "Backup";
            BotonMantenedor.TextAlign = ContentAlignment.MiddleLeft;
            BotonMantenedor.TextImageRelation = TextImageRelation.ImageBeforeText;
            BotonMantenedor.UseVisualStyleBackColor = true;
            BotonMantenedor.Click += iconButton1_Click;
            // 
            // panelVentas
            // 
            panelVentas.BackColor = Color.FromArgb(25, 18, 16);
            panelVentas.Location = new Point(3, 208);
            panelVentas.Name = "panelVentas";
            panelVentas.Size = new Size(200, 71);
            panelVentas.TabIndex = 10;
            panelVentas.Visible = false;
            // 
            // panelCompra
            // 
            panelCompra.BackColor = Color.FromArgb(25, 18, 16);
            panelCompra.Location = new Point(3, 336);
            panelCompra.Name = "panelCompra";
            panelCompra.Size = new Size(200, 71);
            panelCompra.TabIndex = 11;
            panelCompra.Visible = false;
            // 
            // Inicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(253, 221, 200);
            ClientSize = new Size(1280, 900);
            Controls.Add(PanelContenedor);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Inicio";
            Text = "Form1";
            Load += Inicio_Load;
            PanelContenedor.ResumeLayout(false);
            panelCentral.ResumeLayout(false);
            PanelControl.ResumeLayout(false);
            panelSuperior.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logo).EndInit();
            ResumeLayout(false);

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
        private FontAwesome.Sharp.IconButton botonUsuarios;
        private FontAwesome.Sharp.IconButton botonClientes;
        private FontAwesome.Sharp.IconButton botonReportes;
        private FontAwesome.Sharp.IconButton botonProvedores;
        private FontAwesome.Sharp.IconButton botonInicio;
        private FontAwesome.Sharp.IconButton BotonMantenedor;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Panel PanelControl;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}

