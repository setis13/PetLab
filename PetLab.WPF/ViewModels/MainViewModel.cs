using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.ViewModels {
	public class MainViewModel : BaseViewModel {

		#region [ Private Fields ]

		/// <summary>
		/// Is data loading now
		/// </summary>
		private bool _isLoading;
		/// <summary>
		/// Error message
		/// </summary>
		private string _errorMessage;
		/// <summary>
		/// Alert message
		/// </summary>
		private string _alertMessage;
		/// <summary>
		/// список дефектов
		/// </summary>
		private ObservableCollection<DefectViewModel> _defects;

		private EquipmentViewModel _currentEquipment;

		private OrderViewModel _currentOrder;

		private PickupViewModel _currentPickup;

		#endregion [ Private Fields ]

		#region [ Properties ]

		/// <summary>
		/// список дефектов
		/// </summary>
		public ObservableCollection<DefectViewModel> Defects {
			get { return _defects; }
			set {
				_defects = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// список машин
		/// </summary>
		public IEnumerable<EquipmentViewModel> Equipments { get; set; }

		/// <summary>
		/// текущая машина
		/// </summary>
		public EquipmentViewModel CurrentEquipment {
			get { return _currentEquipment; }
			set {
				_currentEquipment = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// текущий заказ
		/// </summary>
		public OrderViewModel CurrentOrder {
			get { return _currentOrder; }
			set {
				_currentOrder = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// текущий съём
		/// </summary>
		public PickupViewModel CurrentPickup {
			get { return _currentPickup; }
			set {
				_currentPickup = value;
				OnPropertyChanged();
			}
		}

		public DefectViewModel CurrentDefect { get; set; }

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
		/// Gets or sets alert message
		/// </summary>
		public string AlertMessage {
			get { return _alertMessage; }
			set {
				_alertMessage = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets is data loading now
		/// </summary>
		public bool IsLoading {
			get { return _isLoading; }
			set {
				_isLoading = value;
				OnPropertyChanged();
			}
		}

		#endregion [ Properties ]


		#region [ Constructors / Destructors ]

		/// <summary>
		/// Constructor
		/// </summary>
		public MainViewModel() {
		}

		#endregion [ Constructors / Destructors ]
	}
}
