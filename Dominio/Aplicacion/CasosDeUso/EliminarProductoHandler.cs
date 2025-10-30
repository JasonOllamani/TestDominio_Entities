using Dominio.Aplicacion.Comandos;
using Dominio.Aplicacion.Comunes;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.CasosDeUso
{
    public class EliminarProductoHandler
    {
        private readonly IProductoRepository _repo;

        public EliminarProductoHandler(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(EliminarProductoCommand command)//Obtiene el ID
        {
            await _repo.EliminarAsync(command.Id);//Elimina
            return Result.Success();
        }
    }

}