using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	class UserConverter : TypeConverter<user, UserDto>{
		protected override UserDto ConvertCore(user source) {
			var result = new UserDto();
			result.Fio = source.fio;
			result.UserId = source.user_id;
			return result;
		}
	}
}
