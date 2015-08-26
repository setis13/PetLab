using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PetLab.DAL.Models {
	[Table("defect")]
	public class defect {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public defect() {
			pickup_defects = new HashSet<pickup_defects>();
		}

		[Key]
		[StringLength(4)]
		public string defect_id { get; set; }

		[Required]
		[StringLength(50)]
		public string name { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<pickup_defects> pickup_defects { get; set; }
	}
}