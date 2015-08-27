using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PetLab.BLL;
using PetLab.DAL;
using PetLab.DAL.Context;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Repositories.Base;
using Unity.AutoRegistration;

namespace PetLab.WPF.App_Start {
	public class UnityConfig {
		#region Unity Container

		private static readonly Lazy<IUnityContainer> MercuryUnityContainer = new Lazy<IUnityContainer>(() => {
			var container = new UnityContainer();
			RegisterTypes(container);
			return container;
		});

		/// <summary>
		/// Gets the configured Unity container.
		/// </summary>
		public static IUnityContainer GetConfiguredContainer() {
			return MercuryUnityContainer.Value;
		}

		#endregion

		/// <summary>Registers the type mappings with the Unity container.</summary>
		/// <param name="container">The unity container to configure.</param>
		/// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
		/// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
		public static void RegisterTypes(IUnityContainer container) {
			// register Db context
			container.RegisterType<IPetLabDbContext, PetLabDbContext>(new ContainerControlledLifetimeManager());
			// register Xml context
			container.RegisterType<IPetLabXmlContext, PetLabXmlContext>(new ContainerControlledLifetimeManager());

			var host = new ServicesHost();
#if DEBUG
			container
			  .ConfigureAutoRegistration()
			  .LoadAssemblyFrom(Assembly.GetExecutingAssembly().Location)
			  .ExcludeAssemblies(a => !a.FullName.ToLowerInvariant().StartsWith("petlab.bll"))
			  .Include((type) => type.Implements<IXmlRepository>() && type.IsClass && 
			  !type.IsAbstract && !type.IsGenericType, Then.Register().
			  UsingLifetime<HierarchicalLifetimeManager>().As<IXmlRepository>().WithName(t => t.FullName))
			  .ApplyAutoRegistration();
			//container.RegisterType<XmlDefectsRepository, MockXmlDefectsRepository>(new ContainerControlledLifetimeManager());
			//container.RegisterType<XmlMaterialsRepository, MockXmlMaterialsRepository>(new ContainerControlledLifetimeManager());
			//container.RegisterType<XmlOrderRepository, MockXmlOrderRepository>(new ContainerControlledLifetimeManager());
			//container.RegisterType<XmlPickupRepository, MockXmlPickupRepository>(new ContainerControlledLifetimeManager());
#else

#endif

			// register Unit of Work
			container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionFactory(c => {
				var uow = new UnitOfWork(c.Resolve<IPetLabDbContext>(), c.Resolve<IPetLabXmlContext>());

				c.Registrations
				.Where(item => item.RegisteredType == typeof(IPetLabXmlContext) && !item.MappedToType.IsInterface && !item.MappedToType.IsGenericType && !item.MappedToType.IsAbstract && !String.IsNullOrEmpty(item.Name))
				.ForEach(item => c.Resolve<IPetLabXmlContext>(item.Name, new ResolverOverride[] {
					new ParameterOverride("unitOfWork", uow)
				}));

				return uow;
			}));

			ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
		}
	}
}