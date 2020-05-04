using System;
using System.Collections.Generic;
using System.Text;

namespace MegaHack.Core.Models.Output
{
    public class ProcessoOutput : BaseOutput
    {
		public int ID_Processo { get; set; }
		public DateTime Data_Atualizacao { get; set; }
		public DateTime Data_Finalizacao { get; set; }
		public int ID_Comerciante { get; set; }
		public int ID_Cliente { get; set; }
		public int ID_Produto { get; set; }
		public int ID_Entregador { get; set; }
		public string Status { get; set; }
	}
}
