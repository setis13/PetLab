using System;
using System.Collections.Generic;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Models {
	public class PickupViewModel : BaseViewModel {
		private bool _etalonMatch;
		private bool _visualMatch;

		public int PickupId { get; set; }
		public string OrderId { get; set; }
		public int ShiftId { get; set; }
		public DateTime DatetimeTake { get; set; }
		public DateTime DatetimeCreate { get; set; }
		public DateTime? DatetimeClose { get; set; }
		public string BoxId { get; set; }
		public string StatioName { get; set; }
		public byte Number { get; set; }
		public bool EtalonMatch {
			get { return _etalonMatch; }
			set {
				_etalonMatch = value;
				OnPropertyChanged();
			}
		}

		public bool VisualMatch {
			get { return _visualMatch; }
			set {
				_visualMatch = value;
				OnPropertyChanged();
			}
		}

		public bool Export { get; set; }
		public IEnumerable<PickupEtalonColorRangeViewModel> PickupEtalonColorRanges { get; set; }
		public Dictionary<string, PickupDefectViewModel[]> PickupDefects { get; set; }
	}
}
