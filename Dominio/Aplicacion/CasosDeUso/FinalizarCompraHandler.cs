using Dominio.Aplicacion.Comandos;
using Dominio.Aplicacion.Comunes;
using Dominio.Entities;
using Dominio.Eventos;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.CasosDeUso
{
    public class FinalizarCompraHandler
    {
        private readonly CarritoDeCompras _carrito;
        private readonly IClienteRepository _clienteRepo;
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IEventPublisher _publisher;

        public FinalizarCompraHandler(
            CarritoDeCompras carrito,
            IClienteRepository clienteRepo,
            IPedidoRepository pedidoRepo,
            IEventPublisher publisher)
        {
            _carrito = carrito;
            _clienteRepo = clienteRepo;
            _pedidoRepo = pedidoRepo;
            _publisher = publisher;
        }

        public async Task<Result<decimal>> Handle(FinalizarCompraCommand command)
        {
            if (_carrito.CantidadProductos == 0)
                return Result<decimal>.Failure("El carrito está vacío");

            var cliente = await _clienteRepo.ObtenerPorIdAsync(command.ClienteId);
            if (cliente == null)
                return Result<decimal>.Failure("Cliente no encontrado");

            var total = _carrito.CalcularTotal();

            if (cliente.EsVip)
                total *= 0.9m; // 10% de descuento

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                Productos = _carrito.ObtenerProductos(),
                Total = total,
                Fecha = DateTime.UtcNow
            };

            await _pedidoRepo.CrearAsync(pedido);
            _carrito.Vaciar();

            await _publisher.PublicarAsync(new CompraFinalizadaEvent(pedido));

            return Result<decimal>.Success(total);
        }
    }
}