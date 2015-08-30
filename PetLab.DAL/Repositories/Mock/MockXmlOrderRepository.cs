using System;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models.xml;

namespace PetLab.DAL.Repositories.Mock {
	/// <summary>
	/// фейковый репозиторий для запроса дефектов
	/// </summary>
	public class MockXmlOrderRepository : XmlOrderRepository {

		public MockXmlOrderRepository(IPetLabXmlContext context) : base(context) {
		}

		/// <summary>
		/// возвращает дефекты. последний дефект имеет случайное имя
		/// </summary>
		/// <param name="value">string, Id машины</param>
		public override orderXml Get(object value = null) {
			var random = new Random();
			var order = new orderXml();
			order.batch_id = "000" + random.Next(7);
			order.color_shade = "оттенок";
			switch (random.Next(3)) {
				case 0:
					order.count_sockets = 48;
					break;
				case 1:
					order.count_sockets = 72;
					break;
				case 2:
					order.count_sockets = 96;
					break;
			}
			order.dye_name = "краситель";
			order.equipment = (string)value;
			order.material_id = "000000" + (random.Next(4) + 1);
			order.order_id = "000000" + (random.Next(20) + 1).ToString("D2");
			//созданная смена, 1 - MaxInt
			//номер смены мне не сильно нужен
			order.shift = 1;
			order.etalons = new orderEtalons();
			order.etalons.color = new orderEtalonsColor();
			switch (random.Next(4)) {
				case 0:
					order.etalons.color.name = "Б0-1";
					order.etalons.color.pickup_mode = 1;
					order.etalons.color.socket_number = (byte)(random.Next(2) == 0 ? 1 : 2);
					order.etalons.color.pickup_rays = new[] {
						new orderEtalonsColorRay() { id = "ray_u", value = (byte) random.Next(2) },
						new orderEtalonsColorRay() { id = "ray_w", value = (byte) random.Next(2) },
						new orderEtalonsColorRay() { id = "ray_b", value = (byte) random.Next(2) },
						new orderEtalonsColorRay() { id = "ray_g", value = (byte) random.Next(2) }
					};
					order.etalons.color.range = new[] {
						new orderEtalonsColorRange() {
							name = "Допуск ВД",
							lim1 = 0 + random.Next(10)/10,
							lim2 = 1 + random.Next(10)/10,
							lim3 = 2 + random.Next(10)/10,
							lim4 = 3 + random.Next(10)/10,
							lim5 = 4 + random.Next(10)/10
						},
						new orderEtalonsColorRange() {
							name = "Допуск ИК",
							lim1 = 3 + random.Next(5)/10,
							lim2 = 4 + random.Next(5)/10,
							lim3 = 5 + random.Next(5)/10,
							lim4 = 6 + random.Next(5)/10,
							lim5 = 7 + random.Next(5)/10
						}
					};
					break;
				case 1:
					order.etalons.color.name = "К1-0";
					order.etalons.color.pickup_mode = 1;
					order.etalons.color.socket_number = (byte)(random.Next(2) == 0 ? 1 : 2);
					order.etalons.color.pickup_rays = new[] {
						new orderEtalonsColorRay() { id = "ray_i8", value = (byte) random.Next(2) },
						new orderEtalonsColorRay() { id = "ray_i9", value = (byte) random.Next(2) }
					};
					order.etalons.color.range = new[] {
						new orderEtalonsColorRange() {
							name = "Допуск ВД",
							lim1 = 10 + random.Next(10),
							lim2 = 20 + random.Next(10),
							lim3 = 30 + random.Next(10),
							lim4 = 40 + random.Next(10),
							lim5 = 50 + random.Next(10)
						},
						new orderEtalonsColorRange() {
							name = "Допуск ИК",
							lim1 = 20 + random.Next(5),
							lim2 = 30 + random.Next(5),
							lim3 = 40 + random.Next(5),
							lim4 = 50 + random.Next(5),
							lim5 = 60 + random.Next(5)
						}
					};
					break;
			}
			if (random.Next(2) == 0) {
				order.etalons.slip = new orderEtalonsSlip { name = "A", lim1 = 1, lim2 = 2, lim3 = 3, lim4 = 4, lim5 = 5 };
			}
			order.etalons.thickness = new orderEtalonsThickness { name = "A", lim3 = 30 + random.Next(2), lim4 = 40 + random.Next(2), lim5 = 50 + random.Next(2) };
			order.etalons.weight = new orderEtalonsWeight { name = 39,
				lim1 = (decimal)(37.5 + random.Next(10) / 10),
				lim2 = (decimal)(37.7 + random.Next(10) / 10),
				lim3 = 38 + random.Next(10)/10,
				lim4 = (decimal) (38.4 + random.Next(10) / 10),
				lim5 = (decimal)(39 + random.Next(10) / 10) };

			return order;
		}
	}
}
