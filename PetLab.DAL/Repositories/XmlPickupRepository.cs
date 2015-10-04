using System;
using PetLab.DAL.Contracts.Context;
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
	}
}
