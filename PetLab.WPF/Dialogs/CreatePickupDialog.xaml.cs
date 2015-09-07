using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Contracts.Settings;
using PetLab.DAL.Models;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.Dialogs {
	public partial class CreatePickupDialog : Window {

		#region [ Public ]

		/// <summary>
		/// созданный съём
		/// </summary>
		public PickupViewModel Pickup { get; set; }

		#endregion [ Public ]

		#region [ Private Fields ]

		/// <summary>
		/// Pickup service
		/// </summary>
		private IPickupService _service;
		/// <summary>
		/// Settings service
		/// </summary>
		private ISettingsService _settings;

		#endregion [ Private Fields ]

		#region [ Properties ]

		/// <summary>
		/// Gets or sets view model
		/// </summary>
		public CreatePickupViewModel Model {
			get { return DataContext as CreatePickupViewModel; }
			set { DataContext = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors / Destructors ]

		public CreatePickupDialog(IServicesHost host, ISettingsService settings, OrderViewModel order) {
			InitializeComponent();

			var shift = Mapper.Map<ShiftViewModel>(host.GetService<IIdentityService>().Shift);
			_service = host.GetService<IPickupService>();
			_settings = settings;

			Initialize(order, shift);
		}

		#endregion [ Constructors / Destructors ]


		#region [ Private Methods ]

		/// <summary>
		/// Initializes dialog
		/// </summary>
		private void Initialize(OrderViewModel order, ShiftViewModel shift) {
			IEnumerable<CoolingStationDto> coolingStations = _service.LookupCoolingStations().GetResult();

			// Create view model
			var viewModel = new CreatePickupViewModel() {
				CoolingStations = Mapper.Map<IEnumerable<CoolingStationViewModel>>(coolingStations),
				Order = order,
				Shift = shift,
				Take = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0)
			};
			// Set view model to data context
			Model = viewModel;
		}

		/// <summary>
		/// On OK button clicked
		/// </summary>
		private void OnOkButtonClicked(object sender, RoutedEventArgs e) {
			if (!Model.IsValid) {
				return;
			}
			
			var time = new DateTime(
				DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
				Model.Take.Hours, Model.Take.Minutes, Model.Take.Seconds);
			var result = _service.OpenPickup(Model.Order.OrderId, null, Model.Shift.ShiftId,
				time, Model.SelectedCoolingStation.StationId);
			Pickup = Mapper.Map<PickupViewModel>(result.GetResult());

			if (result.Successed) {
				Close();
			} else {
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
