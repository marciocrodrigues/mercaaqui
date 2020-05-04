using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaHack.Core.Models.Input;
using MegaHack.Core.Models.Output;
using MegaHack.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MegaHack.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CadastrarController : ControllerBase
    {
        private readonly CadastroService _service;
        private readonly IConfiguration _configuration;

        public CadastrarController(IConfiguration configuration)
        {
            _configuration = configuration;
            _service = new CadastroService(_configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Rota para cadastro de Cliente, Comerciante e Entregador
        /// </summary>
        /// <remarks>Cadastrar pessoa, ao passar o tipo atentar-se para CO = Comerciante, CL = Cliente, EN = Entregador</remarks>
        /// <param name="param"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CadastroOutput), 200)]
        [HttpPost("cadastrar-pessoa")]
        public async Task<ActionResult<CadastroOutput>> cadastrar(CadastroInput param)
        {
            return Ok(await _service.Cadastrar(param));
        }

        /// <summary>
        /// Rota para cadastro ou atualização de produto
        /// </summary>
        /// <remarks>Cadastra o produto e atualiza quando informa o identificador ID_Produto</remarks>
        /// <param name="param"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ProdutoOutput), 200)]
        [HttpPost("cadastrar-produto")]
        public async Task<ActionResult<ProdutoOutput>> cadastrarProduto(ProdutoInput param)
        {
            return Ok(await _service.CadastrarProduto(param));
        }

        /// <summary>
        /// Rota para cadastro ou atualização de processo
        /// </summary>
        /// <remarks>Cadastra o processo e atualiza quando informa o identificador ID_Processo</remarks>
        /// <param name="param"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ProcessoOutput), 200)]
        [HttpPost("cadastrar-processo")]
        public async Task<ActionResult<ProcessoOutput>> cadastrarProcesso(ProcessoInput param)
        {
            return Ok(await _service.CadastrarProcesso(param));
        }
    }
}
