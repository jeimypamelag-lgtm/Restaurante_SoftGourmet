using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurante_SoftGourmet.Clases.Carrito;

namespace Restaurante_SoftGourmet
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            GraphicsPath ruta = new GraphicsPath();

            int radio = 25; // mientras más grande, más redondas

            ruta.StartFigure();
            ruta.AddArc(0, 0, radio, radio, 180, 90);
            ruta.AddArc(panel1.Width - radio, 0, radio, radio, 270, 90);
            ruta.AddArc(panel1.Width - radio, panel1.Height - radio, radio, radio, 0, 90);
            ruta.AddArc(0, panel1.Height - radio, radio, radio, 90, 90);
            ruta.CloseFigure();

            panel1.Region = new Region(ruta);
            panel2.Region = new Region(ruta);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ModuloComensal frm = new ModuloComensal();
            frm.Show();
            this.Hide();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            RegistroPersonal frm = new RegistroPersonal();

            frm.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
