using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 


namespace PetShopDelLitoral
{
    public partial class Inicio : Form
        
    {
        private PrivateFontCollection pfc = new PrivateFontCollection();
        public Inicio()
        {
         InitializeComponent();
         CargarFuentePoppins();
        }



        private void CargarFuentePoppins()
        {
            try
            {
                byte[] fontData = Properties.Resources.Poppins_Regular;
                IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);

                Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                pfc.AddMemoryFont(fontPtr, fontData.Length);
                Marshal.FreeCoTaskMem(fontPtr);

             
                Font fuentePoppins = new Font(pfc.Families[0], 12f, FontStyle.Regular);
                panelMenu.Font = fuentePoppins;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la fuente: " + ex.Message);
            }
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }


        private void PanelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void botonInicio_Click(object sender, EventArgs e)
        {

        }

    

        private void botonVentas_Click(object sender, EventArgs e)
        {

        }

        private void botonVentas_Click_1(object sender, EventArgs e)
        {
            
        }

        private void botonProductos_Click(object sender, EventArgs e)
        {

            this.panelCentral.Controls.Clear();

            FrmProductos frm = new FrmProductos();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;


            this.panelCentral.Controls.Add(frm);

            frm.Show();
        }

        private void botonRopa_Click(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void logo_Click(object sender, EventArgs e)
        {

        }

        private void panelImagen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelImagen_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void botonCompras_Click(object sender, EventArgs e)
        {
            panelCompra.Visible = !panelCompra.Visible;
        }

        private void botonInicio_Click_1(object sender, EventArgs e)
        {

        }

        private void panelCentral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtProveedor_Click(object sender, EventArgs e)
        {
            this.panelCentral.Controls.Clear();

            FrmProveedor frm = new FrmProveedor();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;


            this.panelCentral.Controls.Add(frm);

            frm.Show();
        }
    }
}
