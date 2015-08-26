using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PetLab.DAL.Repositories;
using PetLab.DAL.Repositories.Mock;

namespace PetLab.BLL {
	/// <summary>
	/// Represents bootstrapper for unity container
	/// </summary>
	public class Bootstrapper {
		/// <summary>
		/// Initializes unity container
		/// </summary>
		public static void Initialize() {
			var container = new UnityContainer();
#if DEBUG
			container.RegisterType<XmlDefectsRepository, MockXmlDefectsRepository>(new ContainerControlledLifetimeManager());
#else
			container.RegisterType<XmlDefectsRepository, XmlDefectsRepository>(new ContainerControlledLifetimeManager());
#endif
			// Main manager
			//container.RegisterType<ITruckCallManager, TruckCallManager>(new ContainerControlledLifetimeManager());
			//// Services
			//container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
			//container.RegisterType<IDatPowerService, DatPowerService>(new ContainerControlledLifetimeManager());
			//container.RegisterType<IDatPowerWorker, DatPowerWorker>(new ContainerControlledLifetimeManager());
			//// Managers
			//container.RegisterType<IDatPowerManager, DatPowerManager>(new ContainerControlledLifetimeManager());
			//container.RegisterType<IDataManager, DataManager>(new ContainerControlledLifetimeManager());
			//// DAL
			//container.RegisterType<ITruckCallContext, TruckCallContext>(new PerThreadLifetimeManager());
			//container.RegisterType<IPatternRepository, PatternRepository>(new PerThreadLifetimeManager());
			//container.RegisterType<IRecordFilterRepository, RecordFilterRepository>(new PerThreadLifetimeManager());
			//container.RegisterType<IEquipmentRepository, EquipmentRepository>(new PerThreadLifetimeManager());
			//container.RegisterType<ICityRepository, CityRepository>(new PerThreadLifetimeManager());
			//container.RegisterType<IStateRepository, StateRepository>(new PerThreadLifetimeManager());
			//container.RegisterType<IZoneRepository, ZoneRepository>(new PerThreadLifetimeManager());
			// Set service locator
			ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
		}
	}
}
