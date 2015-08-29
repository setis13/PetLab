using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	public class ShiftConverter : TypeConverter<shift, ShiftDto> {
		protected override ShiftDto ConvertCore(shift source) {
			var result = new ShiftDto();
			result.Datetime = source.datetime;
			result.ShiftId = source.shift_id;
			result.ShiftNumber = source.shift_number;
			result.TimeId = source.time_id;
			result.UserId = source.user_id;
			return result;
		}
	}
}
