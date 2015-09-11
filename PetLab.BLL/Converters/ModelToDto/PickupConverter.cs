using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	public class PickupConverter : TypeConverter<pickup, PickupDto> {
		protected override PickupDto ConvertCore(pickup source) {
			var result = new PickupDto();
			result.BoxId = source.box_id;
			result.DatetimeClose = source.datetime_close;
			result.DatetimeCreate = source.datetime_create;
			result.DatetimeTake = source.datetime_take;
			result.EtalonMatch = source.etalon_match;
			result.VisualMatch = source.visual_match;
			result.Export = source.export;
			result.Number = source.number;
			result.OrderId = source.order_id;
			result.PickupId = source.pickup_id;
			result.ShiftId = source.shift_id;
			result.StatioName = source.pickup_station_cooling.name;
			result.PickupEtalonColorRanges = Mapper.Map<IEnumerable<PickupEtalonColorRangeDto>>(source.pickup_etalon_color_ranges);
			result.PickupDefects = Mapper.Map<IEnumerable<PickupDefectDto>>(source.pickup_defects);
			result.CountSockets = source.order.count_socket;

			return result;
		}
	}
}
