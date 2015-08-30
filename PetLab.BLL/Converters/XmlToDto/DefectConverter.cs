using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToDto {
	public class DefectConverter : TypeConverter<defectsDefect, DefectXmlDto> {
		protected override DefectXmlDto ConvertCore(defectsDefect source) {
			var result = new DefectXmlDto();
			result.DefectId = source.id;
			result.Name = source.text;
			return result;
		}
	}
}
