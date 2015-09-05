using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Common.Settings;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Contracts.Settings;
using PetLab.BLL.Services;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels;

namespace PetLab.WPF.Dialogs {
	/// <summary>
	/// Interaction logic for LoginDialog.xaml
	/// </summary>
	public partial class LoginDialog : Window {

		#region [ Private Fields ]

		/// <summary>
		/// Identity service
		/// </summary>
		private IIdentityService _service;
		/// <summary>
		/// Settings service
		/// </summary>
		private ISettingsService _settings;

		#endregion [ Private Fields ]

		#region [ Properties ]

		/// <summary>
		/// Gets or sets view model
		/// </summary>
		public LoginViewModel Model {
			get { return DataContext as LoginViewModel; }
			set { DataContext = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors / Destructors ]

		/// <summary>
		/// Constructor
		/// </summary>
		public LoginDialog(IServicesHost host, ISettingsService settings) {
			InitializeComponent();

			_service = host.GetService<IIdentityService>();
			_settings = settings;

			Initialize();

#if DEBUG
			Loaded += (sender, e) => {
				Model.Password = "1";
				btnOK.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
			};
#endif
		}

		#endregion [ Constructors / Destructors ]

		#region [ Private Methods ]

		/// <summary>
		/// Initializes dialog
		/// </summary>
		private void Initialize() {
			var settings = _settings.GetLoginSettings();

			IEnumerable<ShiftDto> shifts = _service.LookupShifts().GetResult();
			IEnumerable<UserDto> users = _service.LookupUsers().GetResult();

			// Create view model
			var viewModel = new LoginViewModel() {
				SelectedUserId = settings.UserId,
				SelectedShiftNumber = settings.ShiftNumber,
				Shifts = Mapper.Map<IEnumerable<ShiftViewModel>>(shifts),
				Users = Mapper.Map<IEnumerable<UserViewModel>>(users)
			};

			// Set focus no necessary element
			if (viewModel.SelectedShiftNumber == null)
				cbShift.Focus();
			else if(viewModel.SelectedUserId == null)
				cbUserName.Focus();
			else
				tbPassword.Focus();
			// Set view model to data context
			Model = viewModel;
		}
		/// <summary>
		/// On OK button clicked
		/// </summary>
		private void OnOKButtonClicked(object sender, RoutedEventArgs e) {
			// Check is model valid
			if (!Model.IsValid)
				return;

			LoginResult result = _service.Login(Model.SelectedShiftNumber.Value, Model.SelectedUserId.Value, Model.Password);


			if (result.Successed) {
				// Save user name into settings file
				var settings = Mapper.Map<LoginSettings>(Model);
				_settings.SetLoginSettings(settings);
				// Go to main window
				DialogResult = true;
			} else {
				tbPassword.Focus();
				tbPassword.SelectAll();
				Model.ErrorMessage = result.GetBaseException().Message;
			}
		}
		/// <summary>
		/// On Cancel button clicked
		/// </summary>
		private void OnCancelButtonClicked(object sender, RoutedEventArgs e) {
			// Close application
			Close();
		}

		#endregion [ Private Methods ]
	}
}
