using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante_SoftGourmet.Clases.Modelos;
using Product = Restaurante_SoftGourmet.Clases.Modelos.ProductModel;


namespace Restaurante_SoftGourmet.Clases.Carrito
{
    internal class CartStorage
    {
        private CartNode _head;
        private int _count;

        private class CartNode
        {
            public CartItem Item;
            public CartNode Next;
            public CartNode(CartItem item) { Item = item; Next = null; }
        }

        public void AddOrIncrement(Product product, int cantidad)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (cantidad <= 0) cantidad = 1;

            var cur = _head;
            while (cur != null)
            {
                if (cur.Item.ProductoID == product.ProductoID)
                {
                    cur.Item.Cantidad += cantidad;
                    return;
                }
                cur = cur.Next;
            }

            var node = new CartNode(new CartItem(product, cantidad));
            if (_head == null) _head = node;
            else
            {
                var tail = _head;
                while (tail.Next != null) tail = tail.Next;
                tail.Next = node;
            }
            _count++;
        }

        public void UpdateQuantity(int productoId, int cantidad)
        {
            CartNode prev = null;
            var cur = _head;
            while (cur != null)
            {
                if (cur.Item.ProductoID == productoId)
                {
                    if (cantidad <= 0)
                    {
                        if (prev == null) _head = cur.Next;
                        else prev.Next = cur.Next;
                        _count--;
                    }
                    else
                    {
                        cur.Item.Cantidad = cantidad;
                    }
                    return;
                }
                prev = cur;
                cur = cur.Next;
            }
        }

        public void Remove(int productoId)
        {
            UpdateQuantity(productoId, 0);
        }

        public CartItem[] ToArray()
        {
            var arr = new CartItem[_count];
            var cur = _head;
            int i = 0;
            while (cur != null)
            {
                arr[i++] = cur.Item;
                cur = cur.Next;
            }
            return arr;
        }

        public int Count { get { return _count; } }

        public void Clear()
        {
            _head = null;
            _count = 0;
        }
    }
}
