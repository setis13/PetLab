﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace PetLab.WPF.Converters {
	[ValueConversion(typeof(object), typeof(bool))]
	public class ObjectToBoolInvertConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return value == null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
