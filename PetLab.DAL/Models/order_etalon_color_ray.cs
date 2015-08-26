using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLab.DAL.Models {
	public class order_etalon_color_ray {
		[Key, ForeignKey("order")]
		[Column(Order = 0)]
		[StringLength(10)]
		public string order_id { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(10)]
		public string ray_id { get; set; }

		public bool value { get; set; }

		public virtual order order { get; set; }
	}
}