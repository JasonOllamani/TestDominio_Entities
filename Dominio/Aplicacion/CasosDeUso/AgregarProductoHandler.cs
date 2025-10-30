using Dominio.Aplicacion.Comandos;
using Dominio.Aplicacion.Comunes;
using Dominio.Entities;
using Dominio.Interfaces;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.CasosDeUso
{
    public class AgregarProductoHandler
    {
        private readonly IProductoRepository _repo;

        public AgregarProductoHandler(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(AgregarProductoCommand command)//Recibe los datos
        {
            var producto = new Producto//Instancia con los datos
            {
                Id = command.Id,
                Nombre = command.Nombre,
                Categoria = command.Categoria,
                Precio = new Precio(command.Precio)
            };

            await _repo.CrearAsync(producto);//Guarda datos
            return Result.Success();
        }
    }
}