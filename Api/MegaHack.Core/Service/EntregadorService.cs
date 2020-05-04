using MegaHack.Core.Models.Output;
using MegaHack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Service
{
    public class EntregadorService
    {
        private readonly EntregadorRepository _repository;

        public EntregadorService(string connectionString)
        {
            _repository = new EntregadorRepository(connectionString);
        }

        public async Task<List<EntregadorOutput>> ListarEntregadores()
        {
            return await _repository.ListarEntregadores();
        }

        public async Task<EntregadorOutput> BuscarEntregadorPorId(int ID_Entregador)
        {
            return await _repository.BuscarEntregadorPorId(ID_Entregador);
        }
    }
}
