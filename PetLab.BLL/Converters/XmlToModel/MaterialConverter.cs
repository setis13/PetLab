using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;

namespace PetLab.BLL.Converters.XmlToModel {
	public class MaterialConverter : TypeConverter<MaterialXmlDto, material> {
		protected override material ConvertCore(MaterialXmlDto source) {
			var result = new material();
			result.material_id = source.MaterialId;
			result.name = source.Name;
			return result;
		}
	}
}
