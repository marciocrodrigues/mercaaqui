using System;
using System.Collections.Generic;
using System.Text;

namespace MegaHack.Core.Models.Input
{
    public class ProdutoInput
    {
        /// <summary>
        /// Identificador do produto, caso for um alteração
        /// </summary>
        public int ID_Produto { get; set; }
        /// <summary>
        /// Indetificador do comerciante do produto
        /// </summary>
        public int ID_Comerciante { get; set; }
        /// <summary>
        /// Descrição
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Quantidade vendida
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// Preço
        /// </summary>
        public decimal Preco { get; set; }
        /// <summary>
        /// Imagem do produto, utilizar base64
        /// </summary>
        public string Imagem { get; set; }
    }
}
