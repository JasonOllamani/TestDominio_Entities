using Dominio.Aplicacion.Comandos;
using Dominio.Aplicacion.Comunes;
using Dominio.Interfaces;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.CasosDeUso
{
    public class ActualizarProductoHandler
    {
        private readonly IProductoRepository _repo;

        public ActualizarProductoHandler(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(ActualizarProductoCommand command)//Obtiene el ID
        {
            var producto = await _repo.ObtenerPorIdAsync(command.Id);//Realiza la busqueda por ID
            if (producto == null) return Result.Failure("No encontrado");

            producto.Nombre = command.Nombre;
            producto.Precio = new Precio(command.Precio);
            await _repo.ActualizarAsync(producto);//Actualiza en caso de existir

            return Result.Success();
        }
    }
}