using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetLab.BLL;
using PetLab.BLL.Common.Dto;
using PetLab.BLL.Common.Services.Results;
using PetLab.BLL.Contracts;
using PetLab.BLL.Contracts.Services;
using PetLab.BLL.Services;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models;
using PetLab.DAL.Repositories;
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
			IEnumerable<UserDto> users = CheckResult(service.LookupUsers());
			CheckResult(service.Login(2, users.First().UserId, "2"));
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void UpdateDefects() {
			//получили сервис
			var service = host.GetService<IDefectsService>();
			//получили xml дефекты
			IEnumerable<DefectXmlDto> xmlDefects = CheckResult(service.LookupXmlDefectsAsync().Result);
			//обновляем дефекты в db по объектам dto
			CheckResult(service.UpdateDefects(xmlDefects));
			//получам дефекты из db
			IEnumerable<DefectDto> defects = CheckResult(service.LookupDefects());
			//сравниваем результаты
			foreach (var xmlDefect in xmlDefects) {
				DefectDto dto = defects.FirstOrDefault(d => d.DefectId.Trim() == xmlDefect.DefectId.Trim());
				if (dto == null) {
					Assert.Fail("Дефект не найден. defect_id: {0}", xmlDefect.DefectId);
					return;
				}
				if (xmlDefect.Name != dto.Name) {
					Assert.Fail("Дефект не совпадает. Xml Defect Name:{0}, DB Defect Name: {1}", xmlDefect.Name, dto.Name);
					return;
				}
			}
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void UpdateMaterials() {
			//получили сервис
			var service = host.GetService<IMaterialsService>();
			//получили xml материалы
			IEnumerable<MaterialXmlDto> xmlMaterials = CheckResult(service.LookupXmlMaterialsAsync().Result);
			//обновляем материалы в db по объектам dto
			CheckResult(service.UpdateMaterials(xmlMaterials));
			//получам материалы из db
			IEnumerable<MaterialDto> defects = CheckResult(service.LookupMaterials());
			//сравниваем результаты
			foreach (var xmlMaterial in xmlMaterials) {
				MaterialDto dto = defects.FirstOrDefault(d => d.MaterialId.Trim() == xmlMaterial.MaterialId.Trim());
				if (dto == null) {
					Assert.Fail("Материал не найден. defect_id: {0}", xmlMaterial.MaterialId);
					return;
				}
				if (xmlMaterial.Name != dto.Name) {
					Assert.Fail("Материал не совпадает. Xml Material Name:{0}, DB Material Name: {1}", xmlMaterial.Name, dto.Name);
					return;
				}
			}
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void GetOrder() {
			var random = new Random();
			var service = host.GetService<IPickupService>();
			for (int i = 0; i < 11; i++) {
				CheckResult(service.LookupOrder("PETLIN" + (i + 1).ToString("D2")).Result);
			}
		}

		/// <summary>
		/// OpenPickup
		/// SetPickupDefect
		/// SetColor
		/// SetEtalonMatch
		/// SetVisualMatch
		/// ClosePickup
		/// </summary>
		[TestMethod]
		public void FullTestPickup() {
			var random = new Random();
			var service = host.GetService<IPickupService>();
			var eqId = "PETLIN" + (random.Next(11) + 1).ToString("D2");
			var order = CheckResult(service.LookupOrder(eqId).Result);
			if (order == null) {
				Assert.Inconclusive("нет заказа");
			}
			var pickup = CheckResult(service.LookupOpenPickup(eqId));
			if (pickup != null) {
				CheckResult(service.ClosePickup(pickup.PickupId));
			}
			var boxId = random.Next(1000).ToString("D8");
			pickup = CheckResult(service.OpenPickup(order.OrderId, boxId, 1, 1, DateTime.Now, (byte)(random.Next(4) + 1)));
			var defects = CheckResult(service.LookupDefects()).ToArray();
			for (int i = 0; i < random.Next(order.CountSocket); i++) {
				byte? grade = 0;
				switch (random.Next(3)) {
					case 0:
						grade = null;
						break;
					case 1:
						grade = 0;
						break;
					case 2:
						grade = 1;
						break;
				}
				CheckResult(service.SetPickupDefect(pickup.PickupId,
					defects[random.Next(defects.Length)].DefectId,
					(byte)random.Next(order.CountSocket), grade));
			}
			CheckResult(service.SetEtalonMatch(pickup.PickupId, random.Next(2) == 0));
			CheckResult(service.SetVisualMatch(pickup.PickupId, random.Next(2) == 0));
			var ranges = CheckResult(service.LookupEtalonColorRanges(pickup.OrderId)).ToArray();
			for (int i = 0; i < ranges.Length; i++) {
				CheckResult(service.SetColor(pickup.PickupId, pickup.OrderId, ranges[i].Name, (decimal?)random.Next(100) / 10));
			}
			CheckResult(service.ClosePickup(pickup.PickupId));
		}

		private T CheckResult<T>(ServiceResult<T> result) {
			if (result.Successed == false) {
				Assert.Fail(result.Exception.Message);
			}
			return result.Result;
		}

		private void CheckResult(ServiceResult result) {
			if (result.Successed == false) {
				Assert.Fail(result.Exception.Message);
			}
		}
	}
}
