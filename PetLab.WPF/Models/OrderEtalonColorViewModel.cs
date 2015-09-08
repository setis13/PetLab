using System.Collections.Generic;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Models {
	public class OrderEtalonColorViewModel :BaseViewModel {
		private IEnumerable<OrderEtalonColorRangeViewModel> _ranges;
		public string Name { get; set; }
		public byte SocketNumber { get; set; }
		public byte PickupMode { get; set; }

		public IEnumerable<OrderEtalonColorRangeViewModel> Ranges {
			get { return _ranges; }
			set {
				_ranges = value; 
				OnPropertyChanged();
			}
		}

		public IEnumerable<OrderEtalonColorRayViewModel> Rays {get; set; }
    }
}