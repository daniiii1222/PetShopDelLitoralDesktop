using CapaDatos;
using Capa_Entidad;
using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
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
            MostrarDatosUsuario();
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
            catch (Exception)
            {

            }
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            AplicarPermisosPorRol();
            ConfigurarEsteticaMenu();
            AbrirFormulario(new FrmDashboard());
        }

        private void ConfigurarEsteticaMenu()
        {

            this.BackColor = Color.FromArgb(254, 249, 231);


            if (panelSuperior != null)
            {
                panelSuperior.BackColor = Color.FromArgb(34, 24, 21);
            }


            if (panelMenu != null)
            {
                panelMenu.BackColor = Color.FromArgb(34, 24, 21);
            }
        }

        private void AplicarPermisosPorRol()
        {
            if (Sesion.UsuarioActual != null)
            {

                string rol = Sesion.UsuarioActual.idRol != null ? Sesion.UsuarioActual.idRol.descripcion_rol.Trim() : "";


                MessageBox.Show($"Rol detectado en sesión: '{rol}'", "Verificación de Permisos");

                if (rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ||
                    rol.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    SetVisible(botonInicio, true);
                    SetVisible(botonVentas, true);
                    SetVisible(botonCompras, true);
                    SetVisible(botonProductos, true);
                    SetVisible(botonPersonas, true);
                    SetVisible(botonReportes, true);
                    SetVisible(btnBackup, true);
                }
                else if (rol.Equals("Supervisor", StringComparison.OrdinalIgnoreCase))
                {
                    SetVisible(botonInicio, true);
                    SetVisible(botonVentas, true);
                    SetVisible(botonCompras, true);
                    SetVisible(botonProductos, true);
                    SetVisible(botonPersonas, true);
                    SetVisible(botonReportes, false);
                    SetVisible(btnBackup, false);
                }
                else if (rol.Equals("Vendedor", StringComparison.OrdinalIgnoreCase))
                {
                    SetVisible(botonInicio, true);
                    SetVisible(botonVentas, true);
                    SetVisible(botonProductos, true);
                    SetVisible(botonPersonas, true);

                    SetVisible(botonCompras, false);
                    SetVisible(botonReportes, true);
                    SetVisible(btnBackup, false);
                }
                else
                {
                    SetVisible(botonInicio, true);
                    SetVisible(botonVentas, true);
                    SetVisible(botonProductos, true);

                    SetVisible(botonPersonas, false);
                    SetVisible(botonCompras, false);
                    SetVisible(botonReportes, false);
                    SetVisible(btnBackup, false);
                }
            }
            else
            {
                MessageBox.Show("Sesion.UsuarioActual es NULL", "Error de Sesión");
            }
        }

        private void SetVisible(Control control, bool visible)
        {
            if (control != null)
            {
                control.Visible = visible;
            }
        }

        private void botonVentas_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmVentas());
        }

        private void botonProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProductos());
        }

        private void botonCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCompras());
        }

        private void botonReportes_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual != null && Sesion.UsuarioActual.idRol != null)
            {
                // Traemos el rol de la sesión actual
                string rol = Sesion.UsuarioActual.idRol.descripcion_rol.Trim();

                // Derivamos según el perfil
                if (rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ||
                    rol.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    AbrirFormulario(new FrmReportesAdministrador());
                }
                else if (rol.Equals("Vendedor", StringComparison.OrdinalIgnoreCase))
                {
                    AbrirFormulario(new FrmReportesVendedor());
                }
            }
        }

        private void btnPersonas_click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmPersonas());
        }

        private void botonInicio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmDashboard());
        }

        private void AbrirFormulario(Form formulario)
        {
            foreach (Control control in this.panelCentral.Controls)
            {
                if (control is Form formPrevio)
                {
                    formPrevio.Close();
                    formPrevio.Dispose();
                }
            }

            this.panelCentral.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            this.panelCentral.Controls.Add(formulario);
            formulario.Show();
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login frmLogin = new Login();
            frmLogin.Show();
        }


        private void panelSuperior_Paint(object sender, PaintEventArgs e) { }
        private void PanelControl_Paint(object sender, PaintEventArgs e) { }
        private void panelMenu_Paint(object sender, PaintEventArgs e) { }

       
        private void PanelContenedor_Paint(object sender, PaintEventArgs e) { }
        private void panelCentral_Paint(object sender, PaintEventArgs e) { }
        private void logo_Click(object sender, EventArgs e) { }
        private void botonInicio_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmDashboard());
        }

        private void btnBackup_Click_2(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmBackup());
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void MostrarDatosUsuario()
        {
            if (Sesion.UsuarioActual != null)
            {
                // Verifica si la propiedad de la persona tiene datos
                if (Sesion.UsuarioActual.idPersona != null)
                {
                    string nombre = Sesion.UsuarioActual.idPersona.nombre_persona;
                    string apellido = Sesion.UsuarioActual.idPersona.apellido_persona;

                    // Asigna el nombre a tu Label del encabezado (asegúrate de que el Name en el diseñador coincida)
                    if (lblBienvenida != null)
                    {
                        lblBienvenida.Text = $"Bienvenido/a, {nombre} {apellido}";
                    }
                }
                else
                {
                    if (lblBienvenida != null)
                    {
                        lblBienvenida.Text = "Bienvenido/a al Sistema";
                    }
                }
            }
        }

        private void ActualizarFechaHora()
        {
            if (lblFecha != null)
            {

                lblFecha.Text = DateTime.Now.ToString("dddd, d 'de' MMMM 'de' yyyy, hh:mm tt");
            }
        }

    }
}