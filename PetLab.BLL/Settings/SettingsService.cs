using PetLab.BLL.Common.Settings;
using PetLab.BLL.Contracts.Settings;

namespace PetLab.BLL.Settings {
	public class SettingsService : ISettingsService {
		public void SetLoginSettings(LoginSettings settings) {
				Properties.Settings.Default.LoginSettings = settings;
			Properties.Settings.Default.Save();
		}

		public LoginSettings GetLoginSettings() {
			if (Properties.Settings.Default.LoginSettings != null) {
				return Properties.Settings.Default.LoginSettings;
			} else {
				return new LoginSettings();
			}
		}
	}
}
