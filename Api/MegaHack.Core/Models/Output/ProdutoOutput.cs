using System;
using System.Collections.Generic;
using System.Text;

namespace MegaHack.Core.Models.Output
{
    public class ProdutoOutput : BaseOutput
    {
        public int ID_Produto { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public string Imagem { get; set; }
    }
}
