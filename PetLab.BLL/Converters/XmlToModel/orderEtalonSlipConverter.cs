using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class OrderEtalonSlipConverter: TypeConverter<orderXml, order_etalon_slip>{
		protected override order_etalon_slip ConvertCore(orderXml source) {
			if (source.etalons?.slip == null) {
				return null;
			}
			var result = new order_etalon_slip() {
				order_id = source.order_id,
				lim1 = source.etalons.slip.lim1,
				lim2 = source.etalons.slip.lim2,
				lim3 = source.etalons.slip.lim3,
				lim4 = source.etalons.slip.lim4,
				lim5 = source.etalons.slip.lim5,
				name = source.etalons.slip.name
			};
			return result;
		}
	}
}
