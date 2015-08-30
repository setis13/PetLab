using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	[Table("material")]
	public class material : BaseEntity {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public material() {
			orders = new HashSet<order>();
		}

		[Key]
		[StringLength(10)]
		public string material_id { get; set; }

		[Required]
		[StringLength(50)]
		public string name { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order> orders { get; set; }
	}
}