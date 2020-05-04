using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;
        private readonly IConfiguration Configuration;

        public ProdutoController(IConfiguration configuration)
        {
            Configuration = configuration;
            _service = new ProdutoService(Configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Buscar produto por Descrição
        /// </summary>
        /// <remarks>Irá trazer o produto caso tenha a descrição em quanlquer local do nome</remarks>
        /// <param name="Descricao"></param>
        /// <returns></returns>
        [HttpGet("buscar-produtos")]
        public async Task<ActionResult<List<ProdutoOutput>>> BuscarProdutoPorDescricao([FromQuery]string Descricao)
        {
            return Ok(await _service.BuscarProdutoPorDescricao(Descricao));
        }
    }
}