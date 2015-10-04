using PetLab.DAL.Contracts.Models.Base;
using System.Collections.Generic;

namespace PetLab.DAL.Contracts.Context {
	public interface IPetLabXmlContext {
		List<object> ExportSet { get; set; }
		string Serialize<T>(T entry);
		T Deserialize<T>(string content);
	}
}