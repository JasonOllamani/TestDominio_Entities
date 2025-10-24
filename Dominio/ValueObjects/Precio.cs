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

        public override string ToString() => $"${Valor:N2}";
    }
}
