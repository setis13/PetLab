using System;
using System.IO;
using System.Linq;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models.xml;

namespace PetLab.DAL.Repositories.Mock {
	/// <summary>
	/// фейковый репозиторий для запроса дефектов
	/// </summary>
	public class MockXmlPickupRepository : XmlPickupRepository {
		protected override string PathRequest => Path.Combine(Directory.GetCurrentDirectory(), base.PathRequest);
		protected override string PathResponse => Path.Combine(Directory.GetCurrentDirectory(), base.PathResponse);

		public MockXmlPickupRepository(IPetLabXmlContext context) : base(context) {
		}

		public override void SaveChanges() {

			if (Directory.Exists(PathRequest) == false) {
				Directory.CreateDirectory(PathRequest);
			}
			if (Directory.Exists(PathResponse) == false) {
				Directory.CreateDirectory(PathResponse);
			}

			var exportList = Context.GetEntries<pickupXml>();
			while (exportList.Count > 0) {
				for (int i = exportList.Count - 1; i >= 0; i--) {
					pickupXml entry = exportList[i];
					CreateResponse(entry);
					exportList.Remove(entry);
				}
			}
		}
	}
}
