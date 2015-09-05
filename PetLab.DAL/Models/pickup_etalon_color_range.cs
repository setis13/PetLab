using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	public class pickup_etalon_color_range : BaseEntity {
		[Key, ForeignKey("pickup")]
		[Column(Order = 0)]
		public int pickup_id { get; set; }

		[Key, ForeignKey("order_etalon_color_range")]
		[Column(Order = 1, TypeName = "VARCHAR")]
		[StringLength(10)]
		public string order_id { get; set; }

		[Key, ForeignKey("order_etalon_color_range")]
		[StringLength(10)]
		[Column(Order = 2, TypeName = "VARCHAR")]
		public string range_name { get; set; }

		public decimal value { get; set; }

		public virtual pickup pickup { get; set; }

		public virtual order_etalon_color_range order_etalon_color_range { get; set; }
	}
}
