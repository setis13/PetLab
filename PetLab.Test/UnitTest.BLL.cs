using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetLab.BLL;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Services;
using PetLab.DAL.Contracts;
using PetLab.WPF;

namespace PetLab.Test {
	[TestClass]
	public class UnitTestService {

		#region private

		/// <summary>
		/// Service Host
		/// </summary>
		private IServicesHost host;

		#endregion private

		#region .ctr

		public UnitTestService() {
			AppBootstrapper.Init();

			host = (IServicesHost)ServiceLocator.Current.GetService(typeof(IServicesHost));
		}

		#endregion .ctr

		[TestMethod]
		public void LookupUser() {
			var service = host.GetService<IIdentityService>();
			var users = service.LookupUser();
			var result = service.Login(2, users.Result.First().UserId, "2");
			Assert.IsTrue(result.Result);
		}

	}
}
