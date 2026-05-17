using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante_SoftGourmet.Clases.Modelos
{
    internal class OrderModel
    {
        public int PedidoID { get; set; }
        public int? MesaID { get; set; }
        public int? NumeroMesa { get; set; }
        public DateTime FechaHora { get; set; }
        public string EstadoCocina { get; set; }
        public int Prioridad { get; set; }
        public int ItemsCount { get; set; }
    }
}
