using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Fakes
{
    public class EventPublisherInMemory : IEventPublisher
    {
        public List<object> EventosPublicados { get; } = new();
        public void Publicar(object evento) => EventosPublicados.Add(evento);
    }

}
