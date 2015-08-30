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
			var result1 = service.LookupUsers();
			if (result1.Successed == false) {
				throw result1.Exception;
			}
			var result2 = service.Login(2, result1.Result.First().UserId, "2");
			if (result2.Successed == false) {
				throw result1.Exception;
			}
			Assert.IsTrue(result2.Result);
		}

		[TestMethod]
		public void UpdateDefects() {
			//получили сервис
			var service = host.GetService<IDefectsService>();
			//получили xml дефекты
			var result1 = service.LookupXmlDefectsAsync().Result;
			if (result1.Successed == false) {
				throw result1.Exception;
			}
			IEnumerable<DefectXmlDto> xmlDefects = result1.Result;
			//обновляем дефекты в db по объектам dto
			service.UpdateDefects(xmlDefects);
			//получам дефекты из db
			var result2 = service.LookupDefects();
			if (result2.Successed == false) {
				throw result2.Exception;
			}
			var defects = result2.Result;
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
			var result1 = service.LookupXmlMaterialsAsync().Result;
			if (result1.Successed == false) {
				throw result1.Exception;
			}
			IEnumerable<MaterialXmlDto> xmlMaterials = result1.Result;
			//обновляем материалы в db по объектам dto
			service.UpdateMaterials(xmlMaterials);
			//получам материалы из db
			var result2 = service.LookupMaterials();
			if (result2.Successed == false) {
				throw result2.Exception;
			}
			var defects = result2.Result;
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
				var result = service.LookupOrder("PETLIN" + (i + 1).ToString("D2")).Result;
				if (result.Successed == false) {
					throw result.Exception;
				}
			}
		}
	}
}
