using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.ModelToDto {
	public class EquipmentConverter : TypeConverter<equipment, EquipmentDto> {
		protected override EquipmentDto ConvertCore(equipment source) {
			var result = new EquipmentDto();
			result.EquipmentId = source.equipment_id;
			result.PickupId = source.pickup_id;
			return result;
		}
	}
}
