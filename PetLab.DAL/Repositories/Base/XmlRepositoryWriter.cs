using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PetLab.DAL.Contracts;
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

		public XmlRepositoryWriter(IUnitOfWork unitOfWork) : base(unitOfWork) {
		}

		#region private methods

		/// <summary>
		/// создать файл ответа
		/// </summary>
		private void CreateResponse(T entry) {
			var queryString = GenerateQuerySubstring();
			var fullPath = Path.Combine(PathResponse, String.Format(FileResponseStringFormat, queryString));
			File.WriteAllText(fullPath, Context.Serialize<T>((T)entry));
		}

		/// <summary>
		/// найти файл для инициализации экспорта
		/// примеры:
		/// ZGPTOTZ20150824203901.txt
		/// </summary>
		private Dictionary<string, string> FindAllRequest() {
			var result = new Dictionary<string, string>();
			var files = Directory.GetFiles(Path.Combine(PathRequest, FileRequestStringFormat));
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

		public void Insert(object entry) {
			Context.Insert(entry, this);
		}

		/// <summary>
		/// сохраняет в xml все добавленные entry
		/// </summary>
		public override void SaveChanges() {
			var dt = DateTime.Now;
			var exportList = Context.GetEntries<T>();
			while (exportList.Count > 0) {
				if (DateTime.Now - dt > Timeout) {
					throw new TimeoutException();
				}
				var keyValues = FindAllRequest();
				for (int i = exportList.Count - 1; i >= 0; i--) {
					T entry = exportList[i];
					foreach (var keyValue in keyValues) {
						if (FileCheckContent(keyValue.Value, entry)) {
							CreateResponse(entry);
							exportList.Remove(entry);
							break;
						}
					}
				}
				keyValues.ToList().ForEach(kv => DeleteFile(kv.Key));
			}
		}

		public Task SaveShangesAsync() {
			return Task.Factory.StartNew(() => {
				SaveChanges();
			});
		}

		#endregion public

	}
}
