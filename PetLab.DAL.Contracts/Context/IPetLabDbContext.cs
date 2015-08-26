using System;
using System.Data.Entity.Infrastructure;

namespace PetLab.DAL.Contracts.Context {
	public interface IPetLabDbContext : IDisposable {
		int SaveChanges();
		DbChangeTracker ChangeTracker { get; }
    }
}
