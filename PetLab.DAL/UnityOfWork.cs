using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Practices.ServiceLocation;
using PetLab.DAL.Contracts;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Models.Base;
using PetLab.DAL.Contracts.Repositories.Base;
using PetLab.DAL.Repositories.Base;

namespace PetLab.DAL {
	public class UnitOfWork : IUnitOfWork {

		/// <summary>
		/// Holds registered repositories
		/// </summary>
		private readonly Dictionary<Type, IEntityRepository> _enityRepositories;

		/// <summary>
		/// Holds registered custom repositories
		/// </summary>
		private readonly Dictionary<Type, IXmlRepository> _xmlRepositories;

		/// <summary>
		/// Thread safe locker
		/// </summary>
		private readonly object _lockObject = new object();

		/// <summary>
		/// Gets application db context instance
		/// </summary>
		public IPetLabDbContext Context { get; private set; }

		/// <summary>
		/// Gets application xml context instance
		/// </summary>
		public IPetLabXmlContext XmlContext { get; private set; }

		/// <summary>
		/// Create UOW instance
		/// </summary>
		/// <param name="context">Application db context</param>
		/// <param name="xmlContext">Aplication xml context</param>
		public UnitOfWork(IPetLabDbContext context, IPetLabXmlContext xmlContext) {
			_enityRepositories = new Dictionary<Type, IEntityRepository>();
			_xmlRepositories = new Dictionary<Type, IXmlRepository>();
			Context = context;
			XmlContext = xmlContext;
		}

		/// <summary>
		/// Register custom repository
		/// </summary>
		/// <param name="xmlRepository">Custom repository instance</param>
		public void RegisterXmlRepository<T>(T xmlRepository) where T : IXmlRepository {
			if (!_xmlRepositories.ContainsKey(typeof(T)))
				_xmlRepositories.Add(typeof(T), xmlRepository);
		}

		/// <summary>
		/// Get repository by entity type
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <returns>Repository instance</returns>
		public IEntityRepository<T> GetRepository<T>() where T : BaseEntity {
			// check if repository exist in cache
			if (_enityRepositories.ContainsKey(typeof(T)))
				return _enityRepositories[typeof(T)] as IEntityRepository<T>;
			// if not then create a new instance and add to cache
			var repositoryType = typeof(EntityRepository<>).MakeGenericType(typeof(T));
			var repository = (IEntityRepository<T>)Activator.CreateInstance(repositoryType, this.Context);
			_enityRepositories.Add(typeof(T), repository);

			return repository;
		}

		/// <summary>
		/// Get repository by repository type
		/// </summary>
		/// <typeparam name="T">Custom repository type</typeparam>
		/// <returns>Custom repository instance</returns>
		public T GetXmlRepository<T>() where T : IXmlRepository {
			// check if repository exist in cache
			if (_xmlRepositories.ContainsKey(typeof (T))) {
				return (T)_xmlRepositories[typeof(T)];
			}
			var xmlRepository = (T) ServiceLocator.Current.GetService(typeof(T));
			_xmlRepositories.Add(typeof(T), xmlRepository);
			return xmlRepository;
		}

		/// <summary>
		/// The roll back.
		/// </summary>
		public void RollBack() {
			var changedEntries = this.Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

			foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified)) {
				entry.CurrentValues.SetValues(entry.OriginalValues);
				entry.State = EntityState.Unchanged;
			}

			foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added)) {
				entry.State = EntityState.Detached;
			}

			foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted)) {
				entry.State = EntityState.Unchanged;
			}
		}

		/// <summary>
		/// The commit.
		/// </summary>
		public void SaveChanges() {

			try {
				Monitor.Enter(_lockObject);
				this.Context.SaveChanges();
			} catch (DbEntityValidationException ex) {
				var sb = new StringBuilder();

				foreach (var failure in ex.EntityValidationErrors) {
					sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
					foreach (var error in failure.ValidationErrors) {
						sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
						sb.AppendLine();
					}
				}

				throw new DbEntityValidationException(
					"Entity Validation Failed - errors follow:\n" +
					sb.ToString(), ex
				); // Add the original exception as the innerException
			} finally {
				Monitor.Exit(_lockObject);
			}
		}

		public void Dispose() {
		}
	}
}
