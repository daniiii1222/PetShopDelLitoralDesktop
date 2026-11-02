using System;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    // Ventana que pide DNI y contraseña. Se usa para autorizar la restauración de la base.
    // El diseño visual está en FrmAutorizacion.Designer.cs (se edita con el diseñador de Visual Studio).
    public partial class FrmAutorizacion : Form
    {
        public string Dni { get { return txtDni.Text.Trim(); } }
        public string Clave { get { return txtClave.Text; } }

        // Constructor sin parámetros: lo necesita el diseñador de Visual Studio para abrir el formulario
        public FrmAutorizacion()
        {
            InitializeComponent();
        }

        public FrmAutorizacion(string titulo, string detalle) : this()
        {
            Text = titulo;
            lblTitulo.Text = titulo;
            lblDetalle.Text = detalle;
        }

        // El DNI solo admite números
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtDni.Text.Trim().Length == 0 || txtClave.Text.Length == 0)
            {
                MessageBox.Show("Completá el DNI y la contraseña.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
