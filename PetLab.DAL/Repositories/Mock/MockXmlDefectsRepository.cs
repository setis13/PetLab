using System;
using System.Threading;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models.xml;

namespace PetLab.DAL.Repositories.Mock {
	/// <summary>
	/// фейковый репозиторий для запроса дефектов
	/// </summary>
	public class MockXmlDefectsRepository : XmlDefectsRepository {

		public MockXmlDefectsRepository(IPetLabXmlContext context) : base(context) {
		}

		/// <summary>
		/// возвращает дефекты. последний дефект имеет случайное имя
		/// </summary>
		public override defectsXml Get(object value) {
			var random = new Random();
			var defectsXml = new defectsXml();
			defectsXml.defect = new[] {
				new defectsDefect() { id = "001", text = "дефект1"},
				new defectsDefect() { id = "002", text = "дефект2"},
				new defectsDefect() { id = "003" + random.Next(10), text = "дефект_" + random.Next(7) + 3}
			};
			Thread.Sleep(1000);
			return defectsXml;
		}
	}
}
