using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class OrderConverter : TypeConverter<orderXml, order> {
		protected override order ConvertCore(orderXml source) {
			var result = new order();
			result.batch_id = source.batch_id;
			result.shift_number_number = source.shift;
			result.order_id = source.order_id;
			result.equipment_id = source.equipment;
			result.color_shade = source.color_shade;
			result.count_socket = source.count_sockets;
			result.dye_name = source.dye_name;
			result.material_id = source.material_id;
            return result;
		}
	}
}
