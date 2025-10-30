using Dominio.Entities;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Integracion
{
    public class CarritoDeComprasIntegracionTests
    {
        [Fact]
        public void AgregarProducto_IncrementaCantidadYTotal()
        {
            var carrito = new CarritoDeCompras();
            var producto = new Producto { Id = 1, Nombre = "Laptop", Precio = new Precio(1000) };

            carrito.AgregarProducto(producto);

            Assert.Equal(1, carrito.CantidadProductos);
            Assert.Equal(1000, carrito.CalcularTotal());
        }

        [Fact]
        public void CarritoVacio_TieneTotalCero()
        {
            var carrito = new CarritoDeCompras();
            Assert.Equal(0, carrito.CalcularTotal());
            Assert.Equal(0, carrito.CantidadProductos);
        }
    }
}