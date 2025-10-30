using Domain.Test.Fakes;
using Dominio.Aplicacion.CasosDeUso;
using Dominio.Aplicacion.Comandos;
using Dominio.Entities;
using Dominio.Enums;
using Dominio.Servicios;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Integracion
{
    public class AplicarDescuentoVipHandlerTests
    {
        [Fact]
        public async Task ClienteVip_AplicaDescuentoYPublicaEventos()
        {
            var cliente = new Cliente { Id = 1, Nombre = "Juan", EsVip = true };
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Laptop", Categoria = CategoriaProducto.Electronica, Precio = new Precio(1000) },
                new Producto { Id = 2, Nombre = "Mouse", Categoria = CategoriaProducto.Electronica, Precio = new Precio(100) }
            };

            var clienteRepo = new ClienteRepositoryInMemory(new List<Cliente> { cliente });
            var productoRepo = new ProductoRepositoryInMemory(productos);
            var publisher = new EventPublisherInMemory();

            var servicio = new ServicioPromocion(clienteRepo, productoRepo, publisher);
            var handler = new AplicarDescuentoVipHandler(servicio);

            var command = new AplicarDescuentoVipCommand
            {
                ClienteId = 1,
                Categoria = CategoriaProducto.Electronica,
                Porcentaje = 0.1m
            };

            var result = await handler.Handle(command);

            Assert.True(result.IsSuccess);
            var actualizados = await productoRepo.ObtenerTodosAsync();
            Assert.Equal(900, actualizados[0].Precio.Valor);
            Assert.Equal(90, actualizados[1].Precio.Valor);
            Assert.Equal(2, publisher.EventosPublicados.Count);
        }
    }
}