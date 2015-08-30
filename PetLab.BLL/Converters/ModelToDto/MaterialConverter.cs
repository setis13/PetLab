using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.ModelToDto {
	public class MaterialConverter : TypeConverter<material, MaterialDto> {
		protected override MaterialDto ConvertCore(material source) {
			var result = new MaterialDto();
			result.MaterialId = source.material_id;
			result.Name = source.name;
			return result;
		}
	}
}
