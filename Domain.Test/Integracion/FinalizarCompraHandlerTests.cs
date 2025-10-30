using Domain.Test.Fakes;
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
    public class FinalizarCompraHandlerTests
    {
        [Fact]
        public async Task ClienteVip_FinalizaCompraConDescuentoYVacíaCarrito()
        {
            var carrito = new CarritoDeCompras();
            carrito.AgregarProducto(new Producto { Id = 1, Nombre = "Laptop", Precio = new Precio(1000) });

            var cliente = new Cliente { Id = 1, Nombre = "Ana", EsVip = true };
            var clienteRepo = new ClienteRepositoryInMemory(new List<Cliente> { cliente });
            var pedidoRepo = new PedidoRepositoryInMemory();
            var publisher = new EventPublisherInMemory();

            var handler = new FinalizarCompraHandler(carrito, clienteRepo, pedidoRepo, publisher);
            var command = new FinalizarCompraCommand { ClienteId = 1 };

            var result = await handler.Handle(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(900, result.Value); // 10% de descuento
            Assert.Equal(0, carrito.CantidadProductos);
            Assert.Single(pedidoRepo.Pedidos);
            Assert.Single(publisher.EventosPublicados);
        }
    }
}