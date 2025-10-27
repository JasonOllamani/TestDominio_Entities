using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ValueObjects
{
    public readonly struct Precio//Especifica que no cambian los valores
    {
        public decimal Valor { get; }

        public Precio(decimal valor)//Valida el precio
        {
            if (valor < 0)
                throw new ArgumentException("El precio no puede ser negativo.");
            Valor = valor;
        }

        public Precio AplicarDescuento(decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 1)
                throw new ArgumentException("Porcentaje inválido.");
            return new Precio(Valor * (1 - porcentaje));
        }

        public override string ToString() => $"${Valor:N2}";
    }
}
