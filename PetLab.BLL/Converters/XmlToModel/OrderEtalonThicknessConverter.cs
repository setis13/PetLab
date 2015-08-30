using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class OrderEtalonThicknessConverter:TypeConverter<orderXml, order_etalon_thickness> {
		protected override order_etalon_thickness ConvertCore(orderXml source) {
			if (source.etalons?.thickness == null) {
				return null;
			}
			var result = new order_etalon_thickness() {
				order_id = source.order_id,
				lim3 = source.etalons.thickness.lim3,
				lim4 = source.etalons.thickness.lim4,
				lim5 = source.etalons.thickness.lim5,
				name = source.etalons.thickness.name
			};
			return result;
		}
	}
}
