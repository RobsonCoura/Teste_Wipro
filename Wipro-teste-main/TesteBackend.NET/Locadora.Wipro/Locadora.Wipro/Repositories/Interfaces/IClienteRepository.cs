using Wipro.Locadora.Models;
using System.Collections.Generic;

namespace Wipro.Locadora.Repositories.Interfaces
{
    interface IClienteRepository
    {
        void Post(Cliente cliente);
        List<Cliente> GetClientes();
        Cliente GetClientePorId(int idCliente);
    }
}
