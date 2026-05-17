using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante_SoftGourmet.Clases.Modelos
{
    internal class ProductModel
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string ImagenRuta { get; set; }
        public bool Disponible { get; set; }
        public string Descripcion { get; set; }

        // Añadido para poder filtrar por categoría desde la BD
        public int? CategoriaID { get; set; }
    }
}
