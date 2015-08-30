using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class DefectConverter : TypeConverter<DefectXmlDto, defect> {
		protected override defect ConvertCore(DefectXmlDto source) {
			var result = new defect();
			result.defect_id = source.DefectId;
			result.name = source.Name;
			return result;
		}
	}
}
