using Capa_Entidad;
using Capa_Negocio;
using CapaDatos;
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
            this.Resize += (s, e) => AcomodarLayout();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            AcomodarLayout();
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

        private static readonly Font fuenteBotonRapido = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);

        // Acomoda los bloques según el tamaño real de la ventana. El Designer los dejó fijos para 1103 px
        // de ancho, por eso en pantallas más grandes quedaban pegados a la izquierda.
        private void AcomodarLayout()
        {
            if (ClientSize.Width < 400 || ClientSize.Height < 300) return; // minimizado o sin tamaño todavía

            const int margen = 50;          // borde de la ventana
            const int separacion = 30;      // entre bloques
            const int alturaTarjetas = 123;
            const int arriba = 40;

            int anchoCol = (ClientSize.Width - 2 * margen - separacion) / 2;
            int xDer = margen + anchoCol + separacion;
            int yFila2 = arriba + alturaTarjetas + separacion;
            int altoFila2 = Math.Min(Math.Max(220, ClientSize.Height - yFila2 - margen), 450);

            // Fila 1: tarjetas de resumen
            panel1.SetBounds(margen, arriba, anchoCol, alturaTarjetas);
            panel2.SetBounds(xDer, arriba, anchoCol, alturaTarjetas);
            //CentrarTarjeta(panel1, iconPictureBox2, label1, lblVentasHoy);
            //CentrarTarjeta(panel2, iconPictureBox1, label2, label5);

            // Fila 2 izquierda: actividad reciente (la grilla hace de fondo, los labels van encima)
            dataGridView1.SetBounds(margen, yFila2, anchoCol, altoFila2);
            label9.Location = new Point(margen + 15, yFila2 + 12);
            label8.Location = new Point(margen + 15, yFila2 + 52);

            // Fila 2 derecha: acciones rápidas, 4 botones parejos en cuadrícula de 2x2
            panel3.SetBounds(xDer, yFila2, anchoCol, altoFila2);
            label4.Location = new Point(18, 14);

            const int pad = 20;
            const int altoTitulo = 50;
            int bw = (anchoCol - 3 * pad) / 2;
            int bh = Math.Min(130, (altoFila2 - altoTitulo - 3 * pad) / 2);
            int yInicio = altoTitulo + (altoFila2 - altoTitulo - (2 * bh + pad)) / 2; // centrado en el espacio libre

            ColocarBoton(panel4, btnNuevaVenta, pad, yInicio, bw, bh);
            ColocarBoton(panel6, btnNuevoProducto, 2 * pad + bw, yInicio, bw, bh);
            ColocarBoton(panel5, btnNuevoCliente, pad, yInicio + bh + pad, bw, bh);
            ColocarBoton(panel7, btnNuevaCompra, 2 * pad + bw, yInicio + bh + pad, bw, bh);
        }

        // Icono a la izquierda; título y número centrados en el resto de la tarjeta
        private void CentrarTarjeta(Control tarjeta, Control icono, Label titulo, Label valor)
        {
            icono.Location = new Point(40, (tarjeta.Height - icono.Height) / 2);

            int zonaIzq = 40 + icono.Width + 10;
            int zonaAncho = tarjeta.Width - zonaIzq - 30;

            titulo.Location = new Point(zonaIzq + (zonaAncho - titulo.Width) / 2, 18);
            valor.Location = new Point(zonaIzq + (zonaAncho - valor.Width) / 2, 65);
        }

        private void ColocarBoton(Control panel, Control boton, int x, int y, int ancho, int alto)
        {
            panel.SetBounds(x, y, ancho, alto);
            boton.SetBounds(6, 6, ancho - 12, alto - 12);
            boton.Font = fuenteBotonRapido;
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
        
         
    }
}
