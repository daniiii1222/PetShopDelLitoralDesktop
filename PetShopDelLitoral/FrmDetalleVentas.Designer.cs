namespace PetShopDelLitoral
{
    partial class FrmDetalleVentas
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
            this.PanelTituloSec = new Guna.UI2.WinForms.Guna2Panel();
            this.panelSecDetalle = new Guna.UI2.WinForms.Guna2Panel();
            this.SuspendLayout();
            // 
            // PanelTituloSec
            // 
            this.PanelTituloSec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(24)))), ((int)(((byte)(21)))));
            this.PanelTituloSec.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTituloSec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.PanelTituloSec.Location = new System.Drawing.Point(0, 0);
            this.PanelTituloSec.Name = "PanelTituloSec";
            this.PanelTituloSec.Size = new System.Drawing.Size(1062, 72);
            this.PanelTituloSec.TabIndex = 0;
            // 
            // panelSecDetalle
            // 
            this.panelSecDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(204)))), ((int)(((byte)(110)))));
            this.panelSecDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSecDetalle.Location = new System.Drawing.Point(0, 72);
            this.panelSecDetalle.Name = "panelSecDetalle";
            this.panelSecDetalle.Size = new System.Drawing.Size(1062, 501);
            this.panelSecDetalle.TabIndex = 1;
            // 
            // FrmDetalleVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 573);
            this.Controls.Add(this.panelSecDetalle);
            this.Controls.Add(this.PanelTituloSec);
            this.Name = "FrmDetalleVentas";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmDetalleVentas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel PanelTituloSec;
        private Guna.UI2.WinForms.Guna2Panel panelSecDetalle;
    }
}