using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool EsVip { get; set; }

        //public Cliente(int id, string nombre, bool esVip)
        //{
        //    Id = id;
        //    Nombre = nombre;
        //    EsVip = esVip;
        //}
    }
}
