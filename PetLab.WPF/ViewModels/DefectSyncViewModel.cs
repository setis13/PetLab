using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF.ViewModels {
	public class DefectSyncViewModel : BaseViewModel {

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

		private IEnumerable<DefectViewModel> _defects;

		#endregion [ Private Fields ]

		#region [ Properties ]

		/// <summary>
		/// список пользователей
		/// </summary>
		public IEnumerable<DefectViewModel> Defects {
			get { return _defects; }
			set {
				_defects = value;
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
		public DefectSyncViewModel() {
		}

		#endregion [ Constructors / Destructors ]
	}
}
