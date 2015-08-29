using System;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories.Base;

namespace PetLab.DAL.Repositories {
	public class XmlPickupRepository : XmlRepositoryWriter<pickupXml> {
		public XmlPickupRepository(IPetLabXmlContext context) : base(context) {
		}

		protected override string GenerateQuerySubstring() {
			return DateTime.Now.ToString("yyyyMMddHHmmss");
		}

		protected override bool FileCheckContent(string content, pickupXml entry) {
			var parts = content.Split(';');
			byte eqId;
			//проверяем EqId в файле
			return parts.Length >= 1 && byte.TryParse(parts[0], out eqId);
		}
	}
}
