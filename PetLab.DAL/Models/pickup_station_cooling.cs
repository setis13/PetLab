using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PetLab.DAL.Models {
	public class pickup_station_cooling {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public pickup_station_cooling() {
			pickups = new HashSet<pickup>();
		}

		[Key]
		public byte station_id { get; set; }

		[Required]
		[StringLength(1)]
		public string name { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<pickup> pickups { get; set; }
	}
}