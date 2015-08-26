using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PetLab.DAL.Models {
	[Table("order")]
	public class order {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public order() {
			order_etalon_color_range = new HashSet<order_etalon_color_range>();
			order_etalon_color_ray = new HashSet<order_etalon_color_ray>();
			pickups = new HashSet<pickup>();
		}

		[Key]
		[StringLength(10)]
		public string order_id { get; set; }

		[Required]
		[StringLength(10)]
		public string batch_id { get; set; }

		[Required]
		[StringLength(10)]
		public string material_id { get; set; }

		public int shift_id { get; set; }

		[Required]
		[StringLength(50)]
		public string dye_name { get; set; }

		[Required]
		[StringLength(50)]
		public string color_shade { get; set; }

		public short count_socket { get; set; }

		[Required]
		[StringLength(8)]
		public string equipment_id { get; set; }

		public virtual equipment equipment { get; set; }

		public virtual material material { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order_etalon_color_range> order_etalon_color_range { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order_etalon_color_ray> order_etalon_color_ray { get; set; }

		public virtual order_etalon_slip order_etalon_slip { get; set; }

		public virtual order_etalon_thickness order_etalon_thickness { get; set; }

		public virtual order_etalon_weight order_etalon_weight { get; set; }

		public virtual order_etalon_color order_etalon_color { get; set; }

		public virtual shift shift { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<pickup> pickups { get; set; }
	}
}