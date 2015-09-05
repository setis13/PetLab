using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Settings;
using PetLab.WPF.Converters.ViewModelToSettings;
using PetLab.WPF.Models;
using PetLab.WPF.ViewModels;

namespace PetLab.WPF.Mappings {
	/// <summary>
	/// профиль мапингов для BLL
	/// </summary>
	public class ViewMappingProfile : Profile {
		protected override void Configure() {
			base.Configure();
			//dto to ViewModel
			CreateMap<ShiftDto, ShiftViewModel>().ReverseMap();
			CreateMap<UserDto, UserViewModel>().ReverseMap();
			CreateMap<DefectDto, DefectViewModel>().ReverseMap();
			CreateMap<MaterialDto, MaterialViewModel>().ReverseMap();

			//ViewModel to Settings
			CreateMap<LoginViewModel, LoginSettings>().ConvertUsing<LoginConverter>();
		}
	}
}
