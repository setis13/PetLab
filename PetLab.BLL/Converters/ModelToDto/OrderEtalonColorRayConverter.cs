using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	public class OrderEtalonColorRayConverter : TypeConverter<order_etalon_color_ray, OrderEtalonColorRayDto> {
		protected override OrderEtalonColorRayDto ConvertCore(order_etalon_color_ray source) {
			var result = new OrderEtalonColorRayDto();
			result.RayId = source.ray_id;
			result.Value = source.value;
			return result;
		}
	}
}
