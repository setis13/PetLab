using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.ModelToDto {
	public class DefectConverter : TypeConverter<defect, DefectDto> {
		protected override DefectDto ConvertCore(defect source) {
			var result = new DefectDto();
			result.DefectId = source.defect_id;
			result.Name = source.name;
			return result;
		}
	}
}
