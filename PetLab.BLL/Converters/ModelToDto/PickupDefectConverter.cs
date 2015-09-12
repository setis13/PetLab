using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	public class PickupDefectConverter : TypeConverter<pickup_defects, PickupDefectDto> {
		protected override PickupDefectDto ConvertCore(pickup_defects source) {
			var result = new PickupDefectDto();
			result.DefectId = source.defect_id;
			result.Grade = source.grade;
			result.Socket = source.socket;
			result.PickupId = source.pickup_id;
			return result;
		}
	}
}
