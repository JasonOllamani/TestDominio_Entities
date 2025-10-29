using Domain.Test.Fakes;
using Dominio.Entities;
using Dominio.Enums;
using Dominio.Eventos;
using Dominio.Servicios;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Integracion
{
    public class ServicioPromocionIntegracionTests
    {
        [Fact]
        public async Task AplicarDescuentoVip_ActualizaPreciosYPublicaEventos()
        {
            // Datos simulados
            //var cliente = new Cliente { Id = 1, Nombre = "Juan", EsVip = true };
            var cliente = new List<Cliente>
            {
                new Cliente { Id = 1, Nombre = "Juan", EsVip = true  },
                new Cliente { Id = 2, Nombre = "Ana", EsVip = false}
            };

            var productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Laptop", Categoria = CategoriaProducto.Electronica, Precio = new Precio(1000) },
                new Producto { Id = 2, Nombre = "Mouse", Categoria = CategoriaProducto.Electronica, Precio = new Precio(100) },
                new Producto { Id = 3, Nombre = "Camisa", Categoria = CategoriaProducto.Ropa, Precio = new Precio(50) },
                new Producto { Id = 4, Nombre = "Tablet", Categoria = CategoriaProducto.Electronica, Precio = new Precio(800) },
                new Producto { Id = 5, Nombre = "Audífonos", Categoria = CategoriaProducto.Electronica, Precio = new Precio(200) },
                new Producto { Id = 6, Nombre = "Zapatos", Categoria = CategoriaProducto.Ropa, Precio = new Precio(120) }
            };

            // Repositorios y publicador reales en memoria
            var clienteRepo = new ClienteRepositoryInMemory( cliente );
            var productoRepo = new ProductoRepositoryInMemory(productos);
            var eventPublisher = new EventPublisherInMemory();

            // Servicio real
            var servicio = new ServicioPromocion(clienteRepo, productoRepo, eventPublisher);

            // Acción
            await servicio.AplicarDescuentoVip(1, CategoriaProducto.Electronica, 0.1m);

            // Verificación
            var actualizados = await productoRepo.ObtenerTodosAsync();
            Assert.Equal(900, actualizados[0].Precio.Valor); // Laptop
            Assert.Equal(90, actualizados[1].Precio.Valor);  // Mouse
            Assert.Equal(50, actualizados[2].Precio.Valor);  // Camisa (no cambia)
            Assert.Equal(720, actualizados[3].Precio.Valor); // Tablet
            Assert.Equal(180, actualizados[4].Precio.Valor); // Audífonos
            Assert.Equal(120, actualizados[5].Precio.Valor); // Zapatos (no cambia)

            Assert.Equal(4, eventPublisher.EventosPublicados.Count);
            Assert.All(eventPublisher.EventosPublicados, e => Assert.IsType<ProductoActualizadoEvent>(e));
        }
    }
}
