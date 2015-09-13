using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Repositories.Base;

namespace PetLab.DAL.Context {
	public class PetLabXmlContext : IPetLabXmlContext {
		private Dictionary<object, IXmlRepository> _savingList = new Dictionary<object, IXmlRepository>();

		public string Serialize<T>(T entry) {
			XmlSerializer formatter = new XmlSerializer(typeof(T));
			StringWriter stream = new StringWriter();
			XmlWriter writer = XmlWriter.Create(stream);
			formatter.Serialize(writer, entry);
			var xml = stream.ToString();
			return xml;
		}

		public T Deserialize<T>(string content) {
			XmlSerializer formatter = new XmlSerializer(typeof(T));
			var bytes = Encoding.UTF8.GetBytes(content);
			MemoryStream stream = new MemoryStream(bytes);
			var entry = formatter.Deserialize(stream);
			return (T)entry;
		}
	}
}
