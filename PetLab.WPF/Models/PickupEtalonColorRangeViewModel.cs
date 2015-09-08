using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Models {
	public class PickupEtalonColorRangeViewModel : BaseViewModel {
		private decimal _value;
		public int PickupId { get; set; }
		public string RangeName { get; set; }
		public string OrderId { get; set; }

		public decimal Value {
			get { return _value; }
			set {
				_value = value;
				OnPropertyChanged();
			}
		}
	}
}
