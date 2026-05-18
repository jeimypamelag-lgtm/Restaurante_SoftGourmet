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
    public partial class MenuEmpleados : Form
    {
        public MenuEmpleados()
        {
            InitializeComponent();

        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {
            ModuloAlmacen frm = new ModuloAlmacen();
            frm.Show();
            this.Hide();
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            ModuloFacturacion frm = new ModuloFacturacion();
            frm.Show();
            this.Hide();
        }

        private void btnProduccion_Click(object sender, EventArgs e)
        {
            ModuloProduccion frm = new ModuloProduccion();
            frm.Show();
            this.Hide();
        }

       


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //FrmMenu regresar
            MenuPrincipal frmMenu = new MenuPrincipal();
            frmMenu.Show();
            this.Close();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            // Obtenemos la fecha en formato largo (ej: domingo, 17 de mayo de 2026)
            string fecha = DateTime.Now.ToLongDateString();

            // Obtenemos la hora en formato 24h
            string hora = DateTime.Now.ToString("HH:mm:ss");

            // Lo asignamos al Label usando una interpolación para que se vean ambos
            lblFechaActual1.Text = $"{fecha}  |  {hora}";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //FrmMenu regresar
            RegistroPersonal frmMenu = new RegistroPersonal();
            frmMenu.Show();
            this.Close();
        }
    }
}
