using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models;
using PetLab.DAL.Models.xml;
using PetLab.DAL.Repositories;
using PetLab.WPF;

namespace PetLab.Test {
	[TestClass]
	public class UnitTestEntityRepository {

		#region private

		/// <summary>
		/// Unity of Work
		/// </summary>
		private IUnitOfWork uow;

		#endregion private

		#region .ctr

		public UnitTestEntityRepository() {
			AppBootstrapper.Init();

			uow = (IUnitOfWork)ServiceLocator.Current.GetService(typeof(IUnitOfWork));
		}

		#endregion .ctr

		#region testing orders

		[TestMethod]
		public void OrderClear() {
			var repository = uow.GetRepository<order>();
			repository.Delete("0099999900");
			uow.SaveChanges();
		}

		[TestMethod]
		public void OrderCreate() {
			var repository = uow.GetRepository<order>();
			var order = new order();
			order.batch_id = "123";
			order.color_shade = "цвет";
			order.count_socket = 96;
			order.dye_name = "краситель";
			order.equipment_id = "PETLIN01";
			order.material_id = "9999999999";
			order.order_id = "0099999900";
			order.shift_number_number = 1;
			repository.Insert(order);
			uow.SaveChanges();
		}

		[TestMethod]
		public void OrderUpdate() {
			var repository = uow.GetRepository<order>();
			var order = repository.GetById("0099999900");
			order.batch_id = "321";
			repository.Update(order);
			uow.SaveChanges();
		}

		[TestMethod]
		public void OrderDelete() {
			var repository = uow.GetRepository<order>();
			var order = repository.GetById("0099999900");
			repository.Delete(order);
			uow.SaveChanges();
		}

		#endregion testing orders

		#region testing xml

		[TestMethod]
		public void GetDefects() {
			var repository = uow.GetXmlRepository<XmlDefectsRepository>();
			var defects = repository.Get();
			Assert.IsTrue(defects.defect.Any());
		}
		[TestMethod]
		public void GetMaterials() {
			var repository = uow.GetXmlRepository<XmlMaterialsRepository>();
			var materials = repository.Get();
			Assert.IsTrue(materials.material.Any());
		}
		[TestMethod]
		public void GetOrder() {
			var random = new Random();
			var repository = uow.GetXmlRepository<XmlOrderRepository>();
			var eqId = (byte)(random.Next(11) + 1);
			var order = repository.Get("PETLIN" + eqId.ToString("D2"));
			Assert.AreEqual<string>(order.equipment, "PETLIN" + eqId.ToString("D2"));
		}
		[TestMethod]
		public void SavePickup() {
			var random = new Random();
			var repository = uow.GetXmlRepository<XmlPickupRepository>();
			var pickup = new pickupXml();
			pickup.defect = new[] {
				new pickupDefect_meas() { defect_id = "00" + (random.Next(3)+1),grade =(byte) random.Next(1),socket = (byte) random.Next(96)},
				new pickupDefect_meas() { defect_id = "00" + (random.Next(3)+1),grade =(byte) random.Next(1),socket = (byte) random.Next(96)},
				new pickupDefect_meas() { defect_id = "00" + (random.Next(3)+1),grade =(byte) random.Next(1),socket = (byte) random.Next(96)},
			};
			pickup.color = new pickupColor() { name = "Б0-1", value_ik = (decimal)10.1, value_vd = (decimal)5.6 };
			pickup.date_begin = DateTime.Now;
			pickup.date_end = DateTime.Now - new TimeSpan(0, 0, 10);
			pickup.date_take = DateTime.Now - new TimeSpan(0, 0, 2);
			pickup.equipment = "PETLIN" + (random.Next(11) + 1).ToString("D2");
			pickup.etalon_match = random.Next(2) == 1;
			pickup.number = (byte)random.Next(10);
			pickup.slip = new[] {
				new pickupSlip_meas() {value = (decimal) 4.9, deviation = (decimal) 1.1, step = 120}
			};
			pickup.station_cooling = "D";
			pickup.thickness = new[] {
				new pickupThickness_meas() {value = (decimal) 10.3, socket = (byte) random.Next(96)}
			};
			pickup.user = "Иванов И.И.";
			pickup.weight = new[] {
				new pickupWeight_meas() {value = (decimal) 12.1, socket = (byte) random.Next(96)}
			};

			repository.Insert(pickup);
			repository.SaveChanges();
		}

		#endregion testing xml
	}
}
