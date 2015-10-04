using System;
using System.Linq;
using AutoMapper;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToXml {
	public class PickupConverter : TypeConverter<pickup, pickupXml> {
		protected override pickupXml ConvertCore(pickup source) {
			var result = new pickupXml();
			result.date_take = source.datetime_take.ToString("yyyy-MM-dd HH:mm:ss");
			result.date_begin = source.datetime_create.ToString("yyyy-MM-dd HH:mm:ss");
			if (source.datetime_close != null) {
				result.date_end = source.datetime_close.Value.ToString("yyyy-MM-dd HH:mm:ss");
			} else {
				throw new Exception("Нет даты закрытия съёма");
			}
			result.equipment = source.order.equipment_id;
			result.etalon_match = source.etalon_match;
			result.station_cooling = source.pickup_station_cooling.name;
			result.number = source.number;
			//color
			if (source.pickup_etalon_color_ranges != null && source.pickup_etalon_color_ranges.Count > 0) {
				var colors = source.pickup_etalon_color_ranges.ToList();
				result.color = new pickupColor();
				result.color.range = new pickupColorRange[colors.Count];
				for (int i = 0; i < colors.Count; i++) {
					result.color.range[i] = new pickupColorRange();
					result.color.range[i].value = colors[i].value;
					result.color.range[i].name = colors[i].range_name;
				}
			}
			//defects
			if (source.pickup_defects != null && source.pickup_defects.Count > 0) {
				var defects = source.pickup_defects.ToList();
				result.defect = new pickupDefect_meas[defects.Count];
				for (int i = 0; i < defects.Count; i++) {
					result.defect[i] = new pickupDefect_meas();
					result.defect[i].socket = (byte)(defects[i].socket + 1);
					result.defect[i].defect_id = defects[i].defect_id;
					result.defect[i].grade = defects[i].grade;
				}
			}

			return result;
		}
	}
}
