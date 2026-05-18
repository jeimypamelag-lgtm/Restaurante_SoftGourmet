using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            string connectionString = "Data Source=.;Initial Catalog=SoftGourmetDB;Integrated Security=True";

            string usuarioInput = txtUsuario.Text.Trim();
            string contrasenaInput = txtContraseña.Text;

            // Validación de campos vacíos
            if (string.IsNullOrEmpty(usuarioInput) || string.IsNullOrEmpty(contrasenaInput))
            {
                MessageBox.Show("Por favor, ingrese sus credenciales.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Consulta SQL parametrizada por seguridad
            string query = "SELECT COUNT(1) FROM Usuarios WHERE NombreUsuario = @user AND Contrasena = @pass";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(query, conexion);

                    // Añadimos los parámetros para evitar Inyección SQL
                    comando.Parameters.AddWithValue("@user", usuarioInput);
                    comando.Parameters.AddWithValue("@pass", contrasenaInput);

                    int existe = Convert.ToInt32(comando.ExecuteScalar());

                    if (existe > 0)
                    {
                        MessageBox.Show("¡Bienvenido al sistema SoftGourmet!", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide(); // Ocultamos el login

                        // Abrimos el menú que diseñamos
                        MenuEmpleados menu = new MenuEmpleados();
                        menu.ShowDialog();

                        this.Close(); // Cerramos definitivamente al salir del menú
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos. Intente de nuevo.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtContraseña.Clear();
                        txtContraseña.Focus();
                    }
                }
                catch (Exception ex)
                {
                    // Mensaje claro por si la otra persona no tiene la base de datos instalada
                    MessageBox.Show("No se pudo establecer conexión con la base de datos local.\n\n" +
                                    "Asegúrese de haber ejecutado el script SQL de 'SoftGourmetDB' en su SQL Server.\n\n" +
                                    "Detalle técnico: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RegistroPersonal_Load(object sender, EventArgs e)
        {

        }
    }
}
