using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order_etalon_color_range> order_etalon_color_ranges { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order_etalon_color_ray> order_etalon_color_rays { get; set; }

	}
}