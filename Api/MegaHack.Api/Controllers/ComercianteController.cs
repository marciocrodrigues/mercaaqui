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
    public class ComercianteController : ControllerBase
    {
        private readonly ComercianteService _service;
        private readonly IConfiguration _Configuration;

        public ComercianteController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _service = new ComercianteService(_Configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Busca de comerciante por cidade e estado
        /// </summary>
        /// <remarks>
        /// Buscar comerciante por cidade e estado
        /// </remarks>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("comerciantes/cidade-estado")]
        public async Task<ActionResult<List<ComercianteOutput>>> ComerciantePorCidadeEstado([FromQuery]ComercianteInput param)
        {
            return Ok(await _service.ComerciantePorCidadeEstado(param));
        }

        /// <summary>
        /// Buscar os produtos por comerciante
        /// </summary>
        /// <remarks></remarks>
        /// <param name="ID_Comerciante"></param>
        /// <returns></returns>
        [HttpGet("produtos/{ID_Comerciante}")]
        public async Task<ActionResult<List<ProdutoOutput>>> BuscarProdutoPorComerciante([FromRoute]int ID_Comerciante)
        {
            return Ok(await _service.BuscaProdutoPorComerciante(ID_Comerciante));
        }

        /// <summary>
        /// Buscar Comerciante pelo Identificador ID_Comerciante
        /// </summary>
        /// <param name="ID_Comerciante"></param>
        /// <returns></returns>
        [HttpGet("{ID_Comerciante}")]
        public async Task<ActionResult<ComercianteOutput>> BuscarComerciantePorId([FromRoute]int ID_Comerciante)
        {
            return Ok(await _service.BuscarComerciantePorId(ID_Comerciante));
        }

    }
}