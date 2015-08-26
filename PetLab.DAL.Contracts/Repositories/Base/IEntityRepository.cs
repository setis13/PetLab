using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PetLab.DAL.Contracts.Repositories.Base {

	public interface IEntityRepository {
		
	}
	/// <summary>
	/// Represents interface for base generic repository
	/// </summary>
	/// <typeparam name="T">Class</typeparam>
	public interface IEntityRepository<T> : IEntityRepository where T : class {
		void Update(T entity);
        void Insert(T entity);

		/// <summary>
		/// Gets all entities
		/// </summary>
		/// <returns>List of entities</returns>
		IEnumerable<T> GetAll();
		/// <summary>
		/// Gets entity by id
		/// </summary>
		/// <param name="keyValues">Id of entity</param>
		/// <returns>Entity</returns>
		T GetById(params object[] keyValues);
		/// <summary>
		/// Deletes entity by id
		/// </summary>
		/// <param name="keyValues">Id of entity</param>
		void Delete(params object[] keyValues);
		/// <summary>
		/// Deletes entities
		/// </summary>
		/// <param name="entity">entity</param>
		void Delete(T entity);
		/// <summary>
		/// Searches entities with predicate
		/// </summary>
		/// <param name="predicate">Predicate</param>
		/// <returns>List of entities</returns>
		IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
	}
}
