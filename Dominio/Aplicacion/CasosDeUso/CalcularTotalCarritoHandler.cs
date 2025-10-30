using Dominio.Aplicacion.Comandos;
using Dominio.Aplicacion.Comunes;
using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.CasosDeUso
{
    public class CalcularTotalCarritoHandler
    {
        private readonly CarritoDeCompras _carrito;

        public CalcularTotalCarritoHandler(CarritoDeCompras carrito)
        {
            _carrito = carrito;
        }

        public Result<decimal> Handle(CalcularTotalCarritoCommand command)
        {
            if (_carrito.CantidadProductos == 0)//Llama a la entidad Carrito de Compras
                return Result<decimal>.Failure("El carrito está vacío");

            var total = _carrito.CalcularTotal();
            return Result<decimal>.Success(total);
        }
    }

}