using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;

namespace PetLab.DAL.Repositories.Base {
	/// <summary>
	/// читает xml и преобразует в entry 
	/// </summary>
	/// <typeparam name="T">entry</typeparam>
	public abstract class XmlRepositoryReader<T> : XmlRepository<T> where T : class {
		#region private fields

		protected sealed override string FileRequestStringFormat => "petlab_cc_import_{0}.txt";
		protected sealed override string FileResponseStringFormat => "petlab_cc_import_{0}.xml";
		protected sealed override string PathRequest => ConfigurationManager.AppSettings["idoc_in"];
		protected sealed override string PathResponse => ConfigurationManager.AppSettings["idoc_out"];
		protected override sealed TimeSpan Timeout
			=> new TimeSpan(0, 0, Int32.Parse(ConfigurationManager.AppSettings["timeout_response"]));

		#endregion private fields

		protected XmlRepositoryReader(IPetLabXmlContext context) : base(context) {
		}

		#region private methods

		/// <summary>
		/// создать содержимое файла по параметру запроса Get
		/// </summary>
		protected abstract string CreateContentFile(object value);

		/// <summary>
		/// создать файл запроса
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fullPath"></param>
		/// <returns>substring</returns>
		private string CreateRequest(object value, out string fullPath) {
			var queryString = GenerateQuerySubstring();
			fullPath = Path.Combine(PathRequest, String.Format(FileRequestStringFormat, queryString));
			File.WriteAllText(fullPath, CreateContentFile(value));
			return queryString;
		}

		/// <summary>
		/// найти файл результата запроса
		/// примеры:
		/// petlab_cc_import_defects.xml
		/// petlab_cc_import_materials.xml
		/// petlab_cc_import_201508191050.xml
		/// </summary>
		private string FindResponse(string substring) {
			var fullPath = Path.Combine(PathResponse, String.Format(FileResponseStringFormat, substring));
			if (Directory.Exists(fullPath)) {
				return File.ReadAllText(fullPath);
			}
			return null;
		}

		#endregion private methods

		#region public methods

		/// <summary>
		/// синхронный запрос
		/// </summary>
		public virtual T Get(object value = null) {
			string fullPath;
			var substring = CreateRequest(value, out fullPath);
			try {
				var dt = DateTime.Now;
				while (DateTime.Now - dt < Timeout) {
					var response = FindResponse(substring);
					if (response != null) {
						DeleteFile(fullPath);
						return (T)Context.Deserialize<T>(response);
					}
				}
				throw new TimeoutException();
			} catch (Exception) {
				MoveToErrorFile(fullPath);
				throw;
			}
		}

		/// <summary>
		/// асинхронный запрос
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public Task<T> GetAsync(object value = null) {
			return Task.Factory.StartNew(() => {
				return Get(value);
			});
		}

		#endregion public methods
	}
}
