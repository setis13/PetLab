using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories.Base;

namespace PetLab.DAL.Repositories {
	/// <summary>
	/// 
	/// </summary>
	public class XmlMaterialsRepository : XmlRepositoryReader<materialsXml> {
		public XmlMaterialsRepository(IPetLabXmlContext context) : base(context) {
		}

		protected override string GenerateQuerySubstring() {
			return "materials";
		}

		protected override string CreateContentFile(object value) {
			return "";
		}
	}
}
