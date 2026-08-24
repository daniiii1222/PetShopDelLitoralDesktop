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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            System.Windows.Forms.Panel panel1;
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.LBNumDocumento = new System.Windows.Forms.Label();
            this.LBContraseña = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.BTNIngresar = new FontAwesome.Sharp.IconButton();
            this.BTNCancelar = new FontAwesome.Sharp.IconButton();
            panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Gray;
            this.imageList1.Images.SetKeyName(0, "Logo Pet Shop Alt.png");
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.CadetBlue;
            panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(332, 343);
            panel1.TabIndex = 0;
            // 
            // LBNumDocumento
            // 
            this.LBNumDocumento.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            this.LBNumDocumento.AllowDrop = true;
            this.LBNumDocumento.AutoSize = true;
            this.LBNumDocumento.Location = new System.Drawing.Point(430, 62);
            this.LBNumDocumento.Name = "LBNumDocumento";
            this.LBNumDocumento.Size = new System.Drawing.Size(104, 16);
            this.LBNumDocumento.TabIndex = 1;
            this.LBNumDocumento.Text = "Nro Documento:";
            this.LBNumDocumento.Click += new System.EventHandler(this.label1_Click);
            // 
            // LBContraseña
            // 
            this.LBContraseña.AutoSize = true;
            this.LBContraseña.Location = new System.Drawing.Point(430, 160);
            this.LBContraseña.Name = "LBContraseña";
            this.LBContraseña.Size = new System.Drawing.Size(79, 16);
            this.LBContraseña.TabIndex = 2;
            this.LBContraseña.Tag = "";
            this.LBContraseña.Text = "Contraseña:";
            this.LBContraseña.Click += new System.EventHandler(this.LBContraseña_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(433, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 22);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(433, 191);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(312, 22);
            this.textBox2.TabIndex = 4;
            // 
            // BTNIngresar
            // 
            this.BTNIngresar.AutoSize = true;
            this.BTNIngresar.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            this.BTNIngresar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.BTNIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTNIngresar.IconSize = 22;
            this.BTNIngresar.Location = new System.Drawing.Point(433, 282);
            this.BTNIngresar.Name = "BTNIngresar";
            this.BTNIngresar.Size = new System.Drawing.Size(121, 32);
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
            this.BTNCancelar.IconChar = FontAwesome.Sharp.IconChar.CircleXmark;
            this.BTNCancelar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.BTNCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTNCancelar.IconSize = 22;
            this.BTNCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNCancelar.Location = new System.Drawing.Point(624, 280);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(121, 34);
            this.BTNCancelar.TabIndex = 6;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BTNCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNCancelar.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.ClientSize = new System.Drawing.Size(929, 343);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNIngresar);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LBContraseña);
            this.Controls.Add(this.LBNumDocumento);
            this.Controls.Add(panel1);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label LBNumDocumento;
        private System.Windows.Forms.Label LBContraseña;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private FontAwesome.Sharp.IconButton BTNIngresar;
        private FontAwesome.Sharp.IconButton BTNCancelar;
    }
}