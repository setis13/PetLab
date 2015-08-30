using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToDto {
	public class MaterialConverter : TypeConverter<materialsmaterial, MaterialXmlDto> {
		protected override MaterialXmlDto ConvertCore(materialsmaterial source) {
			var result = new MaterialXmlDto();
			result.MaterialId = source.id;
			result.Name = source.text;
			return result;
		}
	}
}
