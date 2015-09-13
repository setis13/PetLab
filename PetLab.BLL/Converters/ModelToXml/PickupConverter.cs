using System;
using System.Linq;
using AutoMapper;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToXml {
	public class PickupConverter : TypeConverter<pickup, pickupXml> {
		protected override pickupXml ConvertCore(pickup source) {
			var result = new pickupXml();
			result.date_take = source.datetime_take;
			result.date_begin = source.datetime_create;
			if (source.datetime_close != null) {
				result.date_end = source.datetime_close.Value;
			} else {
				throw new Exception("Нет даты закрытия съёма");
			}
			result.equipment = source.order.equipment_id;
			result.etalon_match = source.etalon_match;
			result.station_cooling = source.pickup_station_cooling.name;
			//color
			var colors = source.pickup_etalon_color_ranges.ToList();
			if (colors.Count > 0) {
				result.color = new pickupColor();
                result.color.range = new pickupColorRange[colors.Count];
				for (int i = 0; i < colors.Count; i++) {
					result.color.range[i] = new pickupColorRange();
					result.color.range[i].value = colors[i].value;
					result.color.range[i].name = colors[i].range_name;
				}
			}
			//defects
			var defects = source.pickup_defects.ToList();
			if (defects.Count > 0) {
				result.defect = new pickupDefect_meas[defects.Count];
				for (int i = 0; i < defects.Count; i++) {
					result.defect[i] = new pickupDefect_meas();
					result.defect[i].socket = defects[i].socket;
					result.defect[i].defect_id = defects[i].defect_id;
					result.defect[i].grade = defects[i].grade;
				}
			}

			return result;
		}
	}
}
