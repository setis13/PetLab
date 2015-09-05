using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Common.Settings;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Contracts.Settings;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels;

namespace PetLab.WPF.Dialogs {
	/// <summary>
	/// Interaction logic for DefectsDialog.xaml
	/// </summary>
	public partial class DefectsDialog : Window {

		#region [ Private Fields ]

		/// <summary>
		/// Defects service
		/// </summary>
		private IDefectsService _service;
		/// <summary>
		/// Settings service
		/// </summary>
		private ISettingsService _settings;

		#endregion [ Private Fields ]

		#region [ Properties ]

		/// <summary>
		/// Gets or sets view model
		/// </summary>
		public DefectSyncViewModel Model {
			get { return DataContext as DefectSyncViewModel; }
			set { DataContext = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors / Destructors ]

		public DefectsDialog(IServicesHost host, ISettingsService settings) {
			InitializeComponent();

			_service = host.GetService<IDefectsService>();
			_settings = settings;

			Initialize();
		}

		#endregion [ Constructors / Destructors ]

		#region [ Private Methods ]

		/// <summary>
		/// Initializes dialog
		/// </summary>
		private void Initialize() {
			var settings = _settings.GetLoginSettings();

			IEnumerable<DefectDto> defects = _service.LookupDefects().GetResult();

			// Create view model
			var viewModel = new DefectSyncViewModel() {
				Defects = Mapper.Map<IEnumerable<DefectViewModel>>(defects)
			};
			// Set view model to data context
			Model = viewModel;
		}

		/// <summary>
		/// On OK button clicked
		/// </summary>
		private async void OnSyncButtonClicked(object sender, RoutedEventArgs e) {
			Model.IsLoading = true;
			var result = await _service.LookupXmlDefectsAsync();
			IEnumerable<DefectXmlDto> defectsXmlDto = result.GetResult();

			if (result.Successed) {
				var syncResult = _service.UpdateDefects(defectsXmlDto);
				if (syncResult.Successed) {
					Model.AlertMessage = "Синхронизация завершена";
					IEnumerable<DefectDto> defects = _service.LookupDefects().GetResult();
					Model.Defects = Mapper.Map<IEnumerable<DefectViewModel>>(defects);
				} else {
					Model.ErrorMessage = syncResult.GetBaseException().Message;
				}
			} else {
				Model.ErrorMessage = result.GetBaseException().Message;
			}
			Model.IsLoading = false;
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
