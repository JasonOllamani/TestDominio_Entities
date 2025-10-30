using Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aplicacion.Comandos
{
    public class AgregarProductoCommand
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public CategoriaProducto Categoria { get; set; }
        public decimal Precio { get; set; }
    }
}