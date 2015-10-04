using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Repositories.Base;
using PetLab.DAL.Helper;

namespace PetLab.DAL.Repositories.Base {
	/// <summary>
	/// базовй xml репазиторий
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class XmlRepository<T> : IXmlRepository where T : class {
		/// <summary>
		/// если папка не существует, то возвращаем относительный путь
		/// </summary>
		private string AbsolutePath(string dir) {
			return Directory.Exists(dir) ? dir :
				// ReSharper disable once AssignNullToNotNullAttribute
				Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), dir);
		}
		/// <summary>
		/// обработанные xml
		/// </summary>
		protected string PathXmlLog => Path.Combine(AbsolutePath(ConfigurationManager.AppSettings["xml_log"]), DateTime.Now.ToString("yyyy-MM-dd"));
		/// <summary>
		/// обработанные xml
		/// </summary>
		protected string PathXmlErr => Path.Combine(AbsolutePath(ConfigurationManager.AppSettings["xml_err"]), DateTime.Now.ToString("yyyy-MM-dd"));
		/// <summary>
		/// логин/пароль к сетевой папке
		/// </summary>
		protected string PathUserName => ConfigurationManager.AppSettings["idoc_user_name"];
		/// <summary>
		/// логин/пароль к сетевой папке
		/// </summary>
		protected string PathPassword => ConfigurationManager.AppSettings["idoc_password"];
		/// <summary>
		/// корневая папка idoc
		/// </summary>
		private string PathIDoc => ConfigurationManager.AppSettings["idoc_root"];
		/// <summary>
		/// файл может быть временно занят другим процессом, надо подождать перед повтором операции
		/// </summary>
		private TimeSpan IDocAccessTimeout => TimeSpan.Parse(ConfigurationManager.AppSettings["idoc_access_timeout"]);
		/// <summary>
		/// Creates custom repository
		/// </summary>
		public XmlRepository(IPetLabXmlContext context) {
			AccessFileHelper.ConnectShare(PathIDoc, PathUserName, PathPassword);

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
		/// пытаться прочитать содержимое файла за заданный промежуток времени 
		/// </summary>
		protected string TryReadAllText(string file) {
			var dt = DateTime.Now;
			while (true) {
				try {
					return File.ReadAllText(file);
				} catch (Exception) {
					if (DateTime.Now - dt < IDocAccessTimeout) {
						continue;
					} else {
						throw;
					}
				}
			}
		}

		/// <summary>
		/// переместить файл запроса в папку ошибок
		/// </summary>
		protected void MoveToErrorFile(string fullPath) {
			try {
				if (Directory.Exists(PathXmlErr) == false) {
					Directory.CreateDirectory(PathXmlErr);
				}
				File.Move(fullPath, Path.Combine(PathXmlErr, Path.GetFileName(fullPath)));
			} catch (Exception) {
				// ignored
			}
		}

		/// <summary>
		/// удалить файл запроса
		/// </summary>
		protected void DeleteFile(string fullPath) {
			try {
				if (Directory.Exists(PathXmlLog) == false) {
					Directory.CreateDirectory(PathXmlLog);
				}
                var newFullPath = Path.Combine(PathXmlLog, Path.GetFileName(fullPath));
                if (File.Exists(newFullPath) == false) {
                    File.Move(fullPath, newFullPath);
                } else {
                    File.Delete(fullPath);
                }
			} catch (Exception exception) {
				// ignored
			}
		}

		#endregion protected methods
	}
}
