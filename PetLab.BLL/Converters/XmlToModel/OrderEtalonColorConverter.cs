using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class OrderEtalonColorConverter : TypeConverter<orderXml, order_etalon_color> {
		protected override order_etalon_color ConvertCore(orderXml source) {
			if (source.etalons?.color?.name == null) {
				return null;
			}
			var result = new order_etalon_color() {
				order_id = source.order_id,
				name = source.etalons.color.name,
				socket_number = source.etalons.color.socket_number,
				pickup_mode = source.etalons.color.pickup_mode,
			};
			return result;
		}
	}
}
