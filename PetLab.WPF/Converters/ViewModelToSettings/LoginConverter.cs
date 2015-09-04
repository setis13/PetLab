using AutoMapper;
using PetLab.BLL.Common.Settings;
using PetLab.WPF.ViewModels;

namespace PetLab.WPF.Converters.ViewModelToSettings {
	public class LoginConverter : TypeConverter<LoginViewModel, LoginSettings> {
		protected override LoginSettings ConvertCore(LoginViewModel source) {
			var result = new LoginSettings();
			result.ShiftNumber = source.SelectedShiftNumber;
			result.UserId = source.SelectedUserId;
			return result;
		}
	}
}
