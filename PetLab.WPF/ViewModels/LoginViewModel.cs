using System;
using System.Collections.Generic;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.ViewModels {
	public class LoginViewModel : BaseViewModel {

		#region [ Private Fields ]

		/// <summary>
		/// Error message
		/// </summary>
		private string _errorMessage;

		/// <summary>
		/// Dat Power user password
		/// </summary>
		private string _password;

		#endregion [ Private Fields ]

		#region [ Properties ]

		/// <summary>
		/// список пользователей
		/// </summary>
		public IEnumerable<UserViewModel> Users { get; set; }

		/// <summary>
		/// список смен
		/// </summary>
		public IEnumerable<ShiftViewModel> Shifts { get; set; }

		/// <summary>
		/// выбранный пользователь
		/// </summary>
		private int? _selectedUserId;
		public int? SelectedUserId {
			get { return _selectedUserId; }
			set {
				_selectedUserId = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// выбранная смена
		/// </summary>
		private byte? _selectedShiftNumber;
		public byte? SelectedShiftNumber {
			get { return _selectedShiftNumber; }
			set {
				_selectedShiftNumber = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets Dat Power user password
		/// </summary>
		public string Password {
			get { return _password; }
			set {
				_password = value;
				OnPropertyChanged();
			}
		}

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
				if (SelectedUserId == null) {
					ErrorMessage = "Выберите пользователя";
					return false;
				}

				if (SelectedShiftNumber == null) {
					ErrorMessage = "Выберите смену";
					return false;
				}

				if (String.IsNullOrEmpty(Password)) {
					ErrorMessage = "Введите пароль";
					return false;
				}

				return true;
			}
		}

		#endregion [ Properties ]


		#region [ Constructors / Destructors ]

		/// <summary>
		/// Constructor
		/// </summary>
		public LoginViewModel() {
			ErrorMessage = String.Empty;
		}

		#endregion [ Constructors / Destructors ]
	}
}
