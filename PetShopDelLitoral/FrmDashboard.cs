using CapaDatos;
using Capa_Entidad;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
    public partial class FrmDashboard : Form
    {
        private Timer timerReloj;

        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            //MostrarDatosUsuario();
            //ActualizarFechaHora();
            //IniciarTimer();
        }

        /*   private void MostrarDatosUsuario()
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
            */
        /*private void IniciarTimer()
        {
            timerReloj = new Timer();
            timerReloj.Interval = 1000; // Se actualiza cada 1 segundo
            timerReloj.Tick += TimerReloj_Tick;
            timerReloj.Start();
        }
        
        private void TimerReloj_Tick(object sender, EventArgs e)
        {
            ActualizarFechaHora();
        }
        */

        // ==========================================
        // EVENTOS DE LOS BOTONES DE ACCIONES RÁPIDAS
        // ==========================================

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                inicio.NavegarAFormulario(new FrmVentas());
            }
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                inicio.NavegarAFormulario(new FrmProductos());
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                // Sacamos el rol de la sesión activa, igual que en Inicio.cs,
                // para que FrmPersonas sepa qué botones mostrar/ocultar.
                string rol = (Sesion.UsuarioActual != null && Sesion.UsuarioActual.idRol != null)
                    ? Sesion.UsuarioActual.idRol.descripcion_rol.Trim()
                    : "";

                inicio.NavegarAFormulario(new FrmPersonas(rol));
            }
        }

        private void btnNuevaCompra_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is Inicio inicio)
            {
                inicio.NavegarAFormulario(new FrmCompras());
            }
        }

        // ==========================================
        // EVENTOS VARIOS / DIBUJO DE PANELES
        // ==========================================

        private void FrmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerReloj != null)
            {
                timerReloj.Stop();
                timerReloj.Dispose();
            }
        }

        private void lblBienvenida_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void panel8_Paint(object sender, PaintEventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void PanelGeneral_Paint(object sender, PaintEventArgs e) { }
        private void d(object sender, EventArgs e) { }

        // NOTA: Revisá en el Designer (rayo de eventos del botón "Nueva Venta")
        // que el Click esté apuntando a btnNuevaVenta_Click y NO a este método vacío.
        // Si está apuntando acá, el botón compila pero no hace nada.
        private void btnNuevaVenta_Click_1(object sender, EventArgs e)
        {

        }
    }
}