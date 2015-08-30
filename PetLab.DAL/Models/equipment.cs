using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	[Table("equipment")]
	public class equipment : BaseEntity {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public equipment() {
			orders = new HashSet<order>();
		}

		[Key]
		[StringLength(8)]
		public string equipment_id { get; set; }

		public int? pickup_id { get; set; }

		public virtual pickup pickup { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order> orders { get; set; }
	}
}