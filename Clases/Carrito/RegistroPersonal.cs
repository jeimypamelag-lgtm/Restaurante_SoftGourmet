using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurante_SoftGourmet.Clases.Carrito
{
    public partial class RegistroPersonal : Form
    {
        public RegistroPersonal()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Captura los datos ingresados por el usuario
            // Asegúrate de que txtUsuario y txtContrasena coincidan con los Name de tus TextBox
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text;

            // Validación básica de campos vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación de credenciales para el empleado
            if (usuario == "empleado" && contraseña == "1234")
            {
                MessageBox.Show("¡Bienvenido al sistema!", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Oculta el formulario de RegistroPersonal
                this.Hide();

                // Abre el nuevo menú de empleados
                MenuEmpleados frm = new MenuEmpleados();
                frm.Show();
                this.Hide();

                // Al cerrar el menú de empleados, se cierra por completo la aplicación
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContraseña.Clear();
                txtContraseña.Focus();
            }
        }

        private void RegistroPersonal_Load(object sender, EventArgs e)
        {

        }
    }
}
