using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Pedido
    {
        public int ClienteId { get; set; }
        public List<Producto> Productos { get; set; } = new();
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}