using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Eventos
{
    public class CompraFinalizadaEvent
    {
        public Pedido Pedido { get; }

        public CompraFinalizadaEvent(Pedido pedido)
        {
            Pedido = pedido;
        }
    }
}