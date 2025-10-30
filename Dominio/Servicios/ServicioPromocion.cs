using Dominio.Enums;
using Dominio.Eventos;
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
        private readonly IEventPublisher _eventPublisher;

        public ServicioPromocion(IClienteRepository clienteRepo, IProductoRepository productoRepo, IEventPublisher eventPublisher)
        {
            _clienteRepo = clienteRepo;
            _productoRepo = productoRepo;
            _eventPublisher = eventPublisher;
        }

        public async Task AplicarDescuentoVip(int clienteId, CategoriaProducto categoria, decimal porcentaje)
        {
            var cliente = await _clienteRepo.ObtenerPorIdAsync(clienteId);
            if (cliente == null || !cliente.EsVip)
                return;

            var productos = await _productoRepo.ObtenerTodosAsync();
            var filtrados = productos.Where(p => p.Categoria == categoria).ToList();

            foreach (var producto in filtrados)
            {
                var nuevoPrecio = producto.Precio.AplicarDescuento(porcentaje);
                producto.Precio = nuevoPrecio;
                await _productoRepo.ActualizarAsync(producto);

                var evento = new ProductoActualizadoEvent(producto.Id, nuevoPrecio.Valor);
                //_eventPublisher.Publicar(evento);
                await _eventPublisher.PublicarAsync(evento);
            }
        }
    }
}
