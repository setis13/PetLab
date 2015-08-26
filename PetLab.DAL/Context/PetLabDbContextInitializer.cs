using System.Data.Entity;

namespace PetLab.DAL.Context {
	/// <summary>
	/// Application db context initializer (migrations)
	/// </summary>
	public class PetLabDbContextInitializer : 
		MigrateDatabaseToLatestVersion<PetLabDbContext, PetLabDbContextConfiguration> {
	}
}
