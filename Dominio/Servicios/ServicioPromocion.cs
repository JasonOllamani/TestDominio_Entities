using Dominio.Enums;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicios
{
    public class ServicioPromocion
    {
        private readonly IProductoRepository _productoRepo;
        private readonly IClienteRepository _clienteRepo;

        public ServicioPromocion(IClienteRepository clienteRepo, IProductoRepository productoRepo)
        {
            _clienteRepo = clienteRepo;
            _productoRepo = productoRepo;
        }

        public async Task AplicarDescuentoVip(int clienteId, CategoriaProducto categoria, decimal porcentaje)
        {
            var cliente = await _clienteRepo.ObtenerPorIdAsync(clienteId);
            if (cliente == null || !cliente.EsVip)
                return;

            var productos = await _productoRepo.ObtenerTodosAsync();
            var filtrados = productos.Where(p => p.Categoria == categoria);

            foreach (var producto in filtrados)
            {
                var nuevoPrecio = producto.Precio.AplicarDescuento(porcentaje);
                producto.Precio = nuevoPrecio;
                await _productoRepo.ActualizarAsync(producto);
            }
        }
    }


}
