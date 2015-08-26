using System.Collections.Generic;
using PetLab.DAL.Contracts.Repositories.Base;

namespace PetLab.DAL.Contracts.Context {
	public interface IPetLabXmlContext {
		List<T> GetEntries<T>();
		void Insert(object entry, IXmlRepository repository);
		string Serialize<T>(T entry);
		T Deserialize<T>(string content);
		void Remove(object entry);
		void SaveChanges();
		void RollBack();
	}
}