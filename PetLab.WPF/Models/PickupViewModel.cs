using System;
using System.Collections.Generic;
using PetLab.BLL.Common.Dto;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Models {
	public class PickupViewModel : BaseViewModel {

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
		public IEnumerable<PickupEtalonColorRangeViewModel> PickupEtalonColorRange { get; set; }
	}
}
