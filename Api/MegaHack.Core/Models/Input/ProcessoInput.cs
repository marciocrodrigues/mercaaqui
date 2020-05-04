using System;
using System.Collections.Generic;
using System.Text;

namespace MegaHack.Core.Models.Input
{
    public class ProcessoInput
    {
		/// <summary>
		/// Indetificador do processo caso for alteração
		/// </summary>
		public int ID_Processo { get; set; }
		public DateTime Data_Atualizacao { get; set; }
		/// <summary>
		/// Data de finalização quando o processo for completado
		/// </summary>
		public DateTime Data_Finalizacao { get; set; }
		/// <summary>
		/// Identificador do comerciante do produto
		/// </summary>
		public int ID_Comerciante { get; set; }
		/// <summary>
		/// Identificador do cliente comprador
		/// </summary>
		public int ID_Cliente { get; set; }
		/// <summary>
		/// Identificador do produto adquirido
		/// </summary>
		public int ID_Produto { get; set; }
		/// <summary>
		/// Indetificador do entregador selecionado
		/// </summary>
		public int ID_Entregador { get; set; }
		/// <summary>
		/// Status da entrega
		/// </summary>
		public string Status { get; set; }
		/// <summary>
		/// Número do comprovante gerado
		/// </summary>
		public string NO_Comprovante { get; set; }
		/// <summary>
		/// Quantidade comprada
		/// </summary>
		public int Quantidade { get; set; }
	}
}
