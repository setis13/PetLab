using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	public class order_etalon_color_range : BaseEntity {
		[Key, ForeignKey("order")]
		[Column(Order = 0)]
		[StringLength(10)]
		public string order_id { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(20)]
		public string name { get; set; }

		public decimal lim1 { get; set; }

		public decimal lim2 { get; set; }

		public decimal lim3 { get; set; }

		public decimal lim4 { get; set; }

		public decimal lim5 { get; set; }

		public virtual order order { get; set; }
	}
}