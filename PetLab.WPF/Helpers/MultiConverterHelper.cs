using System;
using System.Globalization;
using System.Windows.Data;

namespace PetLab.WPF.Helpers {

	/// <summary>
	/// Create multi converter based on the delegates
	/// </summary>
	public class MultiConverterHelper : IMultiValueConverter {

		#region private fields

		private readonly Func<object[], object> _convertTo;
		private readonly Func<object, object[]> _convertBack;

		#endregion private fields

		#region .ctr

		public MultiConverterHelper(Func<object[], object> convertTo, Func<object, object[]> convertBack = null) {
			_convertTo = convertTo;
			_convertBack = convertBack;
		}

		#endregion .ctr

		#region interface implementation

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			return _convertTo(values);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			return _convertBack(value);
		}

		#endregion interface implementation

	}
}
