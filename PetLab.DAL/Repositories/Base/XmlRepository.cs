using System;
using System.Configuration;
using System.IO;
using PetLab.DAL.Context;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Repositories.Base;

namespace PetLab.DAL.Repositories.Base {
	/// <summary>
	/// базовй xml репазиторий
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class XmlRepository<T> : IXmlRepository where T : class {

		/// <summary>
		/// Creates custom repository
		/// </summary>
		/// <param name="context"></param>
		public XmlRepository(IPetLabXmlContext context) {
			Context = context;
		}

		#region protectedfields

		protected IPetLabXmlContext Context;
		/// <summary>
		/// шаблон файла запроса
		/// примеры:
		/// petlab_cc_import_{0}.xml
		/// ZGPTOTZ20150824203901.txt
		/// </summary>
		protected abstract string FileRequestStringFormat { get; }
		/// <summary>
		/// шаблон файла ответа
		/// </summary>
		protected abstract string FileResponseStringFormat { get; }
		/// <summary>
		/// путь для запросов (запрос от репазитория и наоборот)
		/// </summary>
		protected abstract string PathRequest { get; }
		/// <summary>
		/// путь для ответов (ответов от репазитория и наоборот)
		/// </summary>
		protected abstract string PathResponse { get; }
		/// <summary>
		/// путь к файлам с ошибками
		/// </summary>
		protected string PathError => ConfigurationManager.AppSettings["idoc_err"];
		/// <summary>
		/// таймаут, запроса/ответа
		/// </summary>
		protected abstract TimeSpan Timeout { get; }

		#endregion protectedfields

		#region [ abstract methods ]

		/// <summary>
		/// генерировать substring для айла запроса
		/// </summary>
		/// <returns>defects, materials, 201508191050</returns>
		protected abstract string GenerateQuerySubstring();

		#endregion [ abstract methods ]

		#region protected methods

		/// <summary>
		/// переместить файл запроса в папку ошибок
		/// </summary>
		protected void MoveToErrorFile(string fullPath) {
			try {
				File.Move(fullPath, Path.Combine(PathError, Path.GetFileName(fullPath)));
			} catch (Exception) {
				// ignored
			}
		}

		/// <summary>
		/// удалить файл запроса
		/// </summary>
		protected void DeleteFile(string fullPath) {
			try {
				File.Delete(fullPath);
			} catch (Exception) {
				// ignored
			}
		}

		#endregion protected methods
	}
}
