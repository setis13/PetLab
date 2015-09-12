using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Models {
	public class PickupDefectViewModel : BaseViewModel {
		private byte _grade;
		public byte Socket { get; set; }
		public byte CountSockets { get; set; }
		public string DefectId { get; set; }

		public byte Grade {
			get { return _grade; }
			set {
				_grade = value;
				OnPropertyChanged();
			}
		}

		public int PickupId { get; set; }
	}
}
