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
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;
        private readonly IConfiguration _configuration;

        public ClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
            _service = new ClienteService(_configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Listar todos os clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet("listar-clientes")]
        public async Task<ActionResult<List<ClienteOutput>>> ListarClientes()
        {
            return Ok(await _service.ListarClientes());
        }

        /// <summary>
        /// Buscar cliente pelo identificador
        /// </summary>
        /// <param name="ID_Cliente"></param>
        /// <returns></returns>
        [HttpGet("{ID_Cliente}")]
        public async Task<ActionResult<ClienteOutput>> BuscarClientePorId([FromRoute]int ID_Cliente)
        {
            return Ok(await _service.BuscarClientePorId(ID_Cliente));
        }
    }
}