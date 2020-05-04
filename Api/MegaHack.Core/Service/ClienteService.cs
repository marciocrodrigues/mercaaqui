using MegaHack.Core.Models.Output;
using MegaHack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Service
{
    public class ClienteService
    {
        private readonly ClienteRespository _repository;

        public ClienteService(string connectionString)
        {
            _repository = new ClienteRespository(connectionString);
        }

        public async Task<List<ClienteOutput>> ListarClientes()
        {
            return await _repository.ListarClientes();
        }

        public async Task<ClienteOutput> BuscarClientePorId(int ID_Cliente)
        {
            return await _repository.BuscarEntregadorPorId(ID_Cliente);
        }
    }
}
