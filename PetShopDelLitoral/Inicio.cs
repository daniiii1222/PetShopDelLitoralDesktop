using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
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

                IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);

                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(
                        fontData,
                        0,
                        fontPtr,
                        fontData.Length
                    );

                    pfc.AddMemoryFont(fontPtr, fontData.Length);

                    // Usar el nombre de la familia al crear la fuente (mejor compatibilidad)
                    string fam = pfc.Families[0].Name;
                    Font nueva = new Font(fam, 14f, FontStyle.Regular, GraphicsUnit.Point);

                    // Asignar a boton y aplicar recursivamente al panel del menú
                    botonInicio.Font = nueva;
                    botonInicio.FlatStyle = FlatStyle.Flat;
                    botonInicio.UseVisualStyleBackColor = false;
                    botonInicio.Refresh();
                    botonInicio.Update();

                    // Aplicar la fuente a todos los controles del panelMenu (opcional)
                    ApplyFont(this.panelMenu, nueva);

                    // Asegurar estilos específicos y orden de tabulación
                    try
                    {
                        // Forzar uso de colores personalizados (evita que UseVisualStyleBackColor los reemplace)
                        botonVentas.UseVisualStyleBackColor = false;
                        botonVentas.FlatStyle = FlatStyle.Flat;
                        botonVentas.Font = nueva;

                        botonCompras.UseVisualStyleBackColor = false;
                        botonCompras.FlatStyle = FlatStyle.Flat;
                        botonCompras.Font = nueva;

                        // Establecer un orden de tabulación coherente
                        botonInicio.TabIndex = 0;
                        botonVentas.TabIndex = 1;
                        botonCompras.TabIndex = 2;
                    }
                    catch { }
                }
                finally
                {
                    System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la fuente: " + ex.Message);
            }
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        // Aplica la fuente recursivamente a los controles hijos
        private void ApplyFont(Control parent, Font font)
        {
            if (parent == null || font == null) return;
            foreach (Control c in parent.Controls)
            {
                try
                {
                    c.Font = font;
                }
                catch { }
                // Recursión
                ApplyFont(c, font);
            }
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
    }
}
