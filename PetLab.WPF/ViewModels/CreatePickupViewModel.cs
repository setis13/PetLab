using System;
using System.Collections.Generic;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.ViewModels {
	public class CreatePickupViewModel : BaseViewModel {
		/// <summary>
		/// Error message
		/// </summary>
		private string _errorMessage;

		public IEnumerable<CoolingStationViewModel> CoolingStations { get; set; }
		public CoolingStationViewModel SelectedCoolingStation { get; set; }
		public OrderViewModel Order { get; set; }
		public byte PickupNumber { get; set; }
		public TimeSpan Take { get; set; }
		public ShiftViewModel Shift { get; set; }

		/// <summary>
		/// Gets or sets error message
		/// </summary>
		public string ErrorMessage {
			get { return _errorMessage; }
			set {
				_errorMessage = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Is model data valid
		/// </summary>
		/// <returns>True - if valid</returns>
		public bool IsValid {
			get {
				if (SelectedCoolingStation == null) {
					ErrorMessage = "Выберите станцию охлаждения";
					return false;
				}
				return true;
			}
		}
	}
}
