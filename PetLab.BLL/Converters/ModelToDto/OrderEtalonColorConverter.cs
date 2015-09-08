using System.Collections.Generic;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	internal class OrderEtalonColorConverter : TypeConverter<order_etalon_color, OrderEtalonColorDto> {
		protected override OrderEtalonColorDto ConvertCore(order_etalon_color source) {
			var result = new OrderEtalonColorDto();
			result.Name = source.name;
			result.PickupMode = source.pickup_mode;
			result.SocketNumber = source.socket_number;
			result.Ranges = Mapper.Map<IEnumerable<OrderEtalonColorRangeDto>>(source.order_etalon_color_ranges);
			result.Rays = Mapper.Map<IEnumerable<OrderEtalonColorRayDto>>(source.order_etalon_color_rays);
			return result;
		}
	}
}
