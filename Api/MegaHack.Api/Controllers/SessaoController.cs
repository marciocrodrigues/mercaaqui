using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MegaHack.Api.Config;
using MegaHack.Core.Models.Input;
using MegaHack.Core.Models.Output;
using MegaHack.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Rest;

namespace MegaHack.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SessaoController : ControllerBase
    {
        private readonly SessaoService _service;
        private readonly IConfiguration _configuration;

        public SessaoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _service = new SessaoService(_configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Gerar o token para autenticação
        /// </summary>
        /// <remarks>
        /// Realiza acesso autenticando o email e senha criados no cadastro
        /// </remarks>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<ActionResult> IniciarSessao(SessaoInput param)
        {
            var retorno = await _service.IniciarSessao(param);

            if(retorno.Return_Code > 0)
            {
                var dados = new {
                    Return_Code = retorno.Return_Code,
                    ErrMsg = retorno.ErrMsg
                };

                return BadRequest(dados);
            }
            else
            {
                var dados = new {
                    ID_Identificador = retorno.ID_Identificador,
                    Tipo = retorno.Tipo,
                    Token = Token.GerarToken(retorno)
                };

                return Ok(dados);
            }
        }
    }
}