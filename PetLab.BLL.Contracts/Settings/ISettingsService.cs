using PetLab.BLL.Common.Settings;

namespace PetLab.BLL.Contracts.Settings {
	public interface ISettingsService {
		void SetLoginSettings(LoginSettings settings);
		LoginSettings GetLoginSettings();
	}
}
