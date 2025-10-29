using Dominio.Entities;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Fakes
{
    public class ClienteRepositoryInMemory : IClienteRepository
    {
        private readonly List<Cliente> _clientes;
        public ClienteRepositoryInMemory(List<Cliente> clientes) => _clientes = clientes;
        public Task<Cliente?> ObtenerPorIdAsync(int id) => Task.FromResult(_clientes.FirstOrDefault(c => c.Id == id));
    }

}
