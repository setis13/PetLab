using System;
using System.Globalization;
using System.Windows.Data;

namespace PetLab.WPF.Helpers {

	/// <summary>
	/// Create converter based on the delegates
	/// </summary>
	public class ConverterHelper : IValueConverter {

		#region private fields

		private readonly Func<object, object> _convertTo;
		private readonly Func<object, object> _convertBack;

		#endregion private fields

		#region .ctr

		public ConverterHelper(Func<object, object> convertTo, Func<object, object> convertBack = null) {
			_convertTo = convertTo;
			_convertBack = convertBack;
		}

		#endregion .ctr

		#region interface implementation

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return _convertTo(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return _convertBack(value);
		}

		#endregion interface implementation

	}
}
