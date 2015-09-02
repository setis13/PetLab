using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	public class OrderEtalonColorRangeConverter : TypeConverter<order_etalon_color_range, OrderEtalonColorRangeDto> {
		protected override OrderEtalonColorRangeDto ConvertCore(order_etalon_color_range source) {
			var result = new OrderEtalonColorRangeDto();
			result.OrderId = source.order_id;
			result.Name = source.name;
			result.Lim1 = source.lim1;
			result.Lim2 = source.lim2;
			result.Lim3 = source.lim3;
			result.Lim4 = source.lim4;
			result.Lim5 = source.lim5;
			return result;
		}
	}
}
