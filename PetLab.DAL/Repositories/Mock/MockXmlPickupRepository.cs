using System;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models.xml;

namespace PetLab.DAL.Repositories.Mock {
	/// <summary>
	/// фейковый репозиторий для запроса дефектов
	/// </summary>
	public class MockXmlPickupRepository : XmlPickupRepository {

		public MockXmlPickupRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
		}

	}
}
