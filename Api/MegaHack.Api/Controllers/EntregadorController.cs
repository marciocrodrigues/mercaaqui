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
    public class EntregadorController : ControllerBase
    {
        private readonly EntregadorService _service;
        private readonly IConfiguration _configuration;

        public EntregadorController(IConfiguration configuration)
        {
            _configuration = configuration;
            _service = new EntregadorService(_configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Listar todos os entregadores
        /// </summary>
        /// <returns></returns>
        [HttpGet("listar-entregadores")]
        public async Task<ActionResult<List<EntregadorOutput>>> ListarEntregadores()
        {
            return Ok(await _service.ListarEntregadores());
        }

        /// <summary>
        /// Buscar entregador pelo Identificador ID_Entregador
        /// </summary>
        /// <param name="ID_Entregador"></param>
        /// <returns></returns>
        [HttpGet("{ID_Entregador}")]
        [Authorize]
        public async Task<ActionResult<EntregadorOutput>> BuscarEntregadorPorId([FromRoute]int ID_Entregador)
        {
            return Ok(await _service.BuscarEntregadorPorId(ID_Entregador));
        }


    }
}