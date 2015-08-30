using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class OrderEtalonWeightConverter: TypeConverter<orderXml, order_etalon_weight> {
		protected override order_etalon_weight ConvertCore(orderXml source) {
			if (source.etalons?.weight == null) {
				return null;
			}
			var result = new order_etalon_weight() {
				order_id = source.order_id,
				lim1 = source.etalons.weight.lim1,
				lim2 = source.etalons.weight.lim2,
				lim3 = source.etalons.weight.lim3,
				lim4 = source.etalons.weight.lim4,
				lim5 = source.etalons.weight.lim5,
				name = source.etalons.weight.name.ToString()
			};
			return result;
		}
	}
}
