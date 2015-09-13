namespace PetLab.DAL.Contracts.Context {
	public interface IPetLabXmlContext {
		string Serialize<T>(T entry);
		T Deserialize<T>(string content);
	}
}