using System;
using System.Threading.Tasks;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories.Base;
using System.Threading;

namespace PetLab.DAL.Repositories {
	public class XmlPickupRepository : XmlRepositoryWriter<pickupXml> {
		public XmlPickupRepository(IPetLabXmlContext context) : base(context) {
		}

		private object _lock = new object();
		private DateTime oldQueryDateTime;

		/// <summary>
		/// возвращает подстроку для файла-запроса.
		/// к подстроке добавляется секунда, если она уже генерировалась
		/// </summary>
		protected override string GenerateQuerySubstring() {
			lock (_lock) {
				var queryDateTime = DateTime.Now;
				if (queryDateTime.Second == oldQueryDateTime.Second &&
					queryDateTime.Minute == oldQueryDateTime.Minute &&
					queryDateTime.Hour == oldQueryDateTime.Hour) {
					var timeSpan = TimeSpan.FromSeconds(1);
					queryDateTime = queryDateTime + timeSpan;
					Thread.Sleep(timeSpan);
				}
				oldQueryDateTime = queryDateTime;
				return queryDateTime.ToString("yyyyMMddHHmmss");
			}
		}

		protected override bool FileCheckContent(string content, pickupXml entry) {
			var parts = content.Split(';');
			byte eqId;
			//проверяем EqId в файле
			return parts.Length >= 2 && byte.TryParse(parts[0], out eqId) &&
				DateTime.ParseExact(parts[2], "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture) >
				DateTime.ParseExact(entry.date_end, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
		}
	}
}
