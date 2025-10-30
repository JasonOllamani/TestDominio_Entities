using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Eventos
{
    public class ProductoActualizadoEvent
    {
        public int ProductoId { get; }
        public decimal NuevoPrecio { get; }
        public DateTime Fecha { get; }

        public ProductoActualizadoEvent(int productoId, decimal nuevoPrecio)
        {
            ProductoId = productoId;
            NuevoPrecio = nuevoPrecio;
            Fecha = DateTime.UtcNow;
        }
    }
}