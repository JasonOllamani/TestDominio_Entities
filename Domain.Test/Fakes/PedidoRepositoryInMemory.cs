using Dominio.Entities;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Fakes
{
    public class PedidoRepositoryInMemory : IPedidoRepository
    {
        private readonly List<Pedido> _pedidos = new();

        public IReadOnlyList<Pedido> Pedidos => _pedidos.AsReadOnly();

        public Task CrearAsync(Pedido pedido)
        {
            _pedidos.Add(pedido);
            return Task.CompletedTask;
        }

        public Task<Pedido?> ObtenerPorIdAsync(int id)
        {
            var pedido = _pedidos.FirstOrDefault(p => p.ClienteId == id);
            return Task.FromResult(pedido);
        }

        public Task<List<Pedido>> ObtenerTodosAsync()
        {
            return Task.FromResult(_pedidos.ToList());
        }
    }
}