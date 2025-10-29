using Dominio.Entities;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Fakes
{
    public class ProductoRepositoryInMemory : IProductoRepository
    {
        private readonly List<Producto> _productos;

        public ProductoRepositoryInMemory(List<Producto> productos)
        {
            _productos = productos;
        }

        public Task<List<Producto>> ObtenerTodosAsync()
        {
            return Task.FromResult(_productos);
        }

        public Task<Producto?> ObtenerPorIdAsync(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(producto);
        }

        public Task CrearAsync(Producto producto)
        {
            _productos.Add(producto);
            return Task.CompletedTask;
        }

        public Task ActualizarAsync(Producto producto)
        {
            var index = _productos.FindIndex(p => p.Id == producto.Id);
            if (index != -1)
            {
                _productos[index] = producto;
            }
            return Task.CompletedTask;
        }

        public Task EliminarAsync(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                _productos.Remove(producto);
            }
            return Task.CompletedTask;
        }
    }
}
