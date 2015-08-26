using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PetLab.DAL.Models {
	public class shift_time {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public shift_time() {
			shifts = new HashSet<shift>();
		}

		[Key]
		public byte time_id { get; set; }

		[Required]
		[StringLength(8)]
		public string name { get; set; }

		public TimeSpan begin { get; set; }

		public TimeSpan end { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<shift> shifts { get; set; }
	}
}