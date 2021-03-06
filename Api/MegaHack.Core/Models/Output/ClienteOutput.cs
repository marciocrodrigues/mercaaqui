﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MegaHack.Core.Models.Output
{
    public class ClienteOutput : BaseOutput
    {
        /// <summary>
        /// Identificador do cliente
        /// </summary>
        public int ID_Cliente { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Logradouro
        /// </summary>
        public string Logradouro { get; set; }
        /// <summary>
        /// Número
        /// </summary>
        public string Numero { get; set; }
        /// <summary>
        /// Cep
        /// </summary>
        public string Cep { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// DDD
        /// </summary>
        public string DDD { get; set; }
        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Imagem do cliente
        /// </summary>
        public string Imagem { get; set; }
    }
}
