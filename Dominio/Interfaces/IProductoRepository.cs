using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ObtenerTodosAsync();//Devuelve una lista de productos
        Task<Producto?> ObtenerPorIdAsync(int id);//Devuelve un Producto por su Id----Producto? se refiere que puede regresar NULL en caso de no encontrarlo
        Task CrearAsync(Producto producto);//Agrega un nuevo producto
        Task ActualizarAsync(Producto producto);//Modifica un producto
        Task EliminarAsync(int id);//Elimina un producto por su Id
    }
}