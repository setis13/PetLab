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
	/// Interaction logic for MaterialsDialog.xaml
	/// </summary>
	public partial class MaterialsDialog : Window {

		#region [ Private Fields ]

		/// <summary>
		/// Materials service
		/// </summary>
		private IMaterialsService _service;
		/// <summary>
		/// Settings service
		/// </summary>
		private ISettingsService _settings;

		#endregion [ Private Fields ]

		#region [ Properties ]

		/// <summary>
		/// Gets or sets view model
		/// </summary>
		public MaterialSyncViewModel Model {
			get { return DataContext as MaterialSyncViewModel; }
			set { DataContext = value; }
		}

		#endregion [ Properties ]

		#region [ Constructors / Destructors ]

		public MaterialsDialog(IServicesHost host, ISettingsService settings) {
			InitializeComponent();

			_service = host.GetService<IMaterialsService>();
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

			IEnumerable<MaterialDto> Materials = _service.LookupMaterials().GetResult();

			// Create view model
			var viewModel = new MaterialSyncViewModel() {
				Materials = Mapper.Map<IEnumerable<MaterialViewModel>>(Materials)
			};
			// Set view model to data context
			Model = viewModel;
		}

		/// <summary>
		/// On OK button clicked
		/// </summary>
		private async void OnSyncButtonClicked(object sender, RoutedEventArgs e) {
			Model.IsLoading = true;
			var result = await _service.LookupXmlMaterialsAsync();
			IEnumerable<MaterialXmlDto> MaterialsXmlDto = result.GetResult();

			if (result.Successed) {
				var syncResult = _service.UpdateMaterials(MaterialsXmlDto);
				if (syncResult.Successed) {
					Model.AlertMessage = "Синхронизация завершена";
					IEnumerable<MaterialDto> Materials = _service.LookupMaterials().GetResult();
					Model.Materials = Mapper.Map<IEnumerable<MaterialViewModel>>(Materials);
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
