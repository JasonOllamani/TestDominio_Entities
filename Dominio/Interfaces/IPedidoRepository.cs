using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IPedidoRepository
    {
        Task CrearAsync(Pedido pedido);
        Task<Pedido?> ObtenerPorIdAsync(int id);
        Task<List<Pedido>> ObtenerTodosAsync();
    }
}