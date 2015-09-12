using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	[Table("shift")]
	public class shift : BaseEntity {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public shift() {
			pickups = new HashSet<pickup>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int shift_id { get; set; }

		[Column(TypeName = "smalldatetime")]
		public DateTime datetime { get; set; }

		public byte shift_number { get; set; }

		public byte time_id { get; set; }

		public int user_id { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<pickup> pickups { get; set; }

		public virtual shift_number shift_number1 { get; set; }

		public virtual shift_time shift_time { get; set; }

		public virtual user user { get; set; }
	}
}