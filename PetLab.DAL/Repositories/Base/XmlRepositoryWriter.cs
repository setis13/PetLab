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
using PetLab.DAL.Models.xml;
using PetLab.DAL.Contracts.Models.Base;

namespace PetLab.DAL.Repositories.Base {
	/// <summary>
	/// репазиторий для записи
	/// </summary>
	public abstract class XmlRepositoryWriter<T> : XmlRepository<T>, IXmlRepository where T : class {

		#region private 
		//
		private Task ExportTask;
		private DateTime ExportStart;
		private object _lock = new object();

		protected sealed override string FileRequestStringFormat => "ZGPTOTZ*.txt";
		protected sealed override string FileResponseStringFormat => "petlab_cc_export_{0}.xml";
		protected override string PathRequest => ConfigurationManager.AppSettings["idoc_out"];
		protected override string PathResponse => ConfigurationManager.AppSettings["idoc_in"];
		protected override sealed TimeSpan Timeout
			=> new TimeSpan(0, 0, Int32.Parse(ConfigurationManager.AppSettings["timeout_request"]));

		#endregion private

		public XmlRepositoryWriter(IPetLabXmlContext context) : base(context) {
		}

		#region public

		/// <summary>
		/// сохраняет в xml все добавленные entry
		/// </summary>
		public virtual void Export(T entry) {
			var queryString = GenerateQuerySubstring();
			var fullPath = Path.Combine(PathResponse, String.Format(FileResponseStringFormat, queryString));
			var serializer = new XmlSerializer(typeof(T));
			using (var writer = new StreamWriter(fullPath))
			using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true, })) {
				serializer.Serialize(xmlWriter, entry);
			}
		}

		public Task ExportAsync(T entry) {
			return Task.Factory.StartNew(() => {
				Export(entry);
			});
		}

		#endregion public

	}
}
