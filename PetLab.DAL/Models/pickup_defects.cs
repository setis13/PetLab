using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Models {
	public class pickup_defects : BaseEntity {
		[Key]
		[Column(Order = 0)]
		public byte socket { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(4)]
		public string defect_id { get; set; }

		public byte grade { get; set; }

		[Key]
		[Column(Order = 2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int pickup_id { get; set; }

		public virtual defect defect { get; set; }

		public virtual pickup pickup { get; set; }
	}
}