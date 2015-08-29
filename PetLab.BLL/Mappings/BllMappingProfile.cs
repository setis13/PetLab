using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Converters.ModelToDto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Mappings {
	/// <summary>
	/// профиль мапингов для BLL
	/// </summary>
	public class BllMappingProfile : Profile {
		protected override void Configure() {
			base.Configure();

			CreateMap<shift, ShiftDto>().ConvertUsing<ShiftConverter>();
			CreateMap<user, UserDto>().ConvertUsing<UserConverter>();
		}
	}
}
