using System.Collections.Generic;

namespace PetLab.DAL.Contracts.Context {
	public interface IPetLabXmlContext {
		List<object> ExportSet { get; set; }
		void Serialize<T>(string fullPath, T entry);
		T Deserialize<T>(string content);
	}
}