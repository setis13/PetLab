using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using PetLab.DAL.Contracts.Context;

namespace PetLab.DAL.Context {
	public class PetLabXmlContext : IPetLabXmlContext {
		public List<object> ExportSet { get; set; } = new List<object>();

		public void Serialize<T>(string fullPath, T entry) {
			XmlSerializer formatter = new XmlSerializer(typeof(T));
			StreamWriter stream = new StreamWriter(fullPath);
			XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true });
			var ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			formatter.Serialize(writer, entry, ns);
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
