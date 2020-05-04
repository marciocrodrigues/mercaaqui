using MegaHack.Core.Models.Input;
using MegaHack.Core.Models.Output;
using MegaHack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Service
{
    public class ComercianteService
    {
        private readonly ComercianteRepository _repository;

        public ComercianteService(string connectionString)
        {
            _repository = new ComercianteRepository(connectionString);
        }

        public async Task<List<ComercianteOutput>> ComerciantePorCidadeEstado(ComercianteInput param)
        {
            return await _repository.ComerciantePorCidadeEstado(param);
        }

        public async Task<List<ProdutoOutput>> BuscaProdutoPorComerciante(int ID_Comerciante)
        {
            return await _repository.BuscarProdutoPorComerciante(ID_Comerciante);
        }

        public async Task<ComercianteOutput> BuscarComerciantePorId(int ID_Comerciante)
        {
            return await _repository.BuscarComerciantePorId(ID_Comerciante);
        }
    }
}
