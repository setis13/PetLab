using System.Data.Entity;
using PetLab.BLL.Mappings;
using PetLab.DAL.Context;
using PetLab.WPF.App_Start;
using PetLab.WPF.Mappings;

namespace PetLab.WPF {
	public class AppBootstrapper {
		public static void Init() {
			var container = UnityConfig.GetConfiguredContainer();
			//// DB context init
			Database.SetInitializer(new PetLabDbContextInitializer());
			//// Automapper
			AutoMapper.Mapper.AddProfile<BllMappingProfile>();
			AutoMapper.Mapper.AddProfile<ViewMappingProfile>();
			//AutoMapper.Mapper.AssertConfigurationIsValid();
		}
	}
}
