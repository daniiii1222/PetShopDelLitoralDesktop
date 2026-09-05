using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShopDelLitoral
{
   
    public partial class FrmPersonas : Form
    {
        private string tipoSeleccionado = "";
        public FrmPersonas()
        {
            InitializeComponent();
        }

        private void PanelPrincipal_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModifPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PanelListaCateg_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PanelCategorias_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iBSearch_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void LBProveedores_Click(object sender, EventArgs e)
        {

        }

        private void proveedoresPanel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void botonUsuario_Click(object sender, EventArgs e)
        {
            panelUsuarios.Visible = true;
        }

        private void guna2CustomGradientPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            tipoSeleccionado = "Cliente";

            ConfigurarGridClientes();
            CargarClientes();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            tipoSeleccionado = "Proveedor";

            ConfigurarGridProveedores();
           // CargarProveedores();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            tipoSeleccionado = "Usuario";

            ConfigurarGridUsuarios();
           // CargarUsuarios();
        }

        private void ConfigurarGridClientes()
        {
            dgvPersonas.Columns.Clear();

            dgvPersonas.AutoGenerateColumns = false;

            dgvPersonas.Columns.Add("Id", "ID");
            dgvPersonas.Columns.Add("Nombre", "Nombre");
            dgvPersonas.Columns.Add("Apellido", "Apellido");
            dgvPersonas.Columns.Add("Correo", "Correo");
            dgvPersonas.Columns.Add("Telefono", "Teléfono");
            dgvPersonas.Columns.Add("Direccion", "Dirección");

            AgregarColumnasAcciones();

            dgvPersonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarGridProveedores()
        {
            dgvPersonas.Columns.Clear();

            dgvPersonas.AutoGenerateColumns = false;

            dgvPersonas.Columns.Add("Id", "ID");
            dgvPersonas.Columns.Add("Nombre", "Nombre");
            dgvPersonas.Columns.Add("Apellido", "Apellido");
            dgvPersonas.Columns.Add("Correo", "Correo");
            dgvPersonas.Columns.Add("Telefono", "Teléfono");
            dgvPersonas.Columns.Add("Direccion", "Dirección");

            AgregarColumnasAcciones();

            dgvPersonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigurarGridUsuarios()
        {
            dgvPersonas.Columns.Clear();

            dgvPersonas.AutoGenerateColumns = false;

            dgvPersonas.Columns.Add("Id", "ID");
            dgvPersonas.Columns.Add("Nombre", "Nombre");
            dgvPersonas.Columns.Add("Apellido", "Apellido");
            dgvPersonas.Columns.Add("Correo", "Correo");
            dgvPersonas.Columns.Add("Telefono", "Teléfono");
            dgvPersonas.Columns.Add("Rol", "Rol");
            dgvPersonas.Columns.Add("Estado", "Estado");

            AgregarColumnasAcciones();

            dgvPersonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void AgregarColumnasAcciones()
        {
            DataGridViewButtonColumn btnModificar = new DataGridViewButtonColumn();

            btnModificar.Name = "Modificar";
            btnModificar.HeaderText = "Modificar";
            btnModificar.Text = "✏";
            btnModificar.UseColumnTextForButtonValue = true;

            dgvPersonas.Columns.Add(btnModificar);


            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();

            btnEliminar.Name = "Eliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "🗑";
            btnEliminar.UseColumnTextForButtonValue = true;

            dgvPersonas.Columns.Add(btnEliminar);
        }

        private void dgvPersonas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string columna = dgvPersonas.Columns[e.ColumnIndex].Name;

            int id = Convert.ToInt32(
                dgvPersonas.Rows[e.RowIndex].Cells["Id"].Value
            );

            if (columna == "Modificar")
            {
                ModificarPersona(id);
            }

            if (columna == "Eliminar")
            {
                EliminarPersona(id);
            }
        }

        private void ModificarPersona(int id)
        {
            MessageBox.Show(
                $"Modificar {tipoSeleccionado} con ID: {id}"
            );
        }

        private void EliminarPersona(int id)
        {
            DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de eliminar esta persona?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show(
                    $"Se eliminará {tipoSeleccionado} con ID {id}"
                );

                // Acá posteriormente llamamos a Negocio
                // para eliminar realmente el registro.
            }
        }

        private void CargarClientes()
        {
            dgvPersonas.Rows.Clear();

            dgvPersonas.Rows.Add(
                1,
                "Juan",
                "Pérez",
                "juan@gmail.com",
                "3791234567",
                "Corrientes"
            );

            dgvPersonas.Rows.Add(
                2,
                "María",
                "Gómez",
                "maria@gmail.com",
                "3794567890",
                "Corrientes"
            );

            dgvPersonas.Rows.Add(
                3,
                "Carlos",
                "López",
                "carlos@gmail.com",
                "3795678912",
                "Resistencia"
            );
        }
    }
}
