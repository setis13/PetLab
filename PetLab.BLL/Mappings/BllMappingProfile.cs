using System.Collections.Generic;
using System.ComponentModel;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Mappings {
	/// <summary>
	/// профиль мапингов для BLL
	/// </summary>
	public class BllMappingProfile : Profile {
		protected override void Configure() {
			base.Configure();
			//model to dto
			CreateMap<shift, ShiftDto>().ConvertUsing<Converters.ModelToDto.ShiftConverter>();
			CreateMap<user, UserDto>().ConvertUsing<Converters.ModelToDto.UserConverter>();
			CreateMap<material, MaterialDto>().ConvertUsing<Converters.ModelToDto.MaterialConverter>();
			CreateMap<defect, DefectDto>().ConvertUsing<Converters.ModelToDto.DefectConverter>();
			CreateMap<order, OrderDto>().ConvertUsing<Converters.ModelToDto.OrderConverter>();
			//xml model to dto
			CreateMap<materialsmaterial, MaterialXmlDto>().ConvertUsing<Converters.XmlToDto.MaterialConverter>();
			CreateMap<defectsDefect, DefectXmlDto>().ConvertUsing<Converters.XmlToDto.DefectConverter>();
			//xml dto to model
			CreateMap<DefectXmlDto, defect>().ConvertUsing<Converters.XmlToModel.DefectConverter>();
			CreateMap<MaterialXmlDto, material>().ConvertUsing<Converters.XmlToModel.MaterialConverter>();
			CreateMap<orderXml, order>().ConvertUsing<Converters.XmlToModel.OrderConverter>();
			CreateMap<orderXml, order_etalon_color>().ConvertUsing<Converters.XmlToModel.OrderEtalonColorConverter>();
			CreateMap<orderXml, order_etalon_slip>().ConvertUsing<Converters.XmlToModel.OrderEtalonSlipConverter>();
			CreateMap<orderXml, order_etalon_weight>().ConvertUsing<Converters.XmlToModel.OrderEtalonWeightConverter>();
			CreateMap<orderXml, order_etalon_thickness>().ConvertUsing<Converters.XmlToModel.OrderEtalonThicknessConverter>();
			CreateMap<orderXml, IEnumerable<order_etalon_color_range>>().ConvertUsing<Converters.XmlToModel.OrderEtalonColorRangesConverter>();
			CreateMap<orderXml, IEnumerable<order_etalon_color_ray>>().ConvertUsing<Converters.XmlToModel.OrderEtalonColorRaysConverter>();
	        }
	}
}
