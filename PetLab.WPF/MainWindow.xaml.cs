﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Contracts.Settings;
using PetLab.DAL.Models;
using PetLab.WPF.Dialogs;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels;

namespace PetLab.WPF {
	public partial class MainWindow : Window {

		#region [ Private Fields ]

		/// <summary>
		/// Main service
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
		public MainViewModel Model {
			get { return DataContext as MainViewModel; }
			set { DataContext = value; }
		}

		#endregion [ Properties ]

		//public static IMultiValueConverter CountSocketsToButtonsConverter { get { return new MultiConverterHelper(CountSocketsToButtonsConvert); } }
		//public static IMultiValueConverter GradeAndCurrentDefectToBrushConverter { get { return new MultiConverterHelper(GradeAndCurrentDefectToBrushConvert); } }
		//public static IValueConverter CountSockToBlockSockConverter { get { return new ConverterHelper(CountSockToBlockSockConvert); } }
		//public static IValueConverter OrderToColorEtalonConverter { get { return new ConverterHelper(OrderToColorEtalonConvert); } }
		//public static IValueConverter PickupEtalonCoBackgroundConverter { get { return new ConverterHelper(PickupEtalonCoBackgroundConvert); } }

		#region [ Constructors / Destructors ]

		public MainWindow(IServicesHost host, ISettingsService settings) {
			InitializeComponent();

			_service = host.GetService<IPickupService>();
			_settings = settings;

			Initialize();
		}

		#endregion [ Constructors / Destructors ]


		#region [ Private Methods ]

		/// <summary>
		/// Initializes dialog
		/// </summary>
		private void Initialize() {
			//var settings = _settings.GetPickupSettings();

			IEnumerable<EquipmentDto> equipments = _service.LookupEquipments().GetResult();
			IEnumerable<DefectDto> defects = _service.LookupDefects().GetResult();

			// Create view model
			var viewModel = new MainViewModel() {
				Equipments = Mapper.Map<IEnumerable<EquipmentViewModel>>(equipments),
				Defects = Mapper.Map<ObservableCollection<DefectViewModel>>(defects)
			};

			// Set view model to data context
			Model = viewModel;
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
		}

		private void MainWindow_OnClosed(object sender, EventArgs e) {
			// Save user name into settings file
			//var settings = Mapper.Map<PickupSettings>(Model);
			//_settings.SetPickupSettings(settings);
		}

		private void SettingExportImportClick(object sender, RoutedEventArgs e) {
		}

		private void ImportDefectsClick(object sender, RoutedEventArgs e) {
			var dialog = ServiceLocator.Current.GetInstance<DefectsDialog>();
			if (dialog.ShowDialog() == true) {

			}
		}

		private void ImportMaterialsClick(object sender, RoutedEventArgs e) {
			var dialog = ServiceLocator.Current.GetInstance<MaterialsDialog>();
			if (dialog.ShowDialog() == true) {

			}
		}

		private async void EqListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				Model.IsLoading = true;
				var resultOrder = await _service.LookupOrder(Model.CurrentEquipment.EquipmentId);
				Model.CurrentOrder = Mapper.Map<OrderViewModel>(resultOrder.GetResult());
				var resultPickup = _service.LookupOpenPickup(Model.CurrentEquipment.EquipmentId);
				Model.CurrentPickup = Mapper.Map<PickupViewModel>(resultPickup.GetResult());
			} catch (Exception exception) {
				Model.ErrorMessage = exception.Message;
			} finally {
				Model.IsLoading = false;
			}
		}

		#endregion [ Private Methods ]

		private void CreatePickup_Clicked(object sender, RoutedEventArgs e) {
			if (Model.CurrentEquipment != null) {
				if (Model.CurrentOrder != null) {
					var host = ServiceLocator.Current.GetInstance<IServicesHost>();
					var settings = ServiceLocator.Current.GetInstance<ISettingsService>();
					var dialog = new CreatePickupDialog(host, settings, Model.CurrentOrder);
					if (dialog.ShowDialog() == true) {

					}
				} else {
					MessageBox.Show("Нет заказа");
				}
			} else {
				MessageBox.Show("Выбетите машину");
			}
		}

		private void ClosePickup_Clicked(object sender, RoutedEventArgs e) {
			throw new NotImplementedException();
		}
	}
}
