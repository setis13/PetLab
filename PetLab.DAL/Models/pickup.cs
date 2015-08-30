using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	[Table("pickup")]
	public class pickup : BaseEntity {
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public pickup() {
			equipments = new HashSet<equipment>();
			pickup_defects = new HashSet<pickup_defects>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int pickup_id { get; set; }

		[Required]
		[StringLength(10)]
		public string order_id { get; set; }

		public int shift_id { get; set; }

		[Column(TypeName = "smalldatetime")]
		public DateTime datetime_take { get; set; }

		[Column(TypeName = "smalldatetime")]
		public DateTime datetime_create { get; set; }

		[Column(TypeName = "smalldatetime")]
		public DateTime? datetime_close { get; set; }

		public int? box_id { get; set; }

		public byte station_id { get; set; }

		public byte number { get; set; }

		public bool etalon_match { get; set; }

		public bool visual_match { get; set; }

		public bool export { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<equipment> equipments { get; set; }

		public virtual order order { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<pickup_defects> pickup_defects { get; set; }

		public virtual pickup_station_cooling pickup_station_cooling { get; set; }

		public virtual shift shift { get; set; }
	}
}