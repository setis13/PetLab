using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PetLab.DAL.Context;
using PetLab.DAL.Contracts.Context;
using PetLab.DAL.Contracts.Models.Base;
using PetLab.DAL.Contracts.Repositories.Base;

namespace PetLab.DAL.Repositories.Base {
	/// <summary>
	/// Represents base generic repository
	/// </summary>
	public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity {
		#region [ Protected Fields]

		/// <summary>
		/// DbContext
		/// </summary>
		protected readonly DbContext _context;
		/// <summary>
		/// DbSet
		/// </summary>
		protected readonly DbSet<T> _dbSet;

		#endregion  [ Protected Fields ]

		#region [ Properties ]

		#endregion [ Properties ]

		#region [ Constructors / Destructors ]

		/// <summary>
		/// Constructor
		/// </summary>
		public EntityRepository(IPetLabDbContext context) {
			if (context is DbContext == false) {
				throw new ArgumentException("Argument must be not null and child of DbContext");
			}
			_context = (DbContext)context;
			_dbSet = _context.Set<T>();
		}

		#endregion [ Constructors / Destructors ]

		#region [ Private Methods ]

		/// <summary>
		/// Adds entity to database context
		/// </summary>
		/// <param name="entity">Input entity</param>
		public void Insert(T entity) {
			var entityEntry = _context.Entry(entity);
			if (entityEntry.State != EntityState.Detached) {
				entityEntry.State = EntityState.Added;
			} else {
				_dbSet.Add(entity);
			}
		}
		/// <summary>
		/// Updates entity in database context
		/// </summary>
		/// <param name="entity">Input entity</param>
		public void Update(T entity) {
			var entityEntry = _context.Entry(entity);
			if (entityEntry.State == EntityState.Detached) {
				_dbSet.Attach(entity);
			}
			entityEntry.State = EntityState.Modified;
		}

		#endregion [ Private Methods ]

		#region [ Public Methods ]


		/// <summary>
		/// Gets all entities
		/// </summary>
		/// <returns>List of entities</returns>
		public virtual IEnumerable<T> GetAll() {
			return _dbSet;
		}
		/// <summary>
		/// Gets entity by id
		/// </summary>
		/// <param name="keyValues">Id of entity</param>
		/// <returns>Entity</returns>
		public virtual T GetById(params object[] keyValues) {
			return _dbSet.Find(keyValues);
		}
		/// <summary>
		/// Deletes entity by id
		/// </summary>
		/// <param name="keyValues">Id of entity</param>
		public virtual void Delete(params object[] keyValues) {
			var item = GetById(keyValues);
			if (item != null) {
				_dbSet.Remove(item);
			}
		}
		/// <summary>
		/// Deletes entities
		/// </summary>
		/// <param name="entity">entity</param>
		public void Delete(T entity) {
			_dbSet.Remove(entity);
		}
		/// <summary>
		/// Searches entities with predicate
		/// </summary>
		/// <param name="predicate">Predicate</param>
		/// <returns>List of entities</returns>
		public virtual IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate) {
			return _dbSet.Where(predicate);
		}

		/// <summary>
		/// Attaches new entity or saves existing entity to database context
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Save(T entity) {
			var originEntity = GetById(entity.GetKey());
			if (originEntity == null) {
				Insert(entity);
			} else {
				_context.Entry(originEntity).State = EntityState.Detached;
				Update(entity);
			}
		}

		public void ReferenceLoad(T pickup, Expression<Func<T, object>> func) {
			_context.Entry(pickup).Reference(func).Load();
		}

		#endregion [ Public Methods ]
	}
}
