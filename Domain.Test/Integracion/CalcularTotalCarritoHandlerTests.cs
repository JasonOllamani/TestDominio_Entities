using Dominio.Aplicacion.CasosDeUso;
using Dominio.Aplicacion.Comandos;
using Dominio.Entities;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Integracion
{
    public class CalcularTotalCarritoHandlerTests
    {
        [Fact]
        public void CarritoConProductos_CalculaTotalCorrectamente()
        {
            var carrito = new CarritoDeCompras();
            carrito.AgregarProducto(new Producto { Id = 1, Nombre = "Laptop", Precio = new Precio(1000) });
            carrito.AgregarProducto(new Producto { Id = 2, Nombre = "Mouse", Precio = new Precio(100) });

            var handler = new CalcularTotalCarritoHandler(carrito);
            var result = handler.Handle(new CalcularTotalCarritoCommand());

            Assert.True(result.IsSuccess);
            Assert.Equal(1100, result.Value);
        }

        [Fact]
        public void CarritoVacio_RetornaError()
        {
            var carrito = new CarritoDeCompras();
            var handler = new CalcularTotalCarritoHandler(carrito);
            var result = handler.Handle(new CalcularTotalCarritoCommand());

            Assert.False(result.IsSuccess);
            Assert.Equal("El carrito está vacío", result.Error);
        }
    }
}