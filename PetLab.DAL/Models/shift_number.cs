using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PetLab.DAL.Models {
	public class shift_number {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public shift_number() {
			shifts = new HashSet<shift>();
		}

		[Key]
		public byte number { get; set; }

		public int user_id { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<shift> shifts { get; set; }

		public virtual user user { get; set; }
	}
}