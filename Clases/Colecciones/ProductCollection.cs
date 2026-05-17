using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurante_SoftGourmet.Clases.Modelos;
using Product = Restaurante_SoftGourmet.Clases.Modelos.ProductModel;


namespace Restaurante_SoftGourmet.Clases.Colecciones
{
    internal class ProductCollection
    {
        private ProductNode _head;
        private int _count;

        private class ProductNode
        {
            public Product Value;
            public ProductNode Next;
            public ProductNode(Product value) { Value = value; Next = null; }
        }

        public void Add(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            var node = new ProductNode(product);
            if (_head == null)
            {
                _head = node;
            }
            else
            {
                var cur = _head;
                while (cur.Next != null) cur = cur.Next;
                cur.Next = node;
            }
            _count++;
        }

        public Product[] ToArray()
        {
            var arr = new Product[_count];
            var cur = _head;
            int i = 0;
            while (cur != null)
            {
                arr[i++] = cur.Value;
                cur = cur.Next;
            }
            return arr;
        }

        public Product GetAt(int index)
        {
            if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
            var cur = _head;
            int i = 0;
            while (cur != null)
            {
                if (i == index) return cur.Value;
                cur = cur.Next;
                i++;
            }
            throw new IndexOutOfRangeException();
        }

        public int Count { get { return _count; } }

        public void Clear()
        {
            _head = null;
            _count = 0;
        }
    }
}
