using System.Collections.Generic;
using AutoMapper;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class OrderEtalonColorRangesConverter : TypeConverter<orderXml, IEnumerable<order_etalon_color_range>> {
		protected override IEnumerable<order_etalon_color_range> ConvertCore(orderXml source) {
			var listRanges = new List<order_etalon_color_range>();
			if (source.etalons?.color?.range == null) {
				return listRanges;
			}
			foreach (var orderEtalonsColorRange in source.etalons.color.range) {
				listRanges.Add(
					new order_etalon_color_range() {
						order_id = source.order_id,
						lim1 = orderEtalonsColorRange.lim1,
						lim2 = orderEtalonsColorRange.lim2,
						lim3 = orderEtalonsColorRange.lim3,
						lim4 = orderEtalonsColorRange.lim4,
						lim5 = orderEtalonsColorRange.lim5,
						name = orderEtalonsColorRange.name
					});
			}
			return listRanges;
		}
	}
}
