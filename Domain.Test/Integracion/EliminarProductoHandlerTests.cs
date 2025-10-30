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
    public class EliminarProductoHandlerTests
    {
        [Fact]
        public async Task EliminarProducto_EliminaDelRepositorio()
        {
            var repo = new ProductoRepositoryInMemory(new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop", Precio = new Precio(1000) }
        });

            var handler = new EliminarProductoHandler(repo);
            var result = await handler.Handle(new EliminarProductoCommand { Id = 1 });

            Assert.True(result.IsSuccess);
            var producto = await repo.ObtenerPorIdAsync(1);
            Assert.Null(producto);
        }
    }
}