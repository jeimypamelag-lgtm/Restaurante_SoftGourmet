using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante_SoftGourmet.Clases.Modelos
{
    internal class OrderDetailModel
    {
        public int DetalleID { get; set; }
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public string NotasEspeciales { get; set; }
        public decimal Precio { get; set; }
    }
}
