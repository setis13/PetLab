using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Models {
	public class OrderViewModel : BaseViewModel {
		private OrderEtalonColorViewModel _etalonColor;
		public string OrderId { get; set; }
		public string BatchId { get; set; }
		public string MaterialName { get; set; }
		public byte ShiftNumber { get; set; }
		public string DyeName { get; set; }
		public string ColorShade { get; set; }
		public byte CountSocket { get; set; }
		public string EquipmentId { get; set; }

		public OrderEtalonColorViewModel EtalonColor {
			get { return _etalonColor; }
			set {
				_etalonColor = value; 
				OnPropertyChanged();
			}
		}
	}
}
