using Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.Comandos
{
    public class AplicarDescuentoVipCommand
    {
        public int ClienteId { get; set; }
        public CategoriaProducto Categoria { get; set; }
        public decimal Porcentaje { get; set; }
    }

}