using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLab.DAL.Models {
	public class order_etalon_color {
		[Key, ForeignKey("order")]
		[StringLength(10)]
		public string order_id { get; set; }

		[Required]
		[StringLength(50)]
		public string name { get; set; }

		public byte socket_number { get; set; }

		public byte pivkup_mode { get; set; }

		public virtual order order { get; set; }
	}
}