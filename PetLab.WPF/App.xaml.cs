using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.ServiceLocation;
using PetLab.Common;
using PetLab.WPF.Dialogs;

namespace PetLab.WPF {
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		private void Application_Startup(object sender, StartupEventArgs e) {
			AppBootstrapper.Init();

			// Disable shutdown when the dialog closes
			Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

			// Show login dialog
			var loginDialog = ServiceLocator.Current.GetInstance<LoginDialog>();
			if (loginDialog.ShowDialog() == true) {
				// Show Main window
				var mainWindow = ServiceLocator.Current.GetInstance<MainWindow>();

				// Re-enable normal shutdown mode.
				Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
				Current.MainWindow = mainWindow;
				mainWindow.Show();
			} else {
				Current.Shutdown(0);
			}
		}

		private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
			var exp = e.Exception.GetBaseException();
			Logger.Error(exp.Message, exp);
			MessageBox.Show(exp.Message, "PetLab Error", MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;
		}
	}
}