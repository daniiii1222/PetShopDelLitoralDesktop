namespace PetShopDelLitoral
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel panel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.LBNumDocumento = new System.Windows.Forms.Label();
            this.LBContraseña = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.BTNIngresar = new FontAwesome.Sharp.IconButton();
            this.BTNCancelar = new FontAwesome.Sharp.IconButton();
            panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.CadetBlue;
            panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(356, 343);
            panel1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Gray;
            this.imageList1.Images.SetKeyName(0, "Logo Pet Shop Alt.png");
            // 
            // LBNumDocumento
            // 
            this.LBNumDocumento.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            this.LBNumDocumento.AllowDrop = true;
            this.LBNumDocumento.AutoSize = true;
            this.LBNumDocumento.Location = new System.Drawing.Point(502, 48);
            this.LBNumDocumento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBNumDocumento.Name = "LBNumDocumento";
            this.LBNumDocumento.Size = new System.Drawing.Size(104, 16);
            this.LBNumDocumento.TabIndex = 1;
            this.LBNumDocumento.Text = "Nro Documento:";
            this.LBNumDocumento.Click += new System.EventHandler(this.label1_Click);
            // 
            // LBContraseña
            // 
            this.LBContraseña.AutoSize = true;
            this.LBContraseña.Location = new System.Drawing.Point(502, 165);
            this.LBContraseña.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBContraseña.Name = "LBContraseña";
            this.LBContraseña.Size = new System.Drawing.Size(79, 16);
            this.LBContraseña.TabIndex = 2;
            this.LBContraseña.Tag = "";
            this.LBContraseña.Text = "Contraseña:";
            this.LBContraseña.Click += new System.EventHandler(this.LBContraseña_Click);
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(505, 92);
            this.txtDni.Margin = new System.Windows.Forms.Padding(4);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(267, 22);
            this.txtDni.TabIndex = 3;
            this.txtDni.TextChanged += new System.EventHandler(this.TxtNombre_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(505, 208);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(267, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // BTNIngresar
            // 
            this.BTNIngresar.AutoSize = true;
            this.BTNIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTNIngresar.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.BTNIngresar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.BTNIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTNIngresar.IconSize = 22;
            this.BTNIngresar.Location = new System.Drawing.Point(505, 274);
            this.BTNIngresar.Margin = new System.Windows.Forms.Padding(4);
            this.BTNIngresar.Name = "BTNIngresar";
            this.BTNIngresar.Size = new System.Drawing.Size(113, 39);
            this.BTNIngresar.TabIndex = 5;
            this.BTNIngresar.Text = "Ingresar";
            this.BTNIngresar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTNIngresar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNIngresar.UseVisualStyleBackColor = true;
            this.BTNIngresar.Click += new System.EventHandler(this.IBIngresar_Click);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.AutoSize = true;
            this.BTNCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTNCancelar.IconChar = FontAwesome.Sharp.IconChar.CircleXmark;
            this.BTNCancelar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.BTNCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTNCancelar.IconSize = 22;
            this.BTNCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNCancelar.Location = new System.Drawing.Point(659, 274);
            this.BTNCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(113, 39);
            this.BTNCancelar.TabIndex = 6;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTNCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.ClientSize = new System.Drawing.Size(929, 343);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNIngresar);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.LBContraseña);
            this.Controls.Add(this.LBNumDocumento);
            this.Controls.Add(panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label LBNumDocumento;
        private System.Windows.Forms.Label LBContraseña;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.TextBox txtPassword;
        private FontAwesome.Sharp.IconButton BTNIngresar;
        private FontAwesome.Sharp.IconButton BTNCancelar;
    }
}


