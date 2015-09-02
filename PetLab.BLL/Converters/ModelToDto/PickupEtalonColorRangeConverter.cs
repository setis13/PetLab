using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	class PickupEtalonColorRangeConverter : TypeConverter<pickup_etalon_color_range, PickupEtalonColorRangeDto> {
		protected override PickupEtalonColorRangeDto ConvertCore(pickup_etalon_color_range source) {
			var result = new PickupEtalonColorRangeDto();
			result.Value = source.value;
			result.PickupId = source.pickup_id;
			result.OrderId = source.order_id;
			result.RangeName = source.range_name;
			return result;
		}
	}
}
