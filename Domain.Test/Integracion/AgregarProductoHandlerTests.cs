using Domain.Test.Fakes;
using Dominio.Aplicacion.CasosDeUso;
using Dominio.Aplicacion.Comandos;
using Dominio.Entities;
using Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Integracion
{
    public class AgregarProductoHandlerTests
    {
        [Fact]
        public async Task AgregarProducto_CreaProductoEnRepositorio()
        {
            var repo = new ProductoRepositoryInMemory(new List<Producto>());
            var handler = new AgregarProductoHandler(repo);

            var command = new AgregarProductoCommand
            {
                Id = 1,
                Nombre = "Laptop",
                Categoria = CategoriaProducto.Electronica,
                Precio = 1000
            };

            var result = await handler.Handle(command);

            Assert.True(result.IsSuccess);
            var producto = await repo.ObtenerPorIdAsync(1);
            Assert.NotNull(producto);
            Assert.Equal("Laptop", producto?.Nombre);
            Assert.Equal(1000, producto?.Precio.Valor);
        }
    }
}