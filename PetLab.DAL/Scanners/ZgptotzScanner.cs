using System;
using PetLab.DAL.Contracts.Scanners;
using PetLab.DAL.Contracts.Scanners.Base;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using PetLab.DAL.Repositories.Base;
using PetLab.DAL.Contracts.Context;
using System.Threading;
using PetLab.DAL.Contracts.Models.Scan;

namespace PetLab.DAL.Scanners {
	/// <summary>
	/// сканер файлов с орасителя
	/// </summary>
	public class ZgptotzScanner : XmlRepository<Zgptotz>, IZgptotzScanner {
		/// <summary>
		/// вызывается, если получен новый файл
		/// </summary>
		public event ScannerReceived Received;
		/// <summary>
		/// флаг, сканер запущен
		/// </summary>
		public bool IsRunning { get; private set; }

		protected sealed override string FileRequestStringFormat => "ZGPTOTZ*.txt";
		protected sealed override string FileResponseStringFormat => "petlab_cc_export_{0}.xml";
		protected override string PathRequest => ConfigurationManager.AppSettings["idoc_out"];
		protected override string PathResponse => ConfigurationManager.AppSettings["idoc_in"];
		protected override sealed TimeSpan Timeout
			=> new TimeSpan(0, 0, Int32.Parse(ConfigurationManager.AppSettings["timeout_request"]));

		public ZgptotzScanner(IPetLabXmlContext context) : base(context) {
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
				foreach (var file in files.OrderBy(f => f)) {
					try {
						result.Add(file, TryReadAllText(file));
					} catch {
					}
				}
			}
			return result;
		}
		/// <summary>
		/// Invoke event
		/// </summary>
		/// <param name="entry">Zgptotz</param>
		private void OnReceived(object entry) {
			if (Received != null) {
				Received.Invoke(entry);
			}
		}
		/// <summary>
		/// запустить сканер
		/// </summary>
		public void Start() {
            IsRunning = true;
            Task.Factory.StartNew(() => ScannerProcess()).ContinueWith((task) => {
				IsRunning = false;
			});
		}
		/// <summary>
		/// остановить сканер
		/// </summary>
		public void Stop() {
			IsRunning = false;
		}
		/// <summary>
		/// процесс сканера
		/// </summary>
		private void ScannerProcess() {
			while (IsRunning) {
				//получили все файлы ZGPTOTZ*.txt
				var keyValues = FindAllRequest();
				foreach (var keyValue in keyValues) {
					try {
						//переместить файл в папку xml_log
						DeleteFile(keyValue.Key);
						//создать модель
						var entry = new Zgptotz(keyValue.Value);
						//вызвать событие
						OnReceived(entry);
					} catch {
					}
					Thread.Sleep(1000);
				}
			}
		}

		protected override string GenerateQuerySubstring() {
			throw new NotImplementedException();
		}
	}
}