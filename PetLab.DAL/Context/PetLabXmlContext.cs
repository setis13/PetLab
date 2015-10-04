using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using PetLab.DAL.Contracts.Context;

namespace PetLab.DAL.Context {
	public class PetLabXmlContext : IPetLabXmlContext {
		public List<object> ExportSet { get; set; } = new List<object>();

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
