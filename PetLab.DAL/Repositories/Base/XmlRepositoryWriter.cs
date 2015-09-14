using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Repositories.Base;

namespace PetLab.DAL.Repositories.Base {
	/// <summary>
	/// репазиторий для записи
	/// </summary>
	public abstract class XmlRepositoryWriter<T> : XmlRepository<T>, IXmlRepository where T : class {

		#region private 

		protected sealed override string FileRequestStringFormat => "ZGPTOTZ*.txt";
		protected sealed override string FileResponseStringFormat => "petlab_cc_export_{0}.xml";
		protected override string PathRequest => ConfigurationManager.AppSettings["idoc_out"];
		protected override string PathResponse => ConfigurationManager.AppSettings["idoc_in"];
		protected override sealed TimeSpan Timeout
			=> new TimeSpan(0, 0, Int32.Parse(ConfigurationManager.AppSettings["timeout_request"]));

		#endregion private

		public XmlRepositoryWriter(IPetLabXmlContext context) : base(context) {
		}

		#region private methods

		/// <summary>
		/// создать файл ответа
		/// </summary>
		protected void CreateResponse(T entry) {
			var queryString = GenerateQuerySubstring();
			var fullPath = Path.Combine(PathResponse, String.Format(FileResponseStringFormat, queryString));
			var serializer = new XmlSerializer(typeof(T));
			using (var writer = new StreamWriter(fullPath))
			using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true })) {
				serializer.Serialize(xmlWriter, entry);
			}
		}

		/// <summary>
		/// найти файл для инициализации экспорта
		/// примеры:
		/// ZGPTOTZ20150824203901.txt
		/// </summary>
		private Dictionary<string, string> FindAllRequest() {
			var result = new Dictionary<string, string>();
			var files = Directory.GetFiles(PathRequest, FileRequestStringFormat);
			if (files.Length > 0) {
				foreach (var file in files) {
					result.Add(file, File.ReadAllText(file));
				}
			}
			return result;
		}

		#endregion private methods

		#region protected methods

		/// <summary>
		/// проверяет содержимое файла
		/// </summary>
		protected abstract bool FileCheckContent(string content, T entry);

		#endregion protected methods

		#region public

		//public void Insert(T entry) {
		//	Context.Insert(entry, this);
		//}

		/// <summary>
		/// сохраняет в xml все добавленные entry
		/// </summary>
		public virtual void Export(T entry) {
			var dt = DateTime.Now;
			if (DateTime.Now - dt > Timeout) {
				throw new TimeoutException();
			}
			var keyValues = FindAllRequest();
			foreach (var keyValue in keyValues) {
				if (FileCheckContent(keyValue.Value, entry)) {
					CreateResponse(entry);
					break;
				}
			}
			keyValues.ToList().ForEach(kv => DeleteFile(kv.Key));
		}

		public Task ExportAsync(T entry) {
			return Task.Factory.StartNew(() => {
				Export(entry);
			});
		}

		#endregion public

	}
}
