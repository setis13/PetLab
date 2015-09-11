using System;
using System.Windows;
using System.Windows.Controls;
using PetLab.WPF.Models;

namespace PetLab.WPF.Utils {
	public static class GridUtils {

		/// <summary>
		/// Identifies the PetSocket attached property. 
		/// </summary>
		public static readonly DependencyProperty PetSocketProperty =
			DependencyProperty.RegisterAttached("PetSocket", typeof(bool), typeof(GridUtils),
				new PropertyMetadata(false, new PropertyChangedCallback(PetSocketPropertyChanged)));

		/// <summary>
		/// Gets the value of the PetSocket property
		/// </summary>
		public static bool GetPetSocket(DependencyObject d) {
			return (bool)d.GetValue(PetSocketProperty);
		}

		/// <summary>
		/// Sets the value of the PetSocket property
		/// </summary>
		public static void SetPetSocket(DependencyObject d, bool value) {
			d.SetValue(PetSocketProperty, value);
		}

		/// <summary>
		/// Handles property changed event for the ItemsPerRow property, constructing
		/// the required PetSocket elements on the grid which this property is attached to.
		/// </summary>
		public static void PetSocketPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			Grid grid = d as Grid;

			// construct the required row definitions
			grid.LayoutUpdated += (s, e2) => {

				var maxColumn = 6;
				var maxRow = 16;

				foreach (FrameworkElement child in grid.Children) {
					Grid.SetColumn(child, NumberToColumnConvert((PickupDefectViewModel)child.DataContext));
					Grid.SetRow(child, NumberToRowConvert((PickupDefectViewModel)child.DataContext));
				}

				for (int row = 0; row < maxRow - grid.RowDefinitions.Count; row++) {
					grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
				}

				// set the row property for each chid
				for (int i = 0; i < maxColumn - grid.ColumnDefinitions.Count; i++) {
					grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
				}
			};
		}

		public static byte BaseCountSockets(byte countSockets) {
			if (countSockets <= 48 && countSockets >= 42) {
				return 48;
			}
			if (countSockets <= 72 && countSockets >= 66) {
				return 72;
			}
			if (countSockets <= 96 && countSockets >= 90) {
				return 96;
			}
			throw new Exception("Неверное кол-во гнёзд");
		}

		private static int NumberToRowConvert(PickupDefectViewModel petSocket) {
			var number = petSocket.Socket;
			switch (/*BaseCountSockets*/(petSocket.CountSockets)) {
				case 48:
					return number / 4;
				case 72:
					return number / 6;
				case 96:
					return number / 6;
			}
			throw new Exception("Неверное кол-во гнёзд");
		}

		private static int NumberToColumnConvert(PickupDefectViewModel petSocket) {
			var number = petSocket.Socket;
			switch (BaseCountSockets(petSocket.CountSockets)) {
				case 48:
					return number % 4;
				case 72:
					return number % 6;
				case 96:
					return number % 6;
			}
			throw new Exception("Неверное кол-во гнёзд");
		}

	}
}
