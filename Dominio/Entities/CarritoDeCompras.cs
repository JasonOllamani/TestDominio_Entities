using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class CarritoDeCompras
    {
        private readonly List<Producto> _productos = new();

        public void AgregarProducto(Producto producto)
        {
            _productos.Add(producto);
        }

        public decimal CalcularTotal()
        {
            return _productos.Sum(p => p.Precio.Valor);
        }

        public int CantidadProductos => _productos.Count;
    }
}
