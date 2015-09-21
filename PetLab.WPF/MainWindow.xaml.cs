using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Contracts.Settings;
using PetLab.DAL.Models;
using PetLab.WPF.Dialogs;
using PetLab.WPF.Helpers;
using PetLab.WPF.Models;
using PetLab.WPF.Utils;
using PetLab.WPF.ViewModels;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace PetLab.WPF {
	public partial class MainWindow : Window {

		#region [ Converters ]

		/// <summary>
		/// на названию предела и списка съёмов цвета, получает/создаём съём цвета
		/// string, PickupViewModel -> PickupEtalonColorRangeViewModel
		/// </summary>
		public static IMultiValueConverter ColorEtalonToPickupColorConverter =
			new MultiConverterHelper(ToPickupConvert);

		/// <summary>
		/// значение цвета конвертится в цвет (красный, желтый, зеленый)
		/// OrderEtalonColorRangeViewModel, PickupEtalonColorRangeViewModel -> Brush
		/// </summary>
		public static IMultiValueConverter PickupEtalonToBackgroundConverter =
			new MultiConverterHelper(ToBackgroundConvert);

		public static IValueConverter GradeAndCurrentDefectToBrushConverter { get { return new ConverterHelper(GradeAndCurrentDefectToBrushConvert); } }
		public static IMultiValueConverter PickupDefectsSelector { get { return new MultiConverterHelper(PickupDefectsSelect); } }
		public static IValueConverter Inc1Converter { get { return new ConverterHelper(Inc1Convert); } }

		private static object Inc1Convert(object value) {
			return (byte)value + 1;
		}

		//public static IValueConverter OrderToColorEtalonConverter { get { return new ConverterHelper(OrderToColorEtalonConvert); } }
		//public static IValueConverter PickupEtalonCoBackgroundConverter { get { return new ConverterHelper(PickupEtalonCoBackgroundConvert); } }

		/// <summary>
		/// RangeName & PickupViewModel -> PickupEtalonColorRange
		/// </summary>
		private static object ToPickupConvert(object[] values) {
			var rangeName = values[0] as string;
			var pickup = values[1] as PickupViewModel;
			if (rangeName != null && pickup != null) {
				if (pickup.PickupEtalonColorRanges == null) {
					pickup.PickupEtalonColorRanges = new List<PickupEtalonColorRangeViewModel>();
				}
				PickupEtalonColorRangeViewModel range = pickup.PickupEtalonColorRanges.FirstOrDefault(p => p.RangeName == rangeName);
				if (range != null) {
					return range;
				} else {
					range = new PickupEtalonColorRangeViewModel() {
						RangeName = rangeName,
						OrderId = pickup.OrderId,
						PickupId = pickup.PickupId
					};
					((List<PickupEtalonColorRangeViewModel>)pickup.PickupEtalonColorRanges).Add(range);
					return range;
				}
			}
			return null;
		}

		private static object ToBackgroundConvert(object[] values) {
			//PickupEtalonColorRangeViewModel
			var etalon = values[0] as OrderEtalonColorRangeViewModel;
			var pickup = values[1] as PickupEtalonColorRangeViewModel;
			if (etalon != null && pickup != null) {
				var value1 = pickup.Value;
				if (value1 < etalon.Lim1 || value1 > etalon.Lim5) {
					return Brushes.OrangeRed;
				} else if (value1 < etalon.Lim2 || value1 > etalon.Lim4) {
					return Brushes.Yellow;
				} else {
					return Brushes.LightGreen;
				}
			}
			return Brushes.White;
		}

		private static object GradeAndCurrentDefectToBrushConvert(object value) {
			switch (value as byte?) {
				case 1:
					return Brushes.Blue;
				case 2:
					return Brushes.Red;
				default:
					return Brushes.Gray;
			}
		}

		private static object PickupDefectsSelect(object[] values) {
			var defect = values[0] as DefectViewModel;
			var pickup = values[1] as PickupViewModel;
			if (defect != null && pickup != null) {
				return pickup.PickupDefects[defect.DefectId];
			}
			return null;
		}

		#endregion [ Converters ]

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

			var defectsViewModel = Mapper.Map<ObservableCollection<DefectViewModel>>(defects);
			var equipmentsViewModel = Mapper.Map<IEnumerable<EquipmentViewModel>>(equipments);

			// Create view model
			var viewModel = new MainViewModel() {
				Equipments = equipmentsViewModel,
				Defects = defectsViewModel,
				CurrentDefect = defectsViewModel.FirstOrDefault()
			};

			// Set view model to data context
			Model = viewModel;
		}

		private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
			var results = await _service.ExportPickups();
			var pickupsResult = results.GetResult();
			foreach (var serviceResult in pickupsResult) {
				serviceResult.GetResult();
			}
		}

		private void MainWindow_OnClosed(object sender, EventArgs e) {
			// Save user name into settings file
			//var settings = Mapper.Map<PickupSettings>(Model);
			//_settings.SetPickupSettings(settings);
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
			//можно будет разделить и обнулять или order или pickip, вставить 2 try catch
			try {
				Model.IsLoading = true;
				var resultOrder = await _service.LookupOrder(Model.CurrentEquipment.EquipmentId);
				Model.CurrentOrder = Mapper.Map<OrderViewModel>(resultOrder.GetResult());
				var resultPickup = _service.LookupOpenPickup(Model.CurrentEquipment.EquipmentId);
				Model.CurrentPickup = Mapper.Map<PickupViewModel>(resultPickup.GetResult());
			} catch (Exception exception) {
				Model.CurrentOrder = null;
				Model.CurrentPickup = null;
				Model.ErrorMessage = exception.Message;
			} finally {
				Model.IsLoading = false;
			}
		}
		/// <summary>
		/// двойной клик, перегрузить заказ
		/// </summary>
		private void EqListBox_MouseDoubleClicked(object sender, MouseButtonEventArgs e) {
			EqListBox_SelectionChanged(sender, null);
		}

		private void CreatePickup_Clicked(object sender, RoutedEventArgs e) {
			if (Model.CurrentEquipment != null) {
				if (Model.CurrentOrder != null) {
					var host = ServiceLocator.Current.GetInstance<IServicesHost>();
					var settings = ServiceLocator.Current.GetInstance<ISettingsService>();
					var dialog = new CreatePickupDialog(host, settings, Model.CurrentOrder);
					if (dialog.ShowDialog() == true) {
						Model.CurrentPickup = dialog.Pickup;
					}
				} else {
					MessageBox.Show("Нет заказа");
				}
			} else {
				MessageBox.Show("Выбетите машину");
			}
		}

		private void PickupColorDecimalUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
			var range = ((FrameworkElement)sender).DataContext as PickupEtalonColorRangeViewModel;
			var value = ((DecimalUpDown)sender).Value;
			if (range != null && value != null && value != 0) {
				range.Value = value.Value;
				_service.SetColor(Mapper.Map<PickupEtalonColorRangeDto>(range)).CheckResult();
				MultiBindingExpression binding = BindingOperations.GetMultiBindingExpression(((FrameworkElement)sender), DecimalUpDown.BackgroundProperty);
				binding.UpdateTarget();
			}
		}

		private void PickupColorDecimalUpDown_KeyUp(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				PickupColorDecimalUpDown_ValueChanged(sender, null);
				Keyboard.ClearFocus();
				e.Handled = true;
			}
		}

		private void ClosePickup_Clicked(object sender, RoutedEventArgs e) {
			_service.ClosePickup(Model.CurrentPickup.PickupId);
			Model.CurrentPickup = null;
		}

		#endregion [ Private Methods ]

		private void SocketButton_Click(object sender, RoutedEventArgs e) {
			var pickupDefectViewModel = (PickupDefectViewModel)((FrameworkElement)sender).DataContext;
			switch (pickupDefectViewModel.Grade) {
				case 0:
					pickupDefectViewModel.Grade = 1;
					break;
				case 1:
					pickupDefectViewModel.Grade = 2;
					break;
				case 2:
					pickupDefectViewModel.Grade = 0;
					break;
			}
			_service.SetPickupDefect(Mapper.Map<PickupDefectDto>(pickupDefectViewModel));
		}

		private void EtalonMatchCheckBox_CheckChanged(object sender, RoutedEventArgs e) {
			bool? isChecked = ((CheckBox)sender).IsChecked;
			if (Model.CurrentPickup != null && isChecked != null) {
				_service.SetEtalonMatch(Model.CurrentPickup.PickupId, (bool)isChecked);
			}
		}
	}
}
