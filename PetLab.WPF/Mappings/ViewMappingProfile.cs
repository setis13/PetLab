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
			//dto <-> ViewModel
			CreateMap<UserDto, UserViewModel>().ReverseMap();
			CreateMap<DefectDto, DefectViewModel>().ReverseMap();
			CreateMap<MaterialDto, MaterialViewModel>().ReverseMap();
			CreateMap<EquipmentDto, EquipmentViewModel>().ReverseMap();
			CreateMap<OrderDto, OrderViewModel>().ReverseMap();
			CreateMap<ShiftDto, ShiftViewModel>().ReverseMap();
			CreateMap<CoolingStationDto, CoolingStationViewModel>().ReverseMap();
			CreateMap<PickupEtalonColorRangeDto, PickupEtalonColorRangeViewModel>().ReverseMap();
			CreateMap<OrderEtalonColorDto, OrderEtalonColorViewModel>().ReverseMap();
			CreateMap<OrderEtalonColorRangeDto, OrderEtalonColorRangeViewModel>().ReverseMap();
			CreateMap<OrderEtalonColorRayDto, OrderEtalonColorRayViewModel>().ReverseMap();
			CreateMap<PickupDto, PickupViewModel>().ConvertUsing<Converters.DtoToViewModel.PickupConverter>();

			//ViewModel to Settings
			CreateMap<LoginViewModel, LoginSettings>().ConvertUsing<LoginConverter>();
		}
	}
}
