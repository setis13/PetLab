using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.ServiceLocation;
using PetLab.DAL.Models;
using PetLab.WPF.Dialogs;
using PetLab.WPF.Helpers;
using PetLab.WPF.ViewModels.Base;

namespace PetLab.WPF {
	public partial class MainWindow : Window {

		//public static IMultiValueConverter CountSocketsToButtonsConverter { get { return new MultiConverterHelper(CountSocketsToButtonsConvert); } }
		//public static IMultiValueConverter GradeAndCurrentDefectToBrushConverter { get { return new MultiConverterHelper(GradeAndCurrentDefectToBrushConvert); } }
		//public static IValueConverter CountSockToBlockSockConverter { get { return new ConverterHelper(CountSockToBlockSockConvert); } }
		//public static IValueConverter OrderToColorEtalonConverter { get { return new ConverterHelper(OrderToColorEtalonConvert); } }
		//public static IValueConverter PickupEtalonCoBackgroundConverter { get { return new ConverterHelper(PickupEtalonCoBackgroundConvert); } }

		public MainWindow() {
			InitializeComponent();
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
		}

		private void MainWindow_OnClosed(object sender, EventArgs e) {
		}

		private void SettingExportImportClick(object sender, RoutedEventArgs e) {
		}

		private void ImportDefectsClick(object sender, RoutedEventArgs e) {
			var dialog = ServiceLocator.Current.GetInstance<DefectsDialog>();
			if (dialog.ShowDialog() == true) {
				
			}
		}

		private void ImportMaterialsClick(object sender, RoutedEventArgs e) {

		}
	}
}
