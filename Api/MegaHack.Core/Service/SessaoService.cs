using MegaHack.Core.Models.Input;
using MegaHack.Core.Models.Output;
using MegaHack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MegaHack.Core.Service
{
    public class SessaoService
    {
        private readonly SessaoRepository _repository;

        public SessaoService(string connectionString)
        {
            _repository = new SessaoRepository(connectionString);
        }

        public async Task<SessaoOuput> IniciarSessao(SessaoInput param)
        {
            return await _repository.IniciarSessao(param);
        }
    }
}
