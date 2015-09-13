using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories.Base;

namespace PetLab.DAL.Repositories {
	/// <summary>
	/// репозиторий для запроса дефектов
	/// </summary>
	public class XmlDefectsRepository : XmlRepositoryReader<defectsXml> {
		public XmlDefectsRepository(IPetLabXmlContext context) : base(context) {
		}

		protected override string GenerateQuerySubstring() {
			return "defects";
		}

		protected override string CreateContentFile(object value) {
			return "";
		}
	}
}
