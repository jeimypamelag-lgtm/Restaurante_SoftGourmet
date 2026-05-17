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
    }
}
