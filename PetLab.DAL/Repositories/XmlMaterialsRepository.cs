using PetLab.DAL.Contracts;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories.Base;

namespace PetLab.DAL.Repositories {
	/// <summary>
	/// 
	/// </summary>
	public class XmlMaterialsRepository : XmlRepositoryReader<defectsXml> {
		public XmlMaterialsRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
		}

		public override void SaveChanges() { }

		protected override string GenerateQuerySubstring() {
			return "materials";
		}

		protected override string CreateContentFile(object value) {
			return "";
		}
	}
}
