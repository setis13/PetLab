using System;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories.Base;

namespace PetLab.DAL.Repositories {
	/// <summary>
	/// репозиторий для запроса заказов
	/// </summary>
	public class XmlOrderRepository : XmlRepositoryReader<orderXml> {
		public XmlOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
		}

		public override void SaveChanges() { }

		protected override string GenerateQuerySubstring() {
			return DateTime.Now.ToString("yyyyMMddHHmmss");
		}

		protected override string CreateContentFile(object value) {
			return $"{DateTime.Now:dd.MM.yyyy HH.mm} {value}";
		}
	}
}
