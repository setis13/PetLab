using PetLab.DAL.Contracts;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories.Base;

namespace PetLab.DAL.Repositories {
	/// <summary>
	/// репозиторий для запроса дефектов
	/// </summary>
	public class XmlDefectsRepository : XmlRepositoryReader<defectsXml> {
		public XmlDefectsRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
		}

		public override void SaveChanges() {}

		protected override string GenerateQuerySubstring() {
			return "defects";
		}

		protected override string CreateContentFile(object value) {
			return "";
		}
	}
}
