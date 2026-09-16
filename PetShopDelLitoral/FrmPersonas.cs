using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PetShopDelLitoral
{

    public partial class FrmPersonas : Form
    {

        private int idSeleccionado = 0;
        private int idPersonaSeleccionada = 0;
        private string tipoSeleccionado = "";
      
        private string rolUsuarioActual = "";
        public FrmPersonas(string rolLogueado)
        {
            InitializeComponent();
        }

        // 1. El que necesita el sistema ahora mismo para no tirar error al compilar
        public FrmPersonas()
        {
            InitializeComponent();
            rolUsuarioActual = "Administrador"; // Forzamos un rol para que puedas ver tu diseño al probar
        }

        /* 2. El que vas a usar oficialmente cuando unas tu código con el de tu compañera
        public FrmPersonas(string rolLogueado)
        {
            InitializeComponent();
            rolUsuarioActual = rolLogueado;
        }
        */
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
            tipoSeleccionado = "Usuario";
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
            CargarUsuarios();
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

            dgvPersonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Asignamos anchos fijos a las columnas pequeñas
            dgvPersonas.Columns["Id"].Width = 30;
            dgvPersonas.Columns["Estado"].Width = 60;
            dgvPersonas.Columns["Telefono"].Width = 80;
            dgvPersonas.Columns["Rol"].Width = 80;

            // Los botones también van fijos
            dgvPersonas.Columns["Modificar"].Width = 40;
            dgvPersonas.Columns["Eliminar"].Width = 40;

            // Dejamos que las columnas de texto largo se expandan
            dgvPersonas.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPersonas.Columns["Apellido"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPersonas.Columns["Correo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

            // Columnas ocultas necesarias para recuperar datos al editar

            dgvPersonas.Columns.Add("IdPersona", "IdPersona");
            dgvPersonas.Columns["IdPersona"].Visible = false;

            dgvPersonas.Columns.Add("Direccion", "Dirección");
            dgvPersonas.Columns["Direccion"].Visible = false;

            dgvPersonas.Columns.Add("Dni", "DNI");
            dgvPersonas.Columns["Dni"].Visible = false;

            dgvPersonas.Columns.Add("IdRol", "IdRol");
            dgvPersonas.Columns["IdRol"].Visible = false;

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
            idSeleccionado = id;

            // Mostramos el panel de usuario porque estamos editando a un usuario
            panelUsuarios.Visible = true;

            foreach (DataGridViewRow fila in dgvPersonas.Rows)
            {
                if (Convert.ToInt32(fila.Cells["Id"].Value) == id)
                {
                    idPersonaSeleccionada = Convert.ToInt32(fila.Cells["IdPersona"].Value);
                    // Datos personales básicos
                    TBNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                    TBApellido.Text = fila.Cells["Apellido"].Value?.ToString();
                    TBCorreo.Text = fila.Cells["Correo"].Value?.ToString();
                    TBTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
                    TBDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
                    TBDni.Text = fila.Cells["Dni"].Value?.ToString();

                    // 1. EL ROL: Se selecciona automáticamente usando la columna oculta "IdRol"
                    if (fila.Cells["IdRol"].Value != null)
                    {
                        CBRol.SelectedValue = Convert.ToInt32(fila.Cells["IdRol"].Value);
                    }

                    // 2. LA CONTRASEÑA: Siempre se limpia a propósito por seguridad
                    TBContraseniaUsuario.Clear();

                    break;
                }
            }
        }

        private void EliminarPersona(int id)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea dar de baja a este usuario?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                CN_Usuario objNegocio = new CN_Usuario();
                string mensaje = string.Empty;

                bool exito = objNegocio.EliminarUsuario(id, out mensaje);

                if (exito)
                {
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargamos la grilla para que desaparezca visualmente al instante
                    ConfigurarGridUsuarios();
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        private void CargarUsuarios()
        {
            try
            {
                dgvPersonas.Rows.Clear();

                CN_Usuario objNegocio = new CN_Usuario();
                DataSet ds = objNegocio.ListarUsuarios();


                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables["TablaUsuarios"];

                    foreach (DataRow row in dt.Rows)
                    {
                        int id = Convert.ToInt32(row["idUsuario"]);
                        int idPersona = Convert.ToInt32(row["idPersona"]);
                        string nombre = row["nombre_persona"].ToString();
                        string apellido = row["apellido_persona"].ToString();
                        string correo = row["correo_persona"].ToString();
                        string telefono = row["telefono_persona"].ToString();
                        string rol = row["descripcion_rol"].ToString();
                        int idRol = Convert.ToInt32(row["idRol"]);
                        // Asegurate de que tu SP devuelva estas columnas
                        string direccion = row["direccion_persona"].ToString();
                        string dni = row["dni_persona"].ToString();

                        bool estadoBool = Convert.ToBoolean(row["estado_usuario"]);
                        string estado = estadoBool ? "Activo" : "Inactivo";

                        // Añadimos respetando el orden exacto de las columnas creadas
                        dgvPersonas.Rows.Add(id, nombre, apellido, correo, telefono, rol, estado, idPersona, direccion, dni, idRol);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarGrilla()
        {
            try
            {
                // 1. Limpiamos selección para evitar conflictos de WinForms
                dgvPersonas.CurrentCell = null;
                dgvPersonas.ClearSelection();

                string textoBuscado = txtBuscar.Text.ToLower();

                foreach (DataGridViewRow fila in dgvPersonas.Rows)
                {
                    if (fila.IsNewRow) continue;

                    // 2. LEEMOS POR ÍNDICE (Número de columna) en vez de por nombre de texto.
                    // Asegurate que el orden de los números coincida con tus columnas en la grilla:
                    // 0: ID, 1: Nombre, 2: Apellido, 3: Correo, 4: Teléfono, 5: Dirección, etc.
                    string nombre = fila.Cells[1].Value?.ToString().ToLower() ?? "";
                    string apellido = fila.Cells[2].Value?.ToString().ToLower() ?? "";

                    // Buscamos la celda de estado (cambiá el número '6' o el que corresponda según en qué columna tengas el Estado)
                    string estadoFila = fila.Cells[6].Value?.ToString() ?? "";

                    bool coincideTexto = nombre.Contains(textoBuscado) || apellido.Contains(textoBuscado);
                    bool coincideEstado = false;

                    // Lógica de los botones
                    if (btVerInactivos.Text == "Ver Activos")
                    {
                        coincideEstado = (estadoFila == "Inactivo");
                    }
                    else if (btVerInactivos.Text == "Ver Inactivos")
                    {
                        coincideEstado = (estadoFila == "Activo");
                    }
                    else // Si dice "Mostrar Todo" o cualquier otro estado del botón Todos
                    {
                        coincideEstado = true;
                    }

                    fila.Visible = coincideTexto && coincideEstado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el filtro: " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FrmPersonas_Load(object sender, EventArgs e)
        {
            // 1. Ocultamos los botones por defecto 
            btnUsuario.Visible = false;
            btntipoProveedor.Visible = false;
            btnCliente.Visible = false;

            // 2. Evaluamos qué mostrar según el rol
            if (rolUsuarioActual == "Administrador")
            {
                btnUsuario.Visible = true;
                btntipoProveedor.Visible = true;
                btnCliente.Visible = true;

                // El Admin ve Usuarios por defecto al entrar
                tipoSeleccionado = "Usuario";
                ConfigurarGridUsuarios();
                CargarUsuarios();
            }
            else if (rolUsuarioActual == "Supervisor")
            {
                btntipoProveedor.Visible = true;
                btnCliente.Visible = true;

                // El Supervisor arranca viendo la grilla de Clientes
                tipoSeleccionado = "Cliente";
                ConfigurarGridClientes();
                CargarClientes();
            }
            else if (rolUsuarioActual == "Vendedor")
            {
                btnCliente.Visible = true;

                // El Vendedor arranca viendo la grilla de Clientes
                tipoSeleccionado = "Cliente";
                ConfigurarGridClientes();
                CargarClientes();
            }

            CargarRoles();
            btVerInactivos.Text = "Ver Inactivos";
            FiltrarGrilla();
        }

        private void LimpiarCampos()
        {

            TBNombre.Clear();
            TBApellido.Clear();
            TBCorreo.Clear();
            TBTelefono.Clear();
            TBDireccion.Clear();
            TBContraseniaUsuario.Clear();
            TBDni.Clear();



            if (CBRol != null && CBRol.Items.Count > 0)
            {
                CBRol.SelectedIndex = -1;
            }

            idSeleccionado = 0;
        }


        private void botonGuardarRegistro_Click(object sender, EventArgs e)
        {
            if (tipoSeleccionado == "Usuario")
            {

                if (string.IsNullOrWhiteSpace(TBNombre.Text) ||
                string.IsNullOrWhiteSpace(TBApellido.Text) ||
                string.IsNullOrWhiteSpace(TBDni.Text) ||
                CBRol.SelectedIndex == -1) // -1 significa que no eligió nada en el combo
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios (Nombre, Apellido, DNI y Rol).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Corta la ejecución, no va a la base de datos
                }

                // Valida que el DNI sea numérico
                if (!long.TryParse(TBDni.Text.Trim(), out _))
                {
                    MessageBox.Show("El campo DNI solo puede contener números válidos.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Valida la contraseña (Solo obligatoria si es un usuario NUEVO)
                if (idSeleccionado == 0 && string.IsNullOrWhiteSpace(TBContraseniaUsuario.Text))
                {
                    MessageBox.Show("Debe ingresar una contraseña para registrar un nuevo usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                try
                {
                    Persona objPersona = new Persona()
                    {
                        // Si estás editando, asignale el idPersona que capturaste de la celda oculta de la grilla
                        idPersona = idPersonaSeleccionada, // variable global 
                        nombre_persona = TBNombre.Text.Trim(),
                        apellido_persona = TBApellido.Text.Trim(),
                        dni_persona = TBDni.Text.Trim(),
                        correo_persona = TBCorreo.Text.Trim(),
                        telefono_persona = TBTelefono.Text.Trim(),
                        direccion_persona = TBDireccion.Text.Trim(),
                        estado_persona = true
                    };

                    Rol objRol = new Rol()
                    {
                        idRol = Convert.ToInt32(CBRol.SelectedValue)
                    };

                    Usuario objUsuario = new Usuario()
                    {
                        idUsuario = idSeleccionado,
                        contrasenia_usuario = TBContraseniaUsuario.Text.Trim(),
                        idRol = objRol,
                        estado_usuario = true
                    };

                    CN_Usuario objNegocio = new CN_Usuario();
                    string mensaje = string.Empty;
                    bool resultado = false;

                    // SEMÁFORO: Si idSeleccionado es 0, registra. Si tiene valor, modifica.
                    if (idSeleccionado == 0)
                    {
                        // Al registrar, la contraseña es obligatoria
                        resultado = objNegocio.RegistrarUsuario(objPersona, objUsuario, out mensaje);
                    }
                    else
                    {
                        // Al modificar: si la caja de contraseña está vacía, podés enviarla vacía 
                        // para que tu Capa de Datos/SP se encargue de mantener la contraseña actual.
                        resultado = objNegocio.ModificarUsuario(objPersona, objUsuario, out mensaje);
                    }

                    if (resultado)
                    {
                        string mensajeExito = idSeleccionado == 0 ? "¡Usuario registrado exitosamente!" : "¡Usuario modificado exitosamente!";
                        MessageBox.Show(mensajeExito, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ConfigurarGridUsuarios();
                        CargarUsuarios();
                        LimpiarCampos();

                        // Reseteamos el idSeleccionado por las dudas para volver al modo inserción
                        idSeleccionado = 0;
                    }
                    else
                    {
                        MessageBox.Show("Error en la operación: " + mensaje, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

        }

        private void CargarRoles()
        {
            try
            {
                CN_Rol objNegocioRol = new CN_Rol();
                DataSet ds = objNegocioRol.ListarRoles();

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables["TablaRoles"];

                    CBRol.DataSource = dt;
                    CBRol.DisplayMember = "descripcion_rol";
                    CBRol.ValueMember = "idRol";
                    CBRol.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btntipoProveedor_Click(object sender, EventArgs e)
        {
            tipoSeleccionado = "Proveedor";
            ConfigurarGridProveedores();
        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TBbuscarPersonas_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtInactivos_Click(object sender, EventArgs e)
        {

        }

        private void btVerInactivos_Click(object sender, EventArgs e)
        {
            // Alterna estrictamente entre ver solo inactivos o ver solo activos
            if (btVerInactivos.Text == "Ver Inactivos")
            {
                btVerInactivos.Text = "Ver Activos"; // Al hacer clic, nos preparamos para ver Inactivos
            }
            else
            {
                btVerInactivos.Text = "Ver Inactivos"; // Al hacer clic, volvemos a ver Activos
            }

            FiltrarGrilla();
        }

        private void btnTodosLosRegistros_Click(object sender, EventArgs e)
        {
            // 1. Limpiamos el buscador
            txtBuscar.Text = "";

            // 2. Reseteamos el botón de inactivos
            btVerInactivos.Text = "Ver Inactivos";

            // 3. Configuramos la grilla y recargamos los datos desde la base de datos
            ConfigurarGridUsuarios();
            CargarUsuarios();
        }

        private void TBDni_Leave(object sender, EventArgs e)
        {
            string dniBuscado = TBDni.Text.Trim();
            if (string.IsNullOrEmpty(dniBuscado)) return;

            try
            {
                CN_Usuario objNegocio = new CN_Usuario();
                DataTable dt = objNegocio.ObtenerPersonaPorDni(dniBuscado);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // Guardamos el ID de la persona existente para usarlo al guardar
                    idPersonaSeleccionada = Convert.ToInt32(row["idPersona"]);

                    // Precargamos los datos automáticamente en tus TextBoxes
                    TBNombre.Text = row["nombre_persona"].ToString();
                    TBApellido.Text = row["apellido_persona"].ToString();
                    TBCorreo.Text = row["correo_persona"].ToString();
                    TBTelefono.Text = row["telefono_persona"].ToString();
                    TBDireccion.Text = row["direccion_persona"].ToString();

                    MessageBox.Show("¡Persona encontrada!.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    idPersonaSeleccionada = 0; // Es una persona nueva
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el DNI: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

    }

        private void TBNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBDni_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            // char.IsControl permite usar teclas de sistema como Borrar (Backspace)
            // char.IsDigit permite ingresar exclusivamente números (0-9)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si la tecla presionada NO es un control y NO es un número, la anulamos
                e.Handled = true;
            }
        }

        private void TBTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Si la tecla presionada NO es un control y NO es un número, la anulamos
                e.Handled = true;
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {

        }
    }
        
    }
    


