using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetLab.DAL;
using PetLab.DAL.Contracts;
using PetLab.DAL.Models;
using PetLab.WPF;

namespace PetLab.Test.DAL {
	[TestClass]
	public class UnitTestEntityRepository {
		private IUnitOfWork uow;

		public UnitTestEntityRepository() {
			AppBootstrapper.Init();

			uow = (IUnitOfWork) ServiceLocator.Current.GetService(typeof(IUnitOfWork));
		}

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
			order.shift_id = 1;
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
	}
}
