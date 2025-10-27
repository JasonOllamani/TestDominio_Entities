using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class ProductoService
    {
        private readonly IProductoRepository _repositorio;

        public ProductoService(IProductoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Producto?> ObtenerProducto(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }
    }

}
