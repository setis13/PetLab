using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	public class order_etalon_color : BaseEntity {
		[Key, ForeignKey("order")]
		[StringLength(10)]
		[Column(TypeName = "VARCHAR")]
		public string order_id { get; set; }

		[Required]
		[StringLength(20)]
		[Column(TypeName = "VARCHAR")]
		public string name { get; set; }

		public byte socket_number { get; set; }

		public byte pickup_mode { get; set; }

		public virtual order order { get; set; }
	}
}