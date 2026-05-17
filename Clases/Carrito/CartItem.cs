using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante_SoftGourmet.Clases.Modelos;
using Product = Restaurante_SoftGourmet.Clases.Modelos.ProductModel;


namespace Restaurante_SoftGourmet.Clases.Carrito
{
    internal class CartItem
    {
        public int ProductoID { get; private set; }
        public Product Producto { get; private set; }
        public int Cantidad { get; set; }

        public CartItem(Product producto, int cantidad)
        {
            Producto = producto ?? throw new ArgumentNullException(nameof(producto));
            ProductoID = producto.ProductoID;
            Cantidad = Math.Max(1, cantidad);
        }
    }
}
