﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PetLab.WPF.Converters {
	[ValueConversion(typeof(object), typeof(Visibility))]
	public class ObjectToVisibilityConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return value != null ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
