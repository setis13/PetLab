using System.Collections.Generic;
using PetLab.BLL.Contracts.Services;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Models {
	public class PetSocketViewModel :BaseViewModel {
		public byte Number { get; set; }
		public byte CountSockets { get; set; }
		/// <summary>
		/// null/0 - по умолчанию; false/1 - допустимый; true/2 - критичный
		/// </summary>
		public List<PickupDefectViewModel> Defects { get; set; }

		public void DefectsRaisePropertyChanged() {
			OnPropertyChanged(nameof(Defects));
		}

		public PickupViewModel Pickup { get; set; }

		public IPickupService Service { get; set; }
	}
}
