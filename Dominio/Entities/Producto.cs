using Dominio.Enums;
using Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Producto
    {
        public int Id { get; set; }//Identificador del producto
        public string Nombre { get; set; } = string.Empty;
        public Precio Precio { get; set; } = new Precio(0);//Value Object - Encapsula algunas reglas
        public CategoriaProducto Categoria { get; set; }//Tipo de producto

        public void CambiarPrecio(Precio nuevoPrecio)
        {
            Precio = nuevoPrecio;
        }
    }
}