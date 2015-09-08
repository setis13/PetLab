using System;
using System.Collections.Generic;

namespace PetLab.BLL.Common.Dto {
	public class PickupDto {
		public int PickupId { get; set; }
		public string OrderId { get; set; }
		public int ShiftId { get; set; }
		public DateTime DatetimeTake { get; set; }
		public DateTime DatetimeCreate { get; set; }
		public DateTime? DatetimeClose { get; set; }
		public string BoxId { get; set; }
		public string StatioName { get; set; }
		public byte Number { get; set; }
		public bool EtalonMatch { get; set; }
		public bool VisualMatch { get; set; }
		public bool Export { get; set; }
		public IEnumerable<PickupEtalonColorRangeDto> PickupEtalonColorRanges { get; set; }
	}
}
