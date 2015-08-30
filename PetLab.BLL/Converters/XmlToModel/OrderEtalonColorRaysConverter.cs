using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PetLab.BLL.Common.Dto;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;

namespace PetLab.BLL.Converters.XmlToModel {
	public class OrderEtalonColorRaysConverter : TypeConverter<orderXml, IEnumerable<order_etalon_color_ray>> {
		protected override IEnumerable<order_etalon_color_ray> ConvertCore(orderXml source) {
			var listRays = new List<order_etalon_color_ray>();
			if (source.etalons?.color?.pickup_rays == null) {
				return listRays;
			}
			foreach (var orderEtalonsColorRay in source.etalons.color.pickup_rays) {
				listRays.Add(
					new order_etalon_color_ray() {
						order_id = source.order_id,
						ray_id = orderEtalonsColorRay.id,
						value = orderEtalonsColorRay.value == 1
					});
			}
			return listRays;
		}
	}
}
