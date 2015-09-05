using System;
using System.Threading;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models.xml;

namespace PetLab.DAL.Repositories.Mock {
	/// <summary>
	/// фейковый репозиторий для запроса материалов
	/// </summary>
	public class MockXmlMaterialsRepository : XmlMaterialsRepository {

		public MockXmlMaterialsRepository(IPetLabXmlContext context) : base(context) {
		}

		/// <summary>
		/// возвращает материалы. последний материал имеет случайное имя
		/// </summary>
		public override materialsXml Get(object value) {
			var random = new Random();
			var materialsXml = new materialsXml();
			materialsXml.material = new[] {
				new materialsmaterial() { id = "0000001", text = "материал1"},
				new materialsmaterial() { id = "0000002", text = "материал2"},
				new materialsmaterial() { id = "0000003", text = "материал3"},
				new materialsmaterial() { id = "0000004", text = "материал_" + random.Next(7) + 3},
				new materialsmaterial() { id = "0000005" + random.Next(10), text = "материал_" + random.Next(7) + 3}
			};
			Thread.Sleep(1000);
			return materialsXml;
		}
	}
}
