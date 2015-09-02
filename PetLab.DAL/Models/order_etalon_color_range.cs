using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PetLab.DAL.Contracts.Models.Base;
using PetLab.DAL.Models.xml;

namespace PetLab.DAL.Models {
	public class order_etalon_color_range : BaseEntity {
		[Key, ForeignKey("order")]
		[Column(Order = 0)]
		[StringLength(10)]
		public string order_id { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(10)]
		public string name { get; set; }

		public decimal lim1 { get; set; }

		public decimal lim2 { get; set; }

		public decimal lim3 { get; set; }

		public decimal lim4 { get; set; }

		public decimal lim5 { get; set; }

		public virtual order order { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<pickup_etalon_color_range> pickup_etalon_color_ranges { get; set; }
	}
}