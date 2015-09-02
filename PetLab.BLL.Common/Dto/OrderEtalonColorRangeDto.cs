using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetLab.BLL.Common.Dto {
	public class OrderEtalonColorRangeDto {
		public string OrderId { get; set; }
		public string Name { get; set; }
		public decimal Lim1 { get; set; }
		public decimal Lim2 { get; set; }
		public decimal Lim3 { get; set; }
		public decimal Lim4 { get; set; }
		public decimal Lim5 { get; set; }
	}
}
