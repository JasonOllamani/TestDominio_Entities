using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IEventPublisher
    {
        //void Publicar(object evento);

        Task PublicarAsync<T>(T evento);
    }
}