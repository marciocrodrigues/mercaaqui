using MegaHack.Core.Models.Input;
using MegaHack.Core.Models.Output;
using MegaHack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Service
{
    public class CadastroService
    {
        private readonly CadastroRepository _repository;

        public CadastroService(string connectionString)
        {
            _repository = new CadastroRepository(connectionString);
        }

        public async Task<CadastroOutput> Cadastrar(CadastroInput cadastro)
        {
            return await _repository.Cadastrar(cadastro);
        }

        public async Task<ProdutoOutput> CadastrarProduto(ProdutoInput produto)
        {
            return await _repository.CadastrarProduto(produto);
        }

        public async Task<ProcessoOutput> CadastrarProcesso(ProcessoInput processo)
        {
            processo.NO_Comprovante = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 14);
            return await _repository.CadastrarProcesso(processo);
        }
    }
}
