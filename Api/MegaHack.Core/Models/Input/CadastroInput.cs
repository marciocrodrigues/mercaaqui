namespace MegaHack.Core.Models.Input
{
    public class CadastroInput
    {
        /// <summary>
        /// Nome da pessoa
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Nome fantasia, caso for cadastro de comerciante
        /// </summary>
        public string Nome_Fantasia { get; set; }
        /// <summary>
        /// Documento
        /// CPF = Pessoa fisica
        /// CNPJ = Pessoa juridica
        /// CNH = Entregador
        /// </summary>
        public string Documento { get; set; }
        /// <summary>
        /// Email para efetuar login posteriomente
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha para efetuar login posteriormente
        /// </summary>
        public string Senha { get; set; }
        /// <summary>
        /// Logradouro
        /// </summary>
        public string Logradouro { get; set; }
        /// <summary>
        /// Numero
        /// </summary>
        public string Numero { get; set; }
        /// <summary>
        /// Bairro
        /// </summary>
        public string Bairro { get; set; }
        /// <summary>
        /// Cep
        /// Utilizar a seguinte estrutura 00000-000
        /// </summary>
        public string Cep { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Estado, utilizar digitos da unidade federal
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// DDD, preencher com 2 caracteres, 00
        /// </summary>
        public string DDD { get; set; }
        /// <summary>
        /// Telefone, preencher com '-', 00000-0000
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Tipo de cadastro
        /// CO = Comerciante
        /// CL = Cliente
        /// EN = Entregador
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Cadastro da imagem quando cadastro de comerciante, utilizar base64
        /// </summary>
        public string Imagem { get; set; }

    }
}