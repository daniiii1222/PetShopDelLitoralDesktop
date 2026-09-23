using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LBContraseña_Click(object sender, EventArgs e)
        {

        }

      private void IBIngresar_Click(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            string contrasenia = txtPassword.Text.Trim();

            CN_Usuario objNegocio = new CN_Usuario();
            Usuario objUsuarioLogueado;
            string mensaje;

            bool resultado = objNegocio.ValidarLogin(dni, contrasenia, out objUsuarioLogueado, out mensaje);

            if (resultado)
            {
                Sesion.UsuarioActual = objUsuarioLogueado;

                Inicio frmInicio = new Inicio();
                frmInicio.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(mensaje, "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
     
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtDni.Text = "";
            txtPassword.Text = "";
            this.Show();
        }



        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
          
            
            Application.Exit();
        
        }
    }
}
