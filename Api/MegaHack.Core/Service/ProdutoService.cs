using MegaHack.Core.Models.Output;
using MegaHack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Service
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _repository;

        public ProdutoService(string connectionString)
        {
            _repository = new ProdutoRepository(connectionString);
        }

        public async Task<List<ProdutoOutput>> BuscarProdutoPorDescricao(string Descricao)
        {
            return await _repository.BuscarProdutoPorDescricao(Descricao);
        }
    }
}
